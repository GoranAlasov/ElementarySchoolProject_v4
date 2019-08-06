using ElementarySchoolProject.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ElementarySchoolProject.Infrastructure
{
    public class DataAccessContext : IdentityDbContext<ApplicationUser>
    {
        public DataAccessContext() : base("Elementary")
        {
            Database.SetInitializer<DataAccessContext>(new DataAccessContextInitializer());
        }

        public DbSet<SchoolClass> SchoolClasses { get; set; }
        public DbSet<SchoolSubject> SchoolSubjects { get; set; }
        public DbSet<TeacherSchoolSubject> TeacherSchoolSubjects { get; set; }
        public DbSet<SchoolClassTeacherSchoolSubject> SchoolClassTeacherSchoolSubjects { get; set; }
        public DbSet<Grade> Grades { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Admin>().ToTable("Admins");
            modelBuilder.Entity<Teacher>().ToTable("Teachers");
            modelBuilder.Entity<Parent>().ToTable("Parents");
            modelBuilder.Entity<Student>().ToTable("Students");            
        }        
    }
}