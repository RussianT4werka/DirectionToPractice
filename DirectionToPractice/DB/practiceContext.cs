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
        }

        public practiceContext(DbContextOptions<practiceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<Practice> Practices { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;

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
            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Group");
            });

            modelBuilder.Entity<Practice>(entity =>
            {
                entity.ToTable("Practice");

                entity.Property(e => e.DateEnd).HasColumnType("date");

                entity.Property(e => e.DateStart).HasColumnType("date");

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Practices)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Practice_Student");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Patronymic).HasMaxLength(50);

                entity.Property(e => e.Surname).HasMaxLength(50);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_Group");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public static practiceContext GetInstance()
        {
            if(instance == null)
            {
                instance = new practiceContext();
            }
            return instance;
        }
    }
}
