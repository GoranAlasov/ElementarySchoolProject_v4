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
        IEnumerable<StudentWithParentDTO> GetAllByTeacherId(string id);
        IEnumerable<StudentWithParentGradesClassDTO> GetAllByParentId(string parentId);

        StudentWithParentGradesClassDTO GetById(string id);
        StudentWithParentGradesClassDTO GetByIdAndParentId(string studentId, string parentId);
        StudentWithParentGradesClassDTO GetByIdAndTeacherId(string studentId, string teacherId);

        IEnumerable<StudentWithParentDTO> GetAllByGradeDates(DateTime minValue, DateTime maxValue);
        IEnumerable<StudentWithParentDTO> GetAllByGradeDatesAndTeacherId(DateTime minValue, DateTime maxValue, string teacherId);
                
        IEnumerable<StudentWithParentDTO> GetAllBySchoolClassGrade(int grade);
        IEnumerable<StudentWithParentDTO> GetAllBySchoolClassGradeAndTeacherId(int grade, string id);

        IEnumerable<StudentWithParentDTO> GetAllBySchoolClassId(int id);
        IEnumerable<StudentWithParentDTO> GetAllBySchoolClassIdAndTeacherId(int classId, string teacherId);

        IEnumerable<StudentWithParentDTO> GetAllByStudentName(string name);
        IEnumerable<StudentWithParentDTO> GetAllByStudentNameAndTeacherID(string name, string teacherId);

        IEnumerable<StudentWithParentDTO> GetAllByTeacherName(string teacherName);
        IEnumerable<StudentWithParentDTO> GetAllByTeacherNameAndTeacherId(string teacherName, string teacherId);

        IEnumerable<StudentWithParentDTO> GetAllBySchoolSubjectId(int id);
        IEnumerable<StudentWithParentDTO> GetAllBySchoolSubjectIdAndTeacherId(int subjectId, string teacherId);

        IEnumerable<StudentWithParentDTO> GetAllBySchoolSubjectName(string subjectName);
        IEnumerable<StudentWithParentDTO> GetAllBySchoolSubjectNameAndTeacherId(string subjectName, string teacherId);

        IEnumerable<StudentWithParentDTO> GetAllByTeacherSchoolSubjectId(int id);
        IEnumerable<StudentWithParentDTO> GetAllByTeacherSchoolSubjectIdAndTeacherId(int id, string teacherId);
    }
}

