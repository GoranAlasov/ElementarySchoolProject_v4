using ElementarySchoolProject.Models;
using ElementarySchoolProject.Models.DTOs.UserDTOs;
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

        Task<IdentityResult> EditAdmin(string id, EditUserDTO user);
        Task<IdentityResult> EditTeacher(string id, EditUserDTO user);
        Task<IdentityResult> EditParent(string id, EditUserDTO user);
        Task<IdentityResult> EditStudent(string id, EditUserDTO user);
        StudentWithParentDTO ChangeParent(string studentId, string parentId);
        StudentWithParentDTO AddStudentToClass(string studentId, int classId);

        Task<IEnumerable<UserViewWithRoleIdsDTO>> GetAllUsers();
        Task<UserViewWithRoleIdsDTO> GetUserById(string id);

        IEnumerable<UserSimpleViewDTO> GetAllAdmins();
        IEnumerable<UserSimpleViewDTO> GetAllTeachers();
        IEnumerable<ParentSimpleViewDTO> GetAllParents();
        IEnumerable<StudentWithParentDTO> GetAllStudents();

        UserSimpleViewDTO GetAdminById(string id);
        UserSimpleViewDTO GetTeacherById(string id);
        ParentSimpleViewDTO GetParentById(string id);
        StudentWithParentDTO GetStudentById(string id);

        UserSimpleViewDTO DeleteAdmin(string id);
        UserSimpleViewDTO DeleteTeacher(string id);
        UserSimpleViewDTO DeleteParent(string id);
        UserSimpleViewDTO DeleteStudent(string id);
    }
}
