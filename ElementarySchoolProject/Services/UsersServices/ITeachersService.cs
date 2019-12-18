using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElementarySchoolProject.Models.DTOs.UserDTOs;

namespace ElementarySchoolProject.Services.UsersServices
{
    public interface ITeachersService
    {
        IEnumerable<TeacherBasicDTO> GetAll();
        TeacherBasicDTO GetById(string id);

        IEnumerable<TeacherBasicDTO> GetAllTeachingASubject(int id);
        IEnumerable<TeacherBasicDTO> GetAllTeachingToAGrade(int grade);     
        IEnumerable<TeacherBasicDTO> GetAllTeachingToClass(int id);
        IEnumerable<TeacherBasicDTO> GetAllTeachingToStudent(string id);
        IEnumerable<TeacherBasicDTO> GetAllByName(string name);
        IEnumerable<TeacherBasicDTO> GetAllBySubjectName(string name);        
    }
}
