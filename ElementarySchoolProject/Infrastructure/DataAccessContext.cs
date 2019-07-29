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
        //public DataAccessContext() : base("RemoteElementary")
        {
            Database.SetInitializer<DataAccessContext>(new DataAccessContextInitializer());
        }            

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Admin>().ToTable("Admins");
            modelBuilder.Entity<Teacher>().ToTable("Teachers");
            modelBuilder.Entity<Parent>().ToTable("Parents");
            modelBuilder.Entity<Student>().ToTable("Students");

            //modelBuilder.Entity<ApplicationUser>().ToTable("Users");
                        
            //TODO 2: ***DONE***  Build all databases here
        }

        //public DbSet<ApplicationUser> Users { get; set; }
        

        
    }
}