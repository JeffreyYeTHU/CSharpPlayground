using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MysqlPerTest.Entity
{
    public partial class PerfTestContext : DbContext
    {
        public PerfTestContext()
        {
        }

        public PerfTestContext(DbContextOptions<PerfTestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Studient> Studients { get; set; }
        public virtual DbSet<TestScore> TestScores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseMySql("server=localhost;port=3307;userid=root;password=123456;database=perf_test", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.27-mysql"));
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("course");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CourseName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("course_name");

                entity.Property(e => e.TeacherName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("teacher_name");
            });

            modelBuilder.Entity<Studient>(entity =>
            {
                entity.ToTable("studient");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Birthday).HasColumnName("birthday");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("firstname");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("gender");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lastname");
            });

            modelBuilder.Entity<TestScore>(entity =>
            {
                entity.ToTable("test_score");

                entity.HasIndex(e => e.CourseId, "fkey_course_id");

                entity.HasIndex(e => e.StudientId, "fkey_studient_id");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CourseId).HasColumnName("course_id");

                entity.Property(e => e.Score).HasColumnName("score");

                entity.Property(e => e.StudientId).HasColumnName("studient_id");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.TestScores)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkey_course_id");

                entity.HasOne(d => d.Studient)
                    .WithMany(p => p.TestScores)
                    .HasForeignKey(d => d.StudientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkey_studient_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
