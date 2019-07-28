using ElementarySchoolProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ElementarySchoolProject.Infrastructure
{
    public class DataAccessContextInitializer : DropCreateDatabaseAlways<DataAccessContext>
    {
        public override void InitializeDatabase(DataAccessContext context)
        {
            context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction
                , string.Format("ALTER DATABASE {0} SET SINGLE_USER WITH ROLLBACK IMMEDIATE", context.Database.Connection.Database));

            base.InitializeDatabase(context);
        }

        private void SetAllRoles(List<ApplicationUser> users, DataAccessContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            foreach (var user in users)
            {
                if (user.GetType() == typeof(Admin))
                {
                    manager.AddToRole(user.Id, "admins");
                }
                else if (user.GetType() == typeof(Teacher))
                {
                    manager.AddToRole(user.Id, "teachers");
                }
                else if (user.GetType() == typeof(Parent))
                {
                    manager.AddToRole(user.Id, "parents");
                }
                else if (user.GetType() == typeof(Student))
                {
                    manager.AddToRole(user.Id, "students");
                }
            }            
        }

        protected override void Seed(DataAccessContext context)
        {
            

            //var user = new ApplicationUser()
            //{
            //    UserName = "SuperPowerUser",
            //    Email = "taiseer.joudeh@gmail.com",
            //    EmailConfirmed = true,
            //    FirstName = "Taiseer",
            //    LastName = "Joudeh",
            //    Level = 1,
            //    JoinDate = DateTime.Now.AddYears(-3)
            //};

            //manager.Create(user, "MySuperP@ss!");

            //if (roleManager.Roles.Count() == 0)
            //{
            //    roleManager.Create(new IdentityRole { Name = "SuperAdmin" });
            //    roleManager.Create(new IdentityRole { Name = "Admin" });
            //    roleManager.Create(new IdentityRole { Name = "User" });
            //}

            //var adminUser = manager.FindByName("SuperPowerUser");
            
            

            context.Roles.Add(new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "admins" });
            context.Roles.Add(new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "teachers" });
            context.Roles.Add(new IdentityRole() { Id = Guid.NewGuid().ToString(), Name= "parents" });
            context.Roles.Add(new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "students" });

            context.SaveChanges();

            List<ApplicationUser> allUsers = new List<ApplicationUser>();

            ApplicationUser admin1 = new Admin()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Chuck",
                LastName = "Norris",
                UserName = "chuckyboy",
                Email = "chuck@mail.com",
                PasswordHash = new PasswordHasher().HashPassword("qwerty")
            };

            ApplicationUser teacher1 = new Teacher()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Goran",
                LastName = "Alasov",
                UserName = "alas",
                Email = "Ala@gsag",
                PasswordHash = new PasswordHasher().HashPassword("qwerty")
            };

            allUsers.Add(admin1);
            allUsers.Add(teacher1);

            context.Users.Add(admin1);
            context.Users.Add(teacher1);
            
            context.SaveChanges();
            SetAllRoles(allUsers, context);

            
            context.SaveChanges();

            //Admin admin1 = new Admin()
            //{
            //    FirstName = "Goran",
            //    LastName = "Alasov",
            //    UserName = "alas",
            //    Email = "alas@mail.com"
            //};

            //Admin admin2 = new Admin()
            //{
            //    FirstName = "Goran1",
            //    LastName = "Alasov1",
            //    UserName = "alas2",
            //    Email = "alas@mail.com1"
            //};

            //admins.Add(admin1);
            //admins.Add(admin2);

            //context.SaveChanges();
        }
    }
}