using ElementarySchoolProject.Models;
using ElementarySchoolProject.Models.Users.UserDTOs;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementarySchoolProject.Services.UsersService
{
    public interface IUsersService
    {        
        Task<IdentityResult> RegisterAdmin(RegisterUserDTO user);
        Task<IdentityResult> RegisterTeacher(RegisterUserDTO user);
        Task<IdentityResult> RegisterParent(RegisterUserDTO user);
        Task<IdentityResult> RegisterStudent(RegisterUserDTO user);

        Task<IList<ApplicationUser>> GetAllUsers();
        Task<ApplicationUser> GetUserById(string id);

        IEnumerable<Admin> GetAllAdmins();
        IEnumerable<Teacher> GetAllTeachers();
        IEnumerable<Parent> GetAllParents();
        IEnumerable<Student> GetAllStudents();
        Admin GetAdminById(string id);
        Teacher GetTeacherById(string id);
        Parent GetParentById(string id);
        Student GetStudentById(string id);

        //Task<IdentityResult> GetUserById(string Id);
    }
}
