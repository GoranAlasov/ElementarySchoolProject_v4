using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
using ElementarySchoolProject.Models;
using ElementarySchoolProject.Repositories;
using Microsoft.AspNet.Identity;
using ElementarySchoolProject.Utilities;
using ElementarySchoolProject.Models.DTOs.UserDTOs;
using NLog;

namespace ElementarySchoolProject.Services.UsersServices
{
    public class UsersService : IUsersService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        IUnitOfWork db;

        public UsersService(IUnitOfWork db)
        {
            this.db = db;
        }

        #region RegisteringUsers

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

        #endregion

        #region GettingUsers

        public async Task<IEnumerable<UserViewWithRoleIdsDTO>> GetAllUsers()
        {
            IEnumerable<ApplicationUser> users = await db.AuthRepository.GetAllUsers();
            IEnumerable<UserViewWithRoleIdsDTO> retVal = users.Select(x => UserToUserDTOConverters.UserToUserViewWithRoleIds(x));

            return retVal;
        }

        public async Task<UserViewWithRoleIdsDTO> GetUserById(string id)
        {
            ApplicationUser user = await db.AuthRepository.FindUserById(id);

            UserViewWithRoleIdsDTO retVal = UserToUserDTOConverters.UserToUserViewWithRoleIds(user);

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

        public IEnumerable<StudentWithParentDTO> GetAllStudents()
        {
            IEnumerable<Student> users = db.StudentsRepository.Get();
            IEnumerable<StudentWithParentDTO> retVal = users.Select(x => UserToUserDTOConverters.StudentToStudentWithParentDTO(x));

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

        public StudentWithParentDTO GetStudentById(string id)
        {
            Student student = db.StudentsRepository.Get(s => s.Id == id).FirstOrDefault();
            
            if (student == null)
            {
                return null;
            }

            StudentWithParentDTO retVal = UserToUserDTOConverters.StudentToStudentWithParentDTO(student);

            return retVal;
        }

        #endregion

        #region DeletingUsers

        public UserSimpleViewDTO DeleteAdmin(string id)
        {
            Admin admin = db.AdminsRepository.Get(user => user.Id == id).FirstOrDefault();

            int adminCount = db.AdminsRepository.Get().Count();

            if (admin == null || adminCount < 2)
            {
                return null;
            }

            db.AdminsRepository.Delete(admin);
            db.Save();
            UserSimpleViewDTO retVal = UserToUserDTOConverters.UserToUserSimpleViewDTO(admin);

            return retVal;
        }

        public UserSimpleViewDTO DeleteTeacher(string id)
        {
            Teacher teacher = db.TeachersRepository.Get(user => user.Id == id).FirstOrDefault();

            if (teacher == null)
            {
                return null;
            }

            db.TeachersRepository.Delete(teacher);
            db.Save();
            UserSimpleViewDTO retVal = UserToUserDTOConverters.UserToUserSimpleViewDTO(teacher);

            return retVal;
        }

        public UserSimpleViewDTO DeleteParent(string id)
        {
            Parent parent = db.ParentsRepository.Get(user => user.Id == id).FirstOrDefault();
            
            IEnumerable<Student> students = db.StudentsRepository.Get(s => s.Parent.Id == parent.Id);

            if (parent == null)
            {
                return null;
            }

            foreach (var item in students)
            {
                item.Parent = null;
            }

            db.ParentsRepository.Delete(parent);
            db.Save();
            UserSimpleViewDTO retVal = UserToUserDTOConverters.UserToUserSimpleViewDTO(parent);

            return retVal;            
        }

        public UserSimpleViewDTO DeleteStudent(string id)
        {
            Student student = db.StudentsRepository.Get(user => user.Id == id).FirstOrDefault();

            if (student == null)
            {
                return null;
            }

            db.StudentsRepository.Delete(student);
            db.Save();
            UserSimpleViewDTO retVal = UserToUserDTOConverters.UserToUserSimpleViewDTO(student);

            return retVal;
        }

        #endregion

        #region EditingUsers

        public async Task<IdentityResult> EditAdmin(string id, EditUserDTO user)
        {
            Admin admin = db.AdminsRepository.GetByID(id);
            if (admin != null)
            {
                admin.Email = user.Email;
                admin.FirstName = user.FirstName;
                admin.LastName = user.LastName;
                admin.UserName = user.UserName;
            }

            return await db.AuthRepository.EditAdmin(admin);
        }

        public async Task<IdentityResult> EditTeahcher(string id, EditUserDTO user)
        {
            Teacher teacher = db.TeachersRepository.GetByID(id);
            if (teacher != null)
            {
                teacher.Email = user.Email;
                teacher.FirstName = user.FirstName;
                teacher.LastName = user.LastName;
                teacher.UserName = user.UserName;
            }

            return await db.AuthRepository.EditTeacher(teacher);
        }

        public async Task<IdentityResult> EditParent(string id, EditUserDTO user)
        {
            Parent parent = db.ParentsRepository.GetByID(id);
            if (parent != null)
            {
                parent.Email = user.Email;
                parent.FirstName = user.FirstName;
                parent.LastName = user.LastName;
                parent.UserName = user.UserName;
            }            

            return await db.AuthRepository.EditParent(parent);            
        }

        public async Task<IdentityResult> EditStudent(string id, EditUserDTO user)
        {
            Student student = db.StudentsRepository.GetByID(id);
            if (student != null)
            {
                student.Email = user.Email;
                student.FirstName = user.FirstName;
                student.LastName = user.LastName;
                student.UserName = student.UserName;
            }

            return await db.AuthRepository.EditStudent(student);
        }

        #endregion
    }
}