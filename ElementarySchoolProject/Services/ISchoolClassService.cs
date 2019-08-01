using ElementarySchoolProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementarySchoolProject.Services
{
    interface ISchoolClassService
    {
        IEnumerable<SchoolClass> GetAll();
        IEnumerable<SchoolClass> GetById();
    }
}
