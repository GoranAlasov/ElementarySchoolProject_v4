using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
using ElementarySchoolProject.Models;
using ElementarySchoolProject.Models.Users.UserDTOs;
using ElementarySchoolProject.Repositories;
using Microsoft.AspNet.Identity;
using ElementarySchoolProject.Utilities;

namespace ElementarySchoolProject.Services.UsersService
{
    public class UsersService : IUsersService
    {
        IUnitOfWork db;

        public UsersService(IUnitOfWork db)
        {
            this.db = db;
        }

        public async Task<IdentityResult> RegisterAdmin(RegisterUserDTO user)
        {
            Admin userToInsert = UserToUserDTOConverters.RegisterUserDTOtoAdmin(user);

            return await db.AuthRepository.RegisterAdmin(userToInsert, user.Password);
        }

        public async Task<IdentityResult> RegisterTeacher(RegisterUserDTO user)
        {
            Teacher userToInsert = UserToUserDTOConverters.RegisterUserDTOtoTeacher(user);

            return await db.AuthRepository.RegisterTeacher(userToInsert, user.Password);
        }

        public async Task<IdentityResult> RegisterParent(RegisterUserDTO user)
        {
            Parent userToInsert = UserToUserDTOConverters.RegisterUserDTOtoParent(user);

            return await db.AuthRepository.RegisterParent(userToInsert, user.Password);
        }

        public async Task<IdentityResult> RegisterStudent(RegisterStudentDTO user)
        {
            Student userToInsert = UserToUserDTOConverters.RegisterStudentDTOtoStudent(user);

            return await db.AuthRepository.RegisterStudent(userToInsert, user.Password);
        }

        public async Task<IEnumerable<UserViewWithRoleIds>> GetAllUsers()
        {
            IEnumerable<ApplicationUser> users = await db.AuthRepository.GetAllUsers();
            IEnumerable<UserViewWithRoleIds> retVal = users.Select(x => UserToUserDTOConverters.UserToUserViewWithRoleIds(x));

            return retVal;
        }

        public async Task<UserViewWithRoleIds> GetUserById(string id)
        {
            ApplicationUser user = await db.AuthRepository.FindUserById(id);

            UserViewWithRoleIds retVal = UserToUserDTOConverters.UserToUserViewWithRoleIds(user);

            return retVal;
        }

        public IEnumerable<UserSimpleViewDTO> GetAllAdmins()
        {
            IEnumerable<ApplicationUser> users = db.AdminsRepository.Get();
            IEnumerable<UserSimpleViewDTO> retVal = users.Select(x => UserToUserDTOConverters.UserToUserSimpleViewDTO(x));

            return retVal;
        }

        public IEnumerable<UserSimpleViewDTO> GetAllTeachers()
        {
            IEnumerable<ApplicationUser> users = db.TeachersRepository.Get();
            IEnumerable<UserSimpleViewDTO> retVal = users.Select(x => UserToUserDTOConverters.UserToUserSimpleViewDTO(x));

            return retVal;
        }

        public IEnumerable<ParentSimpleViewDTO> GetAllParents()
        {
            IEnumerable<Parent> users = db.ParentsRepository.Get();
            IEnumerable<ParentSimpleViewDTO> retVal = users.Select(x => UserToUserDTOConverters.ParentToParentSimpleViewDTO(x));

            return retVal;
        }

        public IEnumerable<UserSimpleViewDTO> GetAllStudents()
        {
            IEnumerable<ApplicationUser> users = db.StudentsRepository.Get();
            IEnumerable<UserSimpleViewDTO> retVal = users.Select(x => UserToUserDTOConverters.UserToUserSimpleViewDTO(x));

            return retVal;
        }

        public UserSimpleViewDTO GetAdminById(string id)
        {
            Admin admin = db.AdminsRepository.Get(user => user.Id == id).FirstOrDefault();
            
            if (admin == null)
            {
                return null;
            }            

            UserSimpleViewDTO retVal = UserToUserDTOConverters.UserToUserSimpleViewDTO(admin);

            EmailSenders.TestSendEmail(retVal.Email);

            return retVal;
        }

        public UserSimpleViewDTO GetTeacherById(string id)
        {
            Teacher teacher = db.TeachersRepository.Get(user => user.Id == id).FirstOrDefault();

            if (teacher == null)
            {
                return null;
            }

            UserSimpleViewDTO retVal = UserToUserDTOConverters.UserToUserSimpleViewDTO(teacher);

            return retVal;
        }

        public ParentSimpleViewDTO GetParentById(string id)
        {
            Parent parent = db.ParentsRepository.Get(p => p.Id == id).FirstOrDefault();

            if (parent == null)
            {
                return null;
            }

            ParentSimpleViewDTO retVal = UserToUserDTOConverters.ParentToParentSimpleViewDTO(parent);

            return retVal;
        }

        public UserSimpleViewDTO GetStudentById(string id)
        {
            Student student = db.StudentsRepository.Get(s => s.Id == id).FirstOrDefault();

            if (student == null)
            {
                return null;
            }

            UserSimpleViewDTO retVal = UserToUserDTOConverters.UserToUserSimpleViewDTO(student);

            return retVal;
        }
    }
}