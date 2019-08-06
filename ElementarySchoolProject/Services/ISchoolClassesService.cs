using ElementarySchoolProject.Models;
using ElementarySchoolProject.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementarySchoolProject.Services
{
    public interface ISchoolClassesService
    {
        IEnumerable<SchoolClassDTO> GetAll();
        SchoolClassDetailsDTO GetById(int id);
        IEnumerable<SchoolClassDTO> GetBySchoolGrade(int grade);
        
        SchoolClassDTO CreateSchoolClass(SchoolClassCreateAndEditDTO dto);
        SchoolClassDTO EditSchoolClass(int id, SchoolClassCreateAndEditDTO dto);
        SchoolClassDTO DeleteSchoolClass(int id);
    }
}
