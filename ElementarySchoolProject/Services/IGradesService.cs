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
        GradeDTO GetById(int id);
        GradeDTO CreateGrade(string teacherId, GradeCreateAndEditDTO dto);     
        GradeDTO EditGrade(int id, GradeCreateAndEditDTO dto);
        GradeDTO DeleteGrade(int id);
    }
}
