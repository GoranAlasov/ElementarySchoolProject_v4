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
        GradeDTO GetAll();
        GradeDTO GetById(int id);
        GradeDTO CreateGrade(GradeCreateAndEditDTO dto);
        GradeDTO EditGrade(int id, GradeCreateAndEditDTO dto);
        GradeDTO DeleteGrade(int id);
    }
}
