using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CourseSelectionSystem.Entitys
{
    public partial class course_selectionContext : DbContext
    {
        public course_selectionContext()
        {
        }

        public course_selectionContext(DbContextOptions<course_selectionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CourseDb> CourseDb { get; set; }
        public virtual DbSet<StudentCourseMaping> StudentCourseMaping { get; set; }
        public virtual DbSet<StudentDb> StudentDb { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=192.168.127.131;port=3306;user=root;password=test;database=course_selection", x => x.ServerVersion("8.0.19-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseDb>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PRIMARY");

                entity.ToTable("course_db");

                entity.HasComment("课程信息");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Credit).HasColumnName("credit");

                entity.Property(e => e.IsElective).HasColumnName("is_elective");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(64)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<StudentCourseMaping>(entity =>
            {
                entity.ToTable("student_course_maping");

                entity.HasIndex(e => e.CourseId)
                    .HasName("FK_Reference_1");

                entity.HasIndex(e => e.StudentId)
                    .HasName("FK_Reference_2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CourseId).HasColumnName("course_id");

                entity.Property(e => e.StudentId).HasColumnName("student_id");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.StudentCourseMaping)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_Reference_1");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentCourseMaping)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_Reference_2");
            });

            modelBuilder.Entity<StudentDb>(entity =>
            {
                entity.ToTable("student_db");

                entity.HasComment("学生信息");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Class)
                    .HasColumnName("class")
                    .HasColumnType("varchar(64)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Credit).HasColumnName("credit");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(64)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
