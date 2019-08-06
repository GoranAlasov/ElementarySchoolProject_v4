using ElementarySchoolProject.Repositories;
using ElementarySchoolProject.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementarySchoolProject.Services
{
    public interface ISchoolClassTeacherSchoolSubjectService
    {
        IEnumerable<SchoolClassTeacherSchoolSubjectDTO> GetAll();
        SchoolClassTeacherSchoolSubjectDTO GetById(int id);

        SchoolClassTeacherSchoolSubjectDTO CreateSchoolClassTeacherSchoolSubject(SchoolClassTeacherSchoolSubjectCreateAndEditDTO dto);
        SchoolClassTeacherSchoolSubjectDTO EditSchoolClassTeacherSchoolSubject(int id, SchoolClassTeacherSchoolSubjectCreateAndEditDTO dto);
        SchoolClassTeacherSchoolSubjectDTO DeleteSchoolClassTeacherSchoolSubject(int id);
    }
}
