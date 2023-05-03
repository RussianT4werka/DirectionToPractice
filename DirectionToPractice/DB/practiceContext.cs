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

        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<ModulePractice> ModulePractices { get; set; } = null!;
        public virtual DbSet<PracticeType> PracticeTypes { get; set; } = null!;
        public virtual DbSet<Speciality> Specialities { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<Teacher> Teachers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=practice;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Cyrillic_General_100_CI_AI_SC_UTF8");

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

            modelBuilder.Entity<ModulePractice>(entity =>
            {
                entity.ToTable("ModulePractice");

                entity.HasIndex(e => e.SpecialityId, "IX_ModulePractice_SpecialityID");

                entity.Property(e => e.Number).HasMaxLength(10);

                entity.Property(e => e.PracticeTypeId).HasColumnName("PracticeTypeID");

                entity.Property(e => e.SpecialityId).HasColumnName("SpecialityID");

                entity.Property(e => e.Text).HasMaxLength(100);

                entity.HasOne(d => d.PracticeType)
                    .WithMany(p => p.ModulePractices)
                    .HasForeignKey(d => d.PracticeTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ModulePractice_PracticeType");

                entity.HasOne(d => d.Speciality)
                    .WithMany(p => p.ModulePractices)
                    .HasForeignKey(d => d.SpecialityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ModulePractice_Speciality");
            });

            modelBuilder.Entity<PracticeType>(entity =>
            {
                entity.ToTable("PracticeType");

                entity.Property(e => e.Name).HasMaxLength(20);
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

                entity.HasIndex(e => e.GroupNumber, "IX_Student_GroupID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Patronymic).HasMaxLength(50);

                entity.Property(e => e.Surname).HasMaxLength(50);

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
