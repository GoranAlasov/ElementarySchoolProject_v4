using ElementarySchoolProject.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementarySchoolProject.Repositories
{
    public interface IAuthRepository : IDisposable
    {
        Task<IdentityResult> RegisterAdmin(Admin admin, string password);

        Task<IdentityResult> RegisterTeacher(Teacher teacher, string password);        

        Task<IdentityResult> RegisterParent(Parent parent, string password);

        Task<IdentityResult> RegisterStudent(Student student, string password);

        Task<ApplicationUser> FindUser(string userName, string password);

        Task<ApplicationUser> FindUserById(string id);

        //void DeleteUser(string id);

        Task<IList<string>> FindRoles(string userId);

        Task<IList<ApplicationUser>> GetAllUsers();

        //Task<IList<ApplicationUser>> GetAllActiveUsers();
    }
}
