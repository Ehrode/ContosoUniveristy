using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContosoUniveristy.Core.Entities;
using ContosoUniveristy.Core.Interfaces;

namespace ContosoUniversity.Infrastructure.Data
{
    public class SchoolContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Instructors).WithMany(i => i.Courses)
                .Map(t => t.MapLeftKey("CourseID")
                    .MapRightKey("InstructorID")
                    .ToTable("CourseInstructor"));

            modelBuilder.Entity<Course>()
                .Property(c => c.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<Course>()
                .Property(c => c.Title)
                .HasMaxLength(50);

            modelBuilder.Entity<Department>()
                .Property(d => d.Budget)
                .HasColumnType("money");

            modelBuilder.Entity<OfficeAssignment>()
                .HasKey(oa => oa.InstructorId)
                .HasRequired(oa => oa.Instructor)
                .WithRequiredPrincipal();

            modelBuilder.Entity<OfficeAssignment>()
                .Property(oa => oa.Location)
                .HasMaxLength(50);

        }
    }
}
