using ElementarySchoolProject.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementarySchoolProject.Services
{ 
    public interface ISchoolSubjectsService
    {
        IEnumerable<SchoolSubjectWithWeeklyClassesAndTeachersDTO> GetAll();
        IEnumerable<SchoolSubjectWithWeeklyClassesAndTeachersDTO> GetAllByTeacherId(string teacherId);

        SchoolSubjectWithWeeklyClassesAndTeachersDTO GetById(int id);
        SchoolSubjectWithWeeklyClassesDTO CreateSchoolSubject(SchoolSubjectCreateAndEditDTO dto);
        SchoolSubjectWithWeeklyClassesDTO EditSchoolSubject(int id, SchoolSubjectCreateAndEditDTO dto);
        SchoolSubjectWithWeeklyClassesDTO DeleteSchoolSubject(int id);
    }
}
