﻿using System;

namespace AnimalSpawn.Domain.Interfaces
{
    public interface IEntity
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public DateTime CreateAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
