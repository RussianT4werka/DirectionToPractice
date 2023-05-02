using System;
using System.Collections.Generic;
using DirectionToPractice.DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DirectionToPractice.DB
{
    public partial class practiceContext : DbContext
    {
        private static practiceContext instance;

        public practiceContext()
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<Gender> Genders { get; set; } = null!;
        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<Speciality> Specialities { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<Teacher> Teachers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=192.168.200.35;user=user50;database=practice;password=26643;TrustServerCertificate=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Cyrillic_General_100_CI_AI_SC_UTF8");

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("Gender");

                entity.Property(e => e.Name).HasMaxLength(7);
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasKey(e => e.Number)
                    .HasName("PK_Group_1");

                entity.ToTable("Group");

                entity.HasIndex(e => e.SpecialityId, "IX_Group_SpecialityID");

                entity.Property(e => e.Number).ValueGeneratedNever();

                entity.Property(e => e.SpecialityId).HasColumnName("SpecialityID");

                entity.HasOne(d => d.Speciality)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.SpecialityId)
                    .HasConstraintName("FK_Group_Speciality");
            });

            modelBuilder.Entity<Speciality>(entity =>
            {
                entity.ToTable("Speciality");

                entity.Property(e => e.Code).HasMaxLength(11);

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.HasIndex(e => e.GenderId, "IX_Student_GenderID");

                entity.HasIndex(e => e.GroupNumber, "IX_Student_GroupID");

                entity.Property(e => e.GenderId).HasColumnName("GenderID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Patronymic).HasMaxLength(50);

                entity.Property(e => e.Surname).HasMaxLength(50);

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_Gender");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.GroupNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_Group");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("Teacher");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Patronymic).HasMaxLength(50);

                entity.Property(e => e.Surname).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public static practiceContext GetInstance()
        {
            if (instance == null)
            {
                instance = new practiceContext();
            }
            return instance;
        }
    }
}
