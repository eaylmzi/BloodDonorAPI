using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BloodBankAPI.Models
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:blood-bank.database.windows.net,1433;Initial Catalog=BloodBankDB;Persist Security Info=False;User ID=emreyilmaz;Password=Blooddonor123*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hospital>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("hospital");

                entity.Property(e => e.AMinusBloodUnit).HasColumnName("a_minus_blood_unit");

                entity.Property(e => e.APlusBloodUnit).HasColumnName("a_plus_blood_unit");

                entity.Property(e => e.AbMinusBloodUnit).HasColumnName("ab_minus_blood_unit");

                entity.Property(e => e.AbPlusBloodUnit).HasColumnName("ab_plus_blood_unit");

                entity.Property(e => e.BMinusBloodUnit).HasColumnName("b_minus_blood_unit");

                entity.Property(e => e.BPlusBloodUnit).HasColumnName("b_plus_blood_unit");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.ZeroMinusBloodUnit).HasColumnName("zero_minus_blood_unit");

                entity.Property(e => e.ZeroPlusBloodUnit).HasColumnName("zero_plus_blood_unit");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
