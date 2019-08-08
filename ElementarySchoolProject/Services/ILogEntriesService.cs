using ElementarySchoolProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementarySchoolProject.Services
{
    public interface ILogEntriesService
    {
        IEnumerable<LogEntry> GetAll();
        LogEntry GetById(int id);

        //download
    }
}
