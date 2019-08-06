using ElementarySchoolProject.Models;
using ElementarySchoolProject.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementarySchoolProject.Services
{
    public interface ITeacherSchoolSubjectsService
    {
        IEnumerable<TeacherSchoolSubjectDTO> GetAll();
        TeacherSchoolSubjectDTO GetById(int id);
        TeacherSchoolSubjectDTO CreateTeacherSchoolSubject(TeacherSchoolSubjectCreateAndEditDTO dto);

    }
}
