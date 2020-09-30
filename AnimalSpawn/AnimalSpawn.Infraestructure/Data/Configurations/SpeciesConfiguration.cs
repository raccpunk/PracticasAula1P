﻿using AnimalSpawn.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalSpawn.Infraestructure.Data.Configurations
{
    public class SpeciesConfiguration : IEntityTypeConfiguration<Species>
    {
        public void Configure(EntityTypeBuilder<Species> builder)
        {
            builder.Property(e => e.Code)
                   .HasMaxLength(10)
                   .IsUnicode(false);

            builder.Property(e => e.CommonName)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.CreateAt).HasColumnType("datetime");

            builder.Property(e => e.HabitatEcology)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.ScientificName)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.Status).HasDefaultValueSql("((1))");

            builder.Property(e => e.UpdateAt).HasColumnType("datetime");
        }
    }
}
