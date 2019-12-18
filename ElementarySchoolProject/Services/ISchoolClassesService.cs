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
        IEnumerable<SchoolClassDTO> GetAllByTeacherId(string id);

        SchoolClassDetailsDTO GetById(int id);
        SchoolClassDetailsDTO GetByIdAndTeacherId(int id, string teacherId);
        
        IEnumerable<SchoolClassDTO> GetBySchoolGrade(int grade);
        IEnumerable<SchoolClassDTO> GetBySchoolGradeAndTeacherId(int grade, string teacherId);
        
        SchoolClassDTO CreateSchoolClass(SchoolClassCreateAndEditDTO dto);
        SchoolClassDTO EditSchoolClass(int id, SchoolClassCreateAndEditDTO dto);
        SchoolClassDTO DeleteSchoolClass(int id);
    }
}
