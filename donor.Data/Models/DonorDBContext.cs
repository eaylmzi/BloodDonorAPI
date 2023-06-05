using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BloodBankAPI.Models
{
    public partial class DonorDBContext : DbContext
    {
        public DonorDBContext()
        {
        }

        public DonorDBContext(DbContextOptions<DonorDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Branch> Branches { get; set; } = null!;
        public virtual DbSet<DonationHistory> DonationHistories { get; set; } = null!;
        public virtual DbSet<Donor> Donors { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:blood-bank.database.windows.net,1433;Initial Catalog=DonorDB;Persist Security Info=False;User ID=emreyilmaz;Password=Blooddonor123*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Branch>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("branch");

                entity.Property(e => e.AMinusBloodUnit).HasColumnName("a_minus_blood_unit");

                entity.Property(e => e.APlusBloodUnit).HasColumnName("a_plus_blood_unit");

                entity.Property(e => e.AbMinusBloodUnit).HasColumnName("ab_minus_blood_unit");

                entity.Property(e => e.AbPlusBloodUnit).HasColumnName("ab_plus_blood_unit");

                entity.Property(e => e.BMinusBloodUnit).HasColumnName("b_minus_blood_unit");

                entity.Property(e => e.BPlusBloodUnit).HasColumnName("b_plus_blood_unit");

                entity.Property(e => e.CityId).HasColumnName("city_id");

                entity.Property(e => e.GeopointId).HasColumnName("geopoint_id");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.TownId).HasColumnName("town_id");

                entity.Property(e => e.ZeroMinusBloodUnit).HasColumnName("zero_minus_blood_unit");

                entity.Property(e => e.ZeroPlusBloodUnit).HasColumnName("zero_plus_blood_unit");
            });

            modelBuilder.Entity<DonationHistory>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("donation_history");

                entity.Property(e => e.DonationTime)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("donation_time");

                entity.Property(e => e.DonorId).HasColumnName("donor_id");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.TupleCount).HasColumnName("tuple_count");
            });

            modelBuilder.Entity<Donor>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("donor");

                entity.Property(e => e.BloodType)
                    .HasMaxLength(3)
                    .HasColumnName("blood_type");

                entity.Property(e => e.BranchId).HasColumnName("branch_id");

                entity.Property(e => e.CityId).HasColumnName("city_id");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .HasColumnName("phone");

                entity.Property(e => e.Surname)
                    .HasMaxLength(50)
                    .HasColumnName("surname");

                entity.Property(e => e.TownId).HasColumnName("town_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
