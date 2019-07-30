using ElementarySchoolProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementarySchoolProject.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IAuthRepository AuthRepository { get; }
                
        IGenericRepository<Admin> AdminsRepository { get; }
        IGenericRepository<Teacher> TeachersRepository { get; }
        IGenericRepository<Parent> ParentsRepository { get; }
        IGenericRepository<Student> StudentsRepository { get; }

        IGenericRepository<ApplicationUser> UsersRepository { get; }
         
        IGenericRepository<SchoolSubject> SchoolSubjectsRepository { get; }
        IGenericRepository<SchoolClass> SchoolClassesRepository { get; }
        IGenericRepository<TeacherSchoolSubject> TeacherSchoolSubjectSRepository { get; }
        IGenericRepository<Grade> GradesRepository { get; }

        //TODO 6: ***DONE*** Add and implement all other repositories

        

        void Save();
    }
}
