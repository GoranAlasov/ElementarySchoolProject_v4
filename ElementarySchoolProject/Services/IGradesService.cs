using ElementarySchoolProject.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementarySchoolProject.Services
{
    public interface IGradesService
    {
        IEnumerable<GradeDTO> GetAll();
        IEnumerable<GradeDTO> GetAllByTeacherId(string id);        
        IEnumerable<GradeDTO> GetAllByParentId(string id);
        IEnumerable<GradeDTO> GetAllByStudentId(string id);

        IEnumerable<GradeDTO> GetAllByStudentName(string firstName, string lastName);
        IEnumerable<GradeDTO> GetAllByStudentNameAndTeacherId(string firstName, string lastName, string teacherId);
        IEnumerable<GradeDTO> GetAllByStudentNameAndParentId(string firstName, string lastName, string parentId);

        IEnumerable<GradeDTO> GetAllByValueRange(int min, int max);
        IEnumerable<GradeDTO> GetAllByValueRangeAndTeacherId(int min, int max, string teacherId);
        IEnumerable<GradeDTO> GetAllByValueRangeAndParentId(int min, int max, string parentId);
        IEnumerable<GradeDTO> GetAllByValueRangeAndStudentId(int min, int max, string studentId);

        IEnumerable<GradeDTO> GetAllByGradingDateRange(DateTime min, DateTime max);
        IEnumerable<GradeDTO> GetAllByGradignDateRangeAndTeacherId(DateTime min, DateTime max, string teacherId);
        IEnumerable<GradeDTO> GetAllByGradingDateRangeAndParentId(DateTime min, DateTime max, string parentId);
        IEnumerable<GradeDTO> GetAllByGradingDateRangeAndStudentId(DateTime min, DateTime max, string studentId);

        IEnumerable<GradeDTO> GetAllBySchoolSubjectId(int id);
        IEnumerable<GradeDTO> GetAllBySchoolSubjectIdAndTeacherId(int subjectId, string teacherId);
        IEnumerable<GradeDTO> GetAllBySchoolSubjectIdAndParentId(int subjectId, string parentId);
        IEnumerable<GradeDTO> GetAllBySchoolSubjectIdAndStudentId(int subjectId, string studentId);
        
        IEnumerable<GradeDTO> GetAllByTeacherIdAndStudentId(string teacherId, string studentId);

        AverageGradeDTO AverageGradeByStudentId(string id);
        AverageGradeDTO AverageGradeByStudentIdAndSubjectId(string studentId, int subjectId);

        GradeDTO GetById(int id);
        GradeDTO CreateGrade(string teacherId, GradeCreateAndEditDTO dto);     
        GradeDTO EditGrade(int id, string teacherId, GradeCreateAndEditDTO dto);
        GradeDTO DeleteGrade(int id);
    }
}
