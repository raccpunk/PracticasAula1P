﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AnimalSpawn.Domain.Entities;
using AnimalSpawn.Domain.Exceptions;
using AnimalSpawn.Domain.Interfaces;
using AnimalSpawn.Domain.QueryFilters;

namespace AnimalSpawn.Application.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AnimalService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }


        public async Task AddAnimal(Animal animal)
        {
            Expression<Func<Animal,bool>> expressionAnimal = item => item.Name == animal.Name;

            var animals = _unitOfWork.AnimalRepository.FindByCondition(expressionAnimal);

            if (animals.Any(item => item.Name == animal.Name))
                throw new BusinessException("This animal name already exist.");

            if (animal?.EstimatedAge > 0 && (animal?.Weight <= 0 || animal?.Height <= 0))
                throw new BusinessException("The height and weight should be greater than zero.");

            var older = DateTime.Now - (animal?.CaptureDate ?? DateTime.Now);

            if (older.TotalDays > 45)
                throw new BusinessException("The animal's capture date is older than 45 days");

            if (animal.RfidTag != null)
            {
                Expression<Func<RfidTag, bool>> expressionTag = tag => tag.Tag == animal.RfidTag.Tag;
                var tags = _unitOfWork.RfifTagRepository.FindByCondition(expressionTag);
                if (tags.Any())
                    throw new BusinessException("This animal's tag rfid is already exist");
            }                  

            await _unitOfWork.AnimalRepository.Add(animal);
        }

        public async Task DeleteAnimal(int id)
        {
            await _unitOfWork.AnimalRepository.Delete(id);
            await _unitOfWork.SavechangesAsync();
        }

        public async Task<Animal> GetAnimal(int id)
        {
            return await _unitOfWork.AnimalRepository.GetById(id);
        }

        public IEnumerable<Animal> GetAnimals(AnimalQueryFilter filter)
        {
            var animals= _unitOfWork.AnimalRepository.GetAnimals(filter);
            return animals;
        }

        public void UpdateAnimal(Animal animal)
        {
            _unitOfWork.AnimalRepository.Update(animal);
            _unitOfWork.SaveChanges();
        }
    }
}
