using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace donor.Data.Models
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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:bloodbank.database.windows.net,1433;Initial Catalog=DonorDB;Persist Security Info=False;User ID=emreyilmaz;Password=Bloodbank123*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Branch>(entity =>
            {
                entity.ToTable("branch");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AMinusBloodUnit).HasColumnName("a_minus_blood_unit");

                entity.Property(e => e.APlusBloodUnit).HasColumnName("a_plus_blood_unit");

                entity.Property(e => e.AbMinusBloodUnit).HasColumnName("ab_minus_blood_unit");

                entity.Property(e => e.AbPlusBloodUnit).HasColumnName("ab_plus_blood_unit");

                entity.Property(e => e.BMinusBloodUnit).HasColumnName("b_minus_blood_unit");

                entity.Property(e => e.BPlusBloodUnit).HasColumnName("b_plus_blood_unit");

                entity.Property(e => e.City).HasColumnName("city");

                entity.Property(e => e.GeopointId).HasColumnName("geopoint_id");

                entity.Property(e => e.Town).HasColumnName("town");

                entity.Property(e => e.ZeroMinusBloodUnit).HasColumnName("zero_minus_blood_unit");

                entity.Property(e => e.ZeroPlusBloodUnit).HasColumnName("zero_plus_blood_unit");
            });

            modelBuilder.Entity<DonationHistory>(entity =>
            {
                entity.ToTable("donation_history");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DonationTime)
                    .HasColumnName("donation_time");

                entity.Property(e => e.DonorId).HasColumnName("donor_id");

                entity.Property(e => e.TupleCount).HasColumnName("tuple_count");
            });

            modelBuilder.Entity<Donor>(entity =>
            {
                entity.ToTable("donor");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BloodType)
                    .HasMaxLength(3)
                    .HasColumnName("blood_type");

                entity.Property(e => e.BranchId).HasColumnName("branch_id");

                entity.Property(e => e.City).HasColumnName("city");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .HasColumnName("phone");

                entity.Property(e => e.Surname)
                    .HasMaxLength(50)
                    .HasColumnName("surname");

                entity.Property(e => e.Town).HasColumnName("town");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
