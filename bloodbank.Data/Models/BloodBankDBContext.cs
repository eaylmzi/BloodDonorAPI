﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace bloodbank.Data.Models
{
    public partial class BloodBankDBContext : DbContext
    {
        public BloodBankDBContext()
        {
        }

        public BloodBankDBContext(DbContextOptions<BloodBankDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Hospital> Hospitals { get; set; } = null!;
        public virtual DbSet<BloodRequest> BloodRequests { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server=tcp:bloodbank.database.windows.net,1433;Initial Catalog=BloodBankDB;Persist Security Info=False;User ID=emreyilmaz;Password=Bloodbank123*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hospital>(entity =>
            {
                entity.ToTable("hospital");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AMinusBloodUnit).HasColumnName("a_minus_blood_unit");

                entity.Property(e => e.APlusBloodUnit).HasColumnName("a_plus_blood_unit");

                entity.Property(e => e.AbMinusBloodUnit).HasColumnName("ab_minus_blood_unit");

                entity.Property(e => e.AbPlusBloodUnit).HasColumnName("ab_plus_blood_unit");

                entity.Property(e => e.BMinusBloodUnit).HasColumnName("b_minus_blood_unit");

                entity.Property(e => e.BPlusBloodUnit).HasColumnName("b_plus_blood_unit");

                entity.Property(e => e.GeopointId).HasColumnName("geopoint_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.ZeroMinusBloodUnit).HasColumnName("zero_minus_blood_unit");

                entity.Property(e => e.ZeroPlusBloodUnit).HasColumnName("zero_plus_blood_unit");
            });
            modelBuilder.Entity<BloodRequest>(entity =>
            {
                entity.ToTable("blood_request");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.BloodCount).HasColumnName("blood_count");
                entity.Property(e => e.BloodType).HasColumnName("blood_type");

                entity.Property(e => e.DurationTime)
                    .HasColumnName("duration_time");
                entity.Property(e => e.City).HasColumnName("city");
                entity.Property(e => e.Town).HasColumnName("town");
                entity.Property(e => e.HospitalLongitude).HasColumnName("hospital_longitude");
                entity.Property(e => e.HospitalLatitude).HasColumnName("hospital_latitude");
                entity.Property(e => e.HospitalEmail).HasColumnName("hospital_email");

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
