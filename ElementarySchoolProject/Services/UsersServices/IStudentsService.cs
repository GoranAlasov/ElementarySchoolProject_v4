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
    }
}
