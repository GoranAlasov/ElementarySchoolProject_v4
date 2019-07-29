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
        //This method closes the database after each run
        public override void InitializeDatabase(DataAccessContext context)
        {
            context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction
                , string.Format("ALTER DATABASE {0} SET SINGLE_USER WITH ROLLBACK IMMEDIATE", context.Database.Connection.Database));

            base.InitializeDatabase(context);
        }

        //private void SetAllRoles(List<ApplicationUser> users, DataAccessContext context)
        //{
        //    var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

        //    foreach (var user in users)
        //    {
        //        if (user.GetType() == typeof(Admin))
        //        {
        //            manager.AddToRole(user.Id, "admins");
        //        }
        //        else if (user.GetType() == typeof(Teacher))
        //        {
        //            manager.AddToRole(user.Id, "teachers");
        //        }
        //        else if (user.GetType() == typeof(Parent))
        //        {
        //            manager.AddToRole(user.Id, "parents");
        //        }
        //        else if (user.GetType() == typeof(Student))
        //        {
        //            manager.AddToRole(user.Id, "students");
        //        }
        //    }            
        //}

        protected override void Seed(DataAccessContext context)
        {
            #region AddingRoles

            using (var store = new RoleStore<IdentityRole>(context))
            {
                using (var manager = new RoleManager<IdentityRole>(store))
                {
                    manager.Create(new IdentityRole("admin"));
                    manager.Create(new IdentityRole("teacher"));
                    manager.Create(new IdentityRole("parent"));
                    manager.Create(new IdentityRole("student"));
                }
            }

            #endregion

            using (var userStore = new UserStore<ApplicationUser>(context))
            {
                using (var userManager = new UserManager<ApplicationUser>(userStore))
                {
                    #region AddingAdmins

                    ApplicationUser admin1 = new Admin()
                    {                        
                        FirstName = "Chuck",
                        LastName = "Norris",
                        UserName = "chuckyboy",
                        Email = "alasov.jr@gmail.com",                        
                    };
                    userManager.Create(admin1, "qwerty");
                    userManager.AddToRole(admin1.Id, "admin");

                    #endregion

                    #region AddingTeachers

                    ApplicationUser teacher1 = new Teacher()
                    {
                        FirstName = "Petar",
                        LastName = "Stojakovic",
                        UserName = "peca",
                        Email = "peca@mail.com",
                    };
                    userManager.Create(teacher1, "pecaa1");
                    userManager.AddToRole(teacher1.Id, "teacher");

                    ApplicationUser teacher2 = new Teacher()
                    {
                        FirstName = "Eugen",
                        LastName = "Plancak",
                        UserName = "eugen_plancak",
                        Email = "eugen@mail.com",
                    };
                    userManager.Create(teacher2, "eugen1");
                    userManager.AddToRole(teacher2.Id, "teacher");

                    ApplicationUser teacher3 = new Teacher()
                    {
                        FirstName = "Milos",
                        LastName = "Kolarov",
                        UserName = "milos_kolarov",
                        Email = "milos_kolarov@mail.com",
                    };
                    userManager.Create(teacher3, "kolarov1");
                    userManager.AddToRole(teacher3.Id, "teacher");

                    ApplicationUser teacher4 = new Teacher()
                    {
                        FirstName = "Obrad",
                        LastName = "Stojkovic",
                        UserName = "obrad",
                        Email = "obrad@mail.com",
                    };
                    userManager.Create(teacher4, "obrad1");
                    userManager.AddToRole(teacher4.Id, "teacher");

                    #endregion

                    #region AddingParents

                    ApplicationUser parent1 = new Parent()
                    {
                        FirstName = "Gordana",
                        LastName = "Alasov",
                        UserName = "gordana_alasov",
                        Email = "gordana_alasov@mail.com",
                    };
                    userManager.Create(parent1, "gordana1");
                    userManager.AddToRole(parent1.Id, "parent");

                    ApplicationUser parent2 = new Parent()
                    {
                        FirstName = "Vidosava",
                        LastName = "Maodus",
                        UserName = "vida_maodus",
                        Email = "vida_maodus@mail.com",
                    };
                    userManager.Create(parent2, "vidaa1");
                    userManager.AddToRole(parent2.Id, "parent");

                    ApplicationUser parent3 = new Parent()
                    {
                        FirstName = "Djordje",
                        LastName = "Atanackovic",
                        UserName = "djordje_atanackovic",
                        Email = "djordje_atanackovic@mail.com",
                    };
                    userManager.Create(parent3, "djole1");
                    userManager.AddToRole(parent3.Id, "parent");

                    ApplicationUser parent4 = new Parent()
                    {
                        FirstName = "Snezana",
                        LastName = "Stojsic",
                        UserName = "sneza_stojsic",
                        Email = "sneza_stojsic@mail.com",
                    };
                    userManager.Create(parent4, "sneza1");
                    userManager.AddToRole(parent4.Id, "parent");

                    ApplicationUser parent5 = new Parent()
                    {
                        FirstName = "Tatjana",
                        LastName = "Lekic",
                        UserName = "tanja_lekic",
                        Email = "tanja_lekic@mail.com",
                    };
                    userManager.Create(parent5, "tanja1");
                    userManager.AddToRole(parent5.Id, "parent");

                    ApplicationUser parent6 = new Parent()
                    {
                        FirstName = "Macone",
                        LastName = "Nedeljkov",
                        UserName = "maca_nedeljkov",
                        Email = "maca_nedeljkov@mail.com",
                    };
                    userManager.Create(parent6, "macaa1");
                    userManager.AddToRole(parent6.Id, "parent");

                    ApplicationUser parent7 = new Parent()
                    {
                        FirstName = "Mile",
                        LastName = "Etinski",
                        UserName = "mile_etinski",
                        Email = "mile_etinski@mail.com",
                    };
                    userManager.Create(parent7, "milee1");
                    userManager.AddToRole(parent7.Id, "parent");

                    #endregion

                    #region AddingStudents

                    ApplicationUser student1 = new Student()
                    {
                        FirstName = "Goran",
                        LastName = "Alasov",
                        UserName = "goran_alasov",
                        Email = "alasov.jr@mail.com",
                        ParentId = parent1.Id
                    };
                    userManager.Create(student1, "alasov1");
                    userManager.AddToRole(student1.Id, "student");

                    ApplicationUser student2 = new Student()
                    {
                        FirstName = "Milan",
                        LastName = "Maodus",
                        UserName = "milan_maodus",
                        Email = "milan_maodus@mail.com",
                        ParentId = parent2.Id
                    };
                    userManager.Create(student2, "maodus1");
                    userManager.AddToRole(student2.Id, "student");

                    ApplicationUser student3 = new Student()
                    {
                        FirstName = "Nenad",
                        LastName = "Maodus",
                        UserName = "nenad_maodus",
                        Email = "nenad_maodus@mail.com",
                        ParentId = parent2.Id
                    };
                    userManager.Create(student3, "rokii1");
                    userManager.AddToRole(student3.Id, "student");

                    ApplicationUser student4 = new Student()
                    {
                        FirstName = "Bojan",
                        LastName = "Atanackovic",
                        UserName = "bojan_atanackovic",
                        Email = "bojan_atanackovic@mail.com",
                        ParentId = parent3.Id
                    };
                    userManager.Create(student4, "facan1");
                    userManager.AddToRole(student4.Id, "student");

                    ApplicationUser student5 = new Student()
                    {
                        FirstName = "Aleksandar",
                        LastName = "Atanackovic",
                        UserName = "aleksandar_atanackovic",
                        Email = "aleksandar_atanackovic@mail.com",
                        ParentId = parent3.Id
                    };
                    userManager.Create(student5, "jebac1");
                    userManager.AddToRole(student5.Id, "student");

                    ApplicationUser student6 = new Student()
                    {
                        FirstName = "Vladimir",
                        LastName = "Stojsic",
                        UserName = "vladimir_stojsic",
                        Email = "vladimir_stojsic@mail.com",
                        ParentId = parent4.Id
                    };
                    userManager.Create(student6, "doktor1");
                    userManager.AddToRole(student6.Id, "student");

                    ApplicationUser student7 = new Student()
                    {
                        FirstName = "Bogdan",
                        LastName = "Stojsic",
                        UserName = "bogdan_stojsic",
                        Email = "bogdan_stojsic@mail.com",
                        ParentId = parent4.Id
                    };
                    userManager.Create(student7, "bogdan1");
                    userManager.AddToRole(student7.Id, "student");

                    ApplicationUser student8 = new Student()
                    {
                        FirstName = "Svetozar",
                        LastName = "Lekic",
                        UserName = "sveta_lekic",
                        Email = "svetozar_lekic@mail.com",
                        ParentId = parent5.Id
                    };
                    userManager.Create(student8, "sveta1");
                    userManager.AddToRole(student8.Id, "student");

                    ApplicationUser student9 = new Student()
                    {
                        FirstName = "Nebojsa",
                        LastName = "Nedeljkov",
                        UserName = "nebojsa_nedeljkov",
                        Email = "nebojsa_nedeljkov@mail.com",
                        ParentId = parent6.Id
                    };
                    userManager.Create(student9, "nebojsa1");
                    userManager.AddToRole(student9.Id, "student");

                    ApplicationUser student10 = new Student()
                    {
                        FirstName = "Marko",
                        LastName = "Nedeljkov",
                        UserName = "marko_nedeljkov",
                        Email = "marko_nedeljkov@mail.com",
                        ParentId = parent6.Id
                    };
                    userManager.Create(student10, "marko1");
                    userManager.AddToRole(student10.Id, "student");

                    ApplicationUser student11 = new Student()
                    {
                        FirstName = "Dragan",
                        LastName = "Etinski",
                        UserName = "dragan_etinski",
                        Email = "dragan_etinski@mail.com",
                        ParentId = parent7.Id
                    };
                    userManager.Create(student11, "pista1");
                    userManager.AddToRole(student11.Id, "student");

                    #endregion                                       
                }

            }
            
            



            //context.Roles.Add(new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "admins" });
            //context.Roles.Add(new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "teachers" });
            //context.Roles.Add(new IdentityRole() { Id = Guid.NewGuid().ToString(), Name= "parents" });
            //context.Roles.Add(new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "students" });

            //context.SaveChanges();

            //List<ApplicationUser> allUsers = new List<ApplicationUser>();                        

            //allUsers.Add(admin1);
            //allUsers.Add(teacher1);

            //context.Users.Add(admin1);
            //context.Users.Add(teacher1);

            //context.SaveChanges();
            //SetAllRoles(allUsers, context);


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