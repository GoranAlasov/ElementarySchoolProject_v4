using ElementarySchoolProject.Models;
using ElementarySchoolProject.Models.Users.UserDTOs;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementarySchoolProject.Services.UsersServices
{
    public interface IUsersService
    {        
        Task<IdentityResult> RegisterAdmin(RegisterUserDTO user);
        Task<IdentityResult> RegisterTeacher(RegisterUserDTO user);
        Task<IdentityResult> RegisterParent(RegisterUserDTO user);
        Task<IdentityResult> RegisterStudent(RegisterStudentDTO user);

        Task<IEnumerable<UserViewWithRoleIds>> GetAllUsers();
        Task<UserViewWithRoleIds> GetUserById(string id);

        IEnumerable<UserSimpleViewDTO> GetAllAdmins();
        IEnumerable<UserSimpleViewDTO> GetAllTeachers();
        IEnumerable<ParentSimpleViewDTO> GetAllParents();
        IEnumerable<StudentSimpleViewDTO> GetAllStudents();

        UserSimpleViewDTO GetAdminById(string id);
        UserSimpleViewDTO GetTeacherById(string id);
        ParentSimpleViewDTO GetParentById(string id);
        StudentSimpleViewDTO GetStudentById(string id);

        UserSimpleViewDTO DeleteAdmin(string id);
        UserSimpleViewDTO DeleteTeacher(string id);
        UserSimpleViewDTO DeleteParent(string id);
        UserSimpleViewDTO DeleteStudent(string id);
    }
}
