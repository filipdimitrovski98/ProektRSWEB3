using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project_1_Final.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Project_1_Final.Data
{
    public class Project_1_FinalContext : DbContext //IdentityDbContext<AppUser> //!!!
    {
        public Project_1_FinalContext (DbContextOptions<Project_1_FinalContext> options)
            : base(options)
        {
        }

        public DbSet<Project_1_Final.Models.Course> Course { get; set; }

        public DbSet<Project_1_Final.Models.Student> Student { get; set; }

        public DbSet<Project_1_Final.Models.Teacher> Teacher { get; set; }
        public DbSet<Project_1_Final.Models.Enrollment> Enrollment { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Enrollment>()
            .HasOne<Student>(p => p.Student)
            .WithMany(p => p.Courses)
            .HasForeignKey(p => p.StudentId);
            builder.Entity<Enrollment>()
            .HasOne<Course>(p => p.Course)
            .WithMany(p => p.Students)
            .HasForeignKey(p => p.CourseId);
            builder.Entity<Course>()
            .HasOne<Teacher>(p => p.FirstTeacher)
            .WithMany(p => p.Courses)
            .HasForeignKey(p => p.FirstTeacherId);
            builder.Entity<Course>()
            .HasOne<Teacher>(p => p.SecondTeacher)
            .WithMany(p => p.CoursesSec)
            .HasForeignKey(p => p.SecondTeacherId);
        }

    }
}
