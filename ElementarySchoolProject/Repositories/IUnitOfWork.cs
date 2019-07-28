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
        #region UserRepos

        IAuthRepository AuthRepository { get; }
                
        IGenericRepository<Admin> AdminsRepository { get; }
        IGenericRepository<Teacher> TeachersRepository { get; }
        IGenericRepository<Parent> ParentsRepository { get; }
        IGenericRepository<Student> StudentsRepository { get; }

        IGenericRepository<ApplicationUser> UsersRepository { get; }

        #endregion

        #region SchoolRepos

        //TODO 6: Add and implement all other repositories

        #endregion

        void Save();
    }
}
