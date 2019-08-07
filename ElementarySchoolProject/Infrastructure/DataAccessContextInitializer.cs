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

            context.SaveChanges();

            #region AddingSchoolSubjects

            SchoolSubject geography5 = new SchoolSubject()
            {
                Id = 1,
                Name = "Geografija 5",
                WeeklyClasses = 3
            };
            context.SchoolSubjects.Add(geography5);

            SchoolSubject geography6 = new SchoolSubject()
            {
                Id = 2,
                Name = "Geografija 6",
                WeeklyClasses = 4
            };
            context.SchoolSubjects.Add(geography6);

            SchoolSubject biology5 = new SchoolSubject()
            {
                Id = 3,
                Name = "Biologija 5",
                WeeklyClasses = 2,                
            };
            context.SchoolSubjects.Add(biology5);

            SchoolSubject biology6 = new SchoolSubject()
            {
                Id = 4,
                Name = "Biologija 6",
                WeeklyClasses = 3
            };
            context.SchoolSubjects.Add(biology6);

            SchoolSubject serbian5 = new SchoolSubject()
            {
                Id = 5,
                Name = "Srpski 5",
                WeeklyClasses = 4
            };
            context.SchoolSubjects.Add(serbian5);

            SchoolSubject serbian6 = new SchoolSubject()
            {
                Id = 6,
                Name = "Srpski 6",
                WeeklyClasses = 4
            };
            context.SchoolSubjects.Add(serbian6);            

            #endregion

            context.SaveChanges();

            #region AddingSchoolClasses

            SchoolClass petiA = new SchoolClass()
            {
                Id = 1,
                SchoolGrade = 5,
                Name = "A"
            };
            context.SchoolClasses.Add(petiA);

            SchoolClass petiB = new SchoolClass()
            {
                Id = 2,
                SchoolGrade = 5,
                Name = "B"
            };
            context.SchoolClasses.Add(petiB);

            SchoolClass sestiA = new SchoolClass()
            {
                Id = 3,
                SchoolGrade = 6,
                Name = "A"
            };
            context.SchoolClasses.Add(sestiA);

            SchoolClass sestiB = new SchoolClass()
            {
                Id = 4,
                SchoolGrade = 6,
                Name = "B"
            };
            context.SchoolClasses.Add(sestiB);

            #endregion            

            context.SaveChanges();

            using (var userStore = new UserStore<ApplicationUser>(context))
            {
                using (var userManager = new UserManager<ApplicationUser>(userStore))
                {
                    #region AddingAdmins

                    Admin admin1 = new Admin()
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

                    Teacher peca = new Teacher()
                    {
                        FirstName = "Petar",
                        LastName = "Stojakovic",
                        UserName = "peca",
                        Email = "peca@mail.com",                        
                    };
                    userManager.Create(peca, "pecaa1");
                    userManager.AddToRole(peca.Id, "teacher");

                    Teacher eugen = new Teacher()
                    {
                        FirstName = "Eugen",
                        LastName = "Plancak",
                        UserName = "eugen_plancak",
                        Email = "eugen@mail.com",
                    };
                    userManager.Create(eugen, "eugen1");
                    userManager.AddToRole(eugen.Id, "teacher");

                    Teacher kolarov = new Teacher()
                    {
                        FirstName = "Milos",
                        LastName = "Kolarov",
                        UserName = "milos_kolarov",
                        Email = "milos_kolarov@mail.com",
                    };
                    userManager.Create(kolarov, "kolarov1");
                    userManager.AddToRole(kolarov.Id, "teacher");

                    Teacher obrad = new Teacher()
                    {
                        FirstName = "Obrad",
                        LastName = "Stojkovic",
                        UserName = "obrad",
                        Email = "obrad@mail.com",
                    };
                    userManager.Create(obrad, "obrad1");
                    userManager.AddToRole(obrad.Id, "teacher");

                    #endregion

                    #region AddingParents

                    Parent parent1 = new Parent()
                    {
                        FirstName = "Gordana",
                        LastName = "Alasov",
                        UserName = "gordana_alasov",
                        Email = "gordana_alasov@mail.com",
                    };
                    userManager.Create(parent1, "gordana1");
                    userManager.AddToRole(parent1.Id, "parent");

                    Parent parent2 = new Parent()
                    {
                        FirstName = "Vidosava",
                        LastName = "Maodus",
                        UserName = "vida_maodus",
                        Email = "vida_maodus@mail.com",
                    };
                    userManager.Create(parent2, "vidaa1");
                    userManager.AddToRole(parent2.Id, "parent");

                    Parent parent3 = new Parent()
                    {
                        FirstName = "Djordje",
                        LastName = "Atanackovic",
                        UserName = "djordje_atanackovic",
                        Email = "djordje_atanackovic@mail.com",
                    };
                    userManager.Create(parent3, "djole1");
                    userManager.AddToRole(parent3.Id, "parent");

                    Parent parent4 = new Parent()
                    {
                        FirstName = "Snezana",
                        LastName = "Stojsic",
                        UserName = "sneza_stojsic",
                        Email = "sneza_stojsic@mail.com",
                    };
                    userManager.Create(parent4, "sneza1");
                    userManager.AddToRole(parent4.Id, "parent");

                    Parent parent5 = new Parent()
                    {
                        FirstName = "Tatjana",
                        LastName = "Lekic",
                        UserName = "tanja_lekic",
                        Email = "tanja_lekic@mail.com",
                    };
                    userManager.Create(parent5, "tanja1");
                    userManager.AddToRole(parent5.Id, "parent");

                    Parent parent6 = new Parent()
                    {
                        FirstName = "Macone",
                        LastName = "Nedeljkov",
                        UserName = "maca_nedeljkov",
                        Email = "maca_nedeljkov@mail.com",
                    };
                    userManager.Create(parent6, "macaa1");
                    userManager.AddToRole(parent6.Id, "parent");

                    Parent parent7 = new Parent()
                    {
                        FirstName = "Mile",
                        LastName = "Etinski",
                        UserName = "mile_etinski",
                        Email = "alasov.jr@gmail.com",
                    };
                    userManager.Create(parent7, "milee1");
                    userManager.AddToRole(parent7.Id, "parent");

                    #endregion                    

                    #region AddingStudents

                    Student goran = new Student()
                    {
                        FirstName = "Goran",
                        LastName = "Alasov",
                        UserName = "goran_alasov",
                        Email = "alasov.jr@mail.com",
                        Parent = parent1,
                        SchoolClass = sestiA,                        
                    };                    
                    
                    userManager.Create(goran, "alasov1");
                    userManager.AddToRole(goran.Id, "student");

                    Student mao = new Student()
                    {
                        FirstName = "Milan",
                        LastName = "Maodus",
                        UserName = "milan_maodus",
                        Email = "milan_maodus@mail.com",
                        Parent = parent2,
                        SchoolClass = sestiA
                    };
                    userManager.Create(mao, "maodus1");
                    userManager.AddToRole(mao.Id, "student");

                    Student roki = new Student()
                    {
                        FirstName = "Nenad",
                        LastName = "Maodus",
                        UserName = "nenad_maodus",
                        Email = "nenad_maodus@mail.com",
                        Parent = parent2,
                        SchoolClass = petiB
                    };
                    userManager.Create(roki, "rokii1");
                    userManager.AddToRole(roki.Id, "student");

                    Student facan = new Student()
                    {
                        FirstName = "Bojan",
                        LastName = "Atanackovic",
                        UserName = "bojan_atanackovic",
                        Email = "bojan_atanackovic@mail.com",
                        Parent = parent3,
                        SchoolClass = sestiA
                    };
                    userManager.Create(facan, "facan1");
                    userManager.AddToRole(facan.Id, "student");

                    Student jebac = new Student()
                    {
                        FirstName = "Aleksandar",
                        LastName = "Atanackovic",
                        UserName = "aleksandar_atanackovic",
                        Email = "aleksandar_atanackovic@mail.com",
                        Parent = parent3,
                        SchoolClass = petiA
                    };
                    userManager.Create(jebac, "jebac1");
                    userManager.AddToRole(jebac.Id, "student");

                    Student doca = new Student()
                    {
                        FirstName = "Vladimir",
                        LastName = "Stojsic",
                        UserName = "vladimir_stojsic",
                        Email = "vladimir_stojsic@mail.com",
                        Parent = parent4,
                        SchoolClass = sestiB
                    };
                    userManager.Create(doca, "doktor1");
                    userManager.AddToRole(doca.Id, "student");

                    Student bogdan = new Student()
                    {
                        FirstName = "Bogdan",
                        LastName = "Stojsic",
                        UserName = "bogdan_stojsic",
                        Email = "bogdan_stojsic@mail.com",
                        Parent = parent4,
                        SchoolClass = petiA
                    };
                    userManager.Create(bogdan, "bogdan1");
                    userManager.AddToRole(bogdan.Id, "student");

                    Student sveta = new Student()
                    {
                        FirstName = "Svetozar",
                        LastName = "Lekic",
                        UserName = "sveta_lekic",
                        Email = "svetozar_lekic@mail.com",
                        Parent = parent5,
                        SchoolClass = sestiB
                    };
                    userManager.Create(sveta, "sveta1");
                    userManager.AddToRole(sveta.Id, "student");

                    Student seba = new Student()
                    {
                        FirstName = "Nebojsa",
                        LastName = "Nedeljkov",
                        UserName = "nebojsa_nedeljkov",
                        Email = "nebojsa_nedeljkov@mail.com",
                        Parent = parent6,
                        SchoolClass = sestiB
                    };
                    userManager.Create(seba, "nebojsa1");
                    userManager.AddToRole(seba.Id, "student");

                    Student tanatelo = new Student()
                    {
                        FirstName = "Marko",
                        LastName = "Nedeljkov",
                        UserName = "marko_nedeljkov",
                        Email = "marko_nedeljkov@mail.com",
                        Parent = parent6,
                        SchoolClass = petiB
                    };
                    userManager.Create(tanatelo, "marko1");
                    userManager.AddToRole(tanatelo.Id, "student");

                    Student pista = new Student()
                    {
                        FirstName = "Dragan",
                        LastName = "Etinski",
                        UserName = "dragan_etinski",
                        Email = "dragan_etinski@mail.com",
                        Parent = parent7,
                        SchoolClass = petiB
                    };
                    userManager.Create(pista, "pista1");
                    userManager.AddToRole(pista.Id, "student");

                    #endregion

                    context.SaveChanges();                    

                    #region AddingTeacherSchoolSubjects

                    TeacherSchoolSubject pecaGeo5 = new TeacherSchoolSubject()
                    {
                        Id = 1,
                        Teacher = peca,
                        SchoolSubject = geography5                       
                    };
                    context.TeacherSchoolSubjects.Add(pecaGeo5);

                    TeacherSchoolSubject pecaGeo6 = new TeacherSchoolSubject()
                    {
                        Id = 2,
                        Teacher = peca,
                        SchoolSubject = geography6                        
                    };
                    context.TeacherSchoolSubjects.Add(pecaGeo6);

                    TeacherSchoolSubject eugenBio5 = new TeacherSchoolSubject()
                    {
                        Id = 3,
                        Teacher = eugen,
                        SchoolSubject = biology5                        
                    };
                    context.TeacherSchoolSubjects.Add(eugenBio5);

                    TeacherSchoolSubject eugenBio6 = new TeacherSchoolSubject()
                    {
                        Id = 4,
                        Teacher = eugen,
                        SchoolSubject = biology6
                    };
                    context.TeacherSchoolSubjects.Add(eugenBio6);

                    TeacherSchoolSubject kolarovSrp5 = new TeacherSchoolSubject()
                    {
                        Id = 5,
                        Teacher = kolarov,
                        SchoolSubject = serbian5
                    };
                    context.TeacherSchoolSubjects.Add(kolarovSrp5);

                    TeacherSchoolSubject obradSrp6 = new TeacherSchoolSubject()
                    {
                        Id = 6,
                        Teacher = obrad,
                        SchoolSubject = serbian6
                    };
                    context.TeacherSchoolSubjects.Add(obradSrp6);

                    #endregion

                    context.SaveChanges();

                    #region AddingSchoolClassTeacherSchoolSubjects

                    SchoolClassTeacherSchoolSubject pecaGeo5PetiA = new SchoolClassTeacherSchoolSubject()
                    {
                        TeacherSchoolSubject = pecaGeo5,
                        SchoolClass = petiA
                    };
                    context.SchoolClassTeacherSchoolSubjects.Add(pecaGeo5PetiA);

                    SchoolClassTeacherSchoolSubject pecaGeo5PetiB = new SchoolClassTeacherSchoolSubject()
                    {
                        TeacherSchoolSubject = pecaGeo5,
                        SchoolClass = petiB
                    };
                    context.SchoolClassTeacherSchoolSubjects.Add(pecaGeo5PetiB);

                    SchoolClassTeacherSchoolSubject pecaGeo6SestiA = new SchoolClassTeacherSchoolSubject()
                    {
                        TeacherSchoolSubject = pecaGeo6,
                        SchoolClass = sestiA
                    };
                    context.SchoolClassTeacherSchoolSubjects.Add(pecaGeo6SestiA);

                    SchoolClassTeacherSchoolSubject pecaGeo6SestiB = new SchoolClassTeacherSchoolSubject()
                    {
                        TeacherSchoolSubject = pecaGeo6,
                        SchoolClass = sestiB
                    };
                    context.SchoolClassTeacherSchoolSubjects.Add(pecaGeo6SestiB);

                    SchoolClassTeacherSchoolSubject eugenBio5PetiA = new SchoolClassTeacherSchoolSubject()
                    {
                        TeacherSchoolSubject = eugenBio5,
                        SchoolClass = petiA
                    };
                    context.SchoolClassTeacherSchoolSubjects.Add(eugenBio5PetiA);

                    SchoolClassTeacherSchoolSubject eugenBio5PetiB = new SchoolClassTeacherSchoolSubject()
                    {
                        TeacherSchoolSubject = eugenBio5,
                        SchoolClass = petiB
                    };
                    context.SchoolClassTeacherSchoolSubjects.Add(eugenBio5PetiB);

                    SchoolClassTeacherSchoolSubject eugenBio6SestiA = new SchoolClassTeacherSchoolSubject()
                    {
                        TeacherSchoolSubject = eugenBio6,
                        SchoolClass = sestiA
                    };
                    context.SchoolClassTeacherSchoolSubjects.Add(eugenBio6SestiA);

                    SchoolClassTeacherSchoolSubject eugenBio6SestiB = new SchoolClassTeacherSchoolSubject()
                    {
                        TeacherSchoolSubject = eugenBio6,
                        SchoolClass = sestiB
                    };
                    context.SchoolClassTeacherSchoolSubjects.Add(eugenBio6SestiB);

                    SchoolClassTeacherSchoolSubject kolarovSrp5PetiA = new SchoolClassTeacherSchoolSubject()
                    {
                        TeacherSchoolSubject = kolarovSrp5,
                        SchoolClass = petiA
                    };
                    context.SchoolClassTeacherSchoolSubjects.Add(kolarovSrp5PetiA);

                    SchoolClassTeacherSchoolSubject kolarovSrp5PetiB = new SchoolClassTeacherSchoolSubject()
                    {
                        TeacherSchoolSubject = kolarovSrp5,
                        SchoolClass = petiB
                    };
                    context.SchoolClassTeacherSchoolSubjects.Add(kolarovSrp5PetiB);

                    SchoolClassTeacherSchoolSubject obradSrp6SestiA = new SchoolClassTeacherSchoolSubject()
                    {
                        TeacherSchoolSubject = obradSrp6,
                        SchoolClass = sestiA
                    };
                    context.SchoolClassTeacherSchoolSubjects.Add(obradSrp6SestiA);

                    SchoolClassTeacherSchoolSubject obradSrp6SestiB = new SchoolClassTeacherSchoolSubject()
                    {
                        TeacherSchoolSubject = obradSrp6,
                        SchoolClass = sestiB
                    };
                    context.SchoolClassTeacherSchoolSubjects.Add(obradSrp6SestiB);

                    #endregion

                    Grade g1 = new Grade()
                    {
                        Id = 1,
                        Value = 4,
                        DateOfGrading = new DateTime(2000, 12, 21),
                        Student = goran,
                        SchoolClassTeacherSchoolSubject = pecaGeo6SestiA
                    };
                    context.Grades.Add(g1);

                    Grade g2 = new Grade()
                    {
                        Id = 2,
                        Value = 3,
                        DateOfGrading = new DateTime(1998, 5, 8),
                        Student = mao,
                        SchoolClassTeacherSchoolSubject = obradSrp6SestiA
                    };
                    context.Grades.Add(g2);

                    Grade g3 = new Grade()
                    {
                        Id = 2,
                        Value = 3,
                        DateOfGrading = new DateTime(1997, 1, 27),
                        Student = mao,
                        SchoolClassTeacherSchoolSubject = eugenBio6SestiA
                    };
                    context.Grades.Add(g3);

                    Grade g4 = new Grade()
                    {
                        Id = 2,
                        Value = 1,
                        DateOfGrading = new DateTime(1997, 9, 11),
                        Student = roki,
                        SchoolClassTeacherSchoolSubject = eugenBio5PetiB
                    };
                    context.Grades.Add(g4);

                    context.SaveChanges();
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