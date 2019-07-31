using ElementarySchoolProject.Models.Users.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementarySchoolProject.Services.UsersServices
{
    public interface IStudentsService
    {
        IEnumerable<StudentSimpleViewDTO> GetAll();
        StudentWithGradesView GetById(string id);
        StudentWithGradesView GetByIdAndParentUserName(string parentId, string childId);
        IEnumerable<StudentSimpleViewDTO> GetByParentId(string parentId);
        IEnumerable<StudentSimpleViewDTO> GetAllBySchoolClassId(int id);
        IEnumerable<StudentSimpleViewDTO> GetAllBySchoolClassGrade(int grade);
        IEnumerable<StudentSimpleViewDTO> GetAllByTeacherId(string id);
        IEnumerable<StudentSimpleViewDTO> GetAllBySchoolClassGradeAndTeacherId(int grade, string id);
        IEnumerable<StudentSimpleViewDTO> GetAllBySchoolSubjectId(int id);
        IEnumerable<StudentSimpleViewDTO> GetAllByTeacherSchoolSubjectId(int id);
        //IEnumerable<StudentSimpleViewDTO> GetAllByGradeValues(double minValue, double maxValue);
        IEnumerable<StudentSimpleViewDTO> GetAllByGradeDates(DateTime minValue, DateTime maxValue);
    }
}

