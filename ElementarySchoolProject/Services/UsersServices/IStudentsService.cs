using ElementarySchoolProject.Models.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementarySchoolProject.Services.UsersServices
{
    public interface IStudentsService
    {
        IEnumerable<StudentWithParentDTO> GetAll();
        StudentWithParentGradesClassDTO GetById(string id);
        StudentWithParentGradesClassDTO GetByIdAndParentUserName(string parentId, string childId);
        IEnumerable<StudentWithParentDTO> GetAllByParentId(string parentId);
        IEnumerable<StudentWithParentDTO> GetAllBySchoolClassId(int id);
        IEnumerable<StudentWithParentDTO> GetAllBySchoolClassGrade(int grade);
        IEnumerable<StudentWithParentDTO> GetAllByTeacherId(string id);
        IEnumerable<StudentWithParentDTO> GetAllBySchoolClassGradeAndTeacherId(int grade, string id);
        IEnumerable<StudentWithParentDTO> GetAllBySchoolSubjectId(int id);
        IEnumerable<StudentWithParentDTO> GetAllByTeacherSchoolSubjectId(int id);        
        IEnumerable<StudentWithParentDTO> GetAllByGradeDates(DateTime minValue, DateTime maxValue);
    }
}

