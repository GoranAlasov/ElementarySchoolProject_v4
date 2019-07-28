using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ElementarySchoolProject.Models;
using Unity;

namespace ElementarySchoolProject.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private DbContext context;

        public UnitOfWork(DbContext context)
        {
            this.context = context;
        }

        #region UserRepos

        [Dependency]
        public IAuthRepository AuthRepository { get; set; }

        [Dependency]
        public IGenericRepository<ApplicationUser> UsersRepository { get; set; }

        [Dependency]
        public IGenericRepository<Admin> AdminsRepository { get; set; }

        [Dependency]
        public IGenericRepository<Teacher> TeachersRepository { get; set; }

        [Dependency]
        public IGenericRepository<Parent> ParentsRepository { get; set; }

        [Dependency]
        public IGenericRepository<Student> StudentsRepository { get; set; }

        #endregion

        #region OtherRepos

        #endregion

        #region SaveAndDispose

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}