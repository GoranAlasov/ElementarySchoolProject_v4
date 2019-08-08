using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ElementarySchoolProject.Models;
using ElementarySchoolProject.Repositories;

namespace ElementarySchoolProject.Services
{
    public class LogEntriesService : ILogEntriesService
    {
        IUnitOfWork db;
        public LogEntriesService(IUnitOfWork db)
        {
            this.db = db;
        }


        public IEnumerable<LogEntry> GetAll()
        {
            var retVal = db.LogEntriesRepository.Get();

            return retVal;
        }

        public LogEntry GetById(int id)
        {
            var retVal = db.LogEntriesRepository.GetByID(id);

            return retVal;
        }
    }
}