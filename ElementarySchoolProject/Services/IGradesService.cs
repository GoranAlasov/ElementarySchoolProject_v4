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
        IEnumerable<GradeDTO> GetAllByValueRange(int min, int max);
        IEnumerable<GradeDTO> GetAllByGradingDateRange(DateTime min, DateTime max);
        IEnumerable<GradeDTO> GetAllBySchoolSubjectId(int id);
        IEnumerable<GradeDTO> GetAllByTeacherId(string id);
        IEnumerable<GradeDTO> GetAllByStudentId(string id);
        IEnumerable<GradeDTO> GetAllByTeacherIdAndStudentId(string teacherId, string studentId);

        GradeDTO GetById(int id);
        GradeDTO CreateGrade(string teacherId, GradeCreateAndEditDTO dto);     
        GradeDTO EditGrade(int id, string teacherId, GradeCreateAndEditDTO dto);
        GradeDTO DeleteGrade(int id);
    }
}
