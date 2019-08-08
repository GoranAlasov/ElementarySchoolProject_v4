using ElementarySchoolProject.Infrastructure;
using ElementarySchoolProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ElementarySchoolProject.Repositories
{
    public class AuthRepository : IAuthRepository, IDisposable
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private UserManager<ApplicationUser> _userManager;

        public AuthRepository(DbContext context)
        {            
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
        }        

        public async Task<IdentityResult> EditAdmin(Admin admin)
        {
            var result = await _userManager.UpdateAsync(admin);
            

            return result;
        }

        public async Task<IdentityResult> EditTeacher(Teacher teacher)
        {
            var result = await _userManager.UpdateAsync(teacher);

            return result;
        }

        public async Task<IdentityResult> EditParent(Parent parent)
        {
            var result = await _userManager.UpdateAsync(parent);

            return result;
        }

        public async Task<IdentityResult> EditStudent(Student student)
        {
            var result = await _userManager.UpdateAsync(student);

            return result;
        }

        public async Task<IdentityResult> RegisterAdmin(Admin admin, string password)
        {
            var result = await _userManager.CreateAsync(admin, password);
            _userManager.AddToRole(admin.Id, "admin");
            return result;
        }

        public async Task<IdentityResult> RegisterTeacher(Teacher teacher, string password)
        {
            var result = await _userManager.CreateAsync(teacher, password);
            _userManager.AddToRole(teacher.Id, "teacher");
            return result;
        }

        public async Task<IdentityResult> RegisterParent(Parent parent, string password)
        {
            var result = await _userManager.CreateAsync(parent, password);
            _userManager.AddToRole(parent.Id, "parent");
            return result;
        }

        public async Task<IdentityResult> RegisterStudent(Student student, string password)
        {
            var result = await _userManager.CreateAsync(student, password);
            _userManager.AddToRole(student.Id, "student");
            return result;
        }

        public async Task<ApplicationUser> FindUser(string userName, string password)
        {
            ApplicationUser user = await _userManager.FindAsync(userName, password);
            
            return user;
        }

        public async Task<ApplicationUser> FindUserById(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);            

            return user;
        }
        
        public async Task<IList<ApplicationUser>> GetAllUsers()
        {
            List<ApplicationUser> users = await _userManager.Users.ToListAsync();

            return users;
        }        

        public async Task<IList<string>> FindRoles(string userId)
        {
            return await _userManager.GetRolesAsync(userId);
        }

        public void Dispose()
        {
            if (_userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }
        }        
    }
}