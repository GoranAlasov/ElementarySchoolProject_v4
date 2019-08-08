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

            logger.Info("Registering admin.");
            return await db.AuthRepository.RegisterAdmin(userToInsert, user.Password);
        }

        public async Task<IdentityResult> RegisterTeacher(RegisterUserDTO user)
        {
            Teacher userToInsert = UserToUserDTOConverters.RegisterUserDTOtoTeacher(user);

            logger.Info("Registering teacher.");
            return await db.AuthRepository.RegisterTeacher(userToInsert, user.Password);
        }

        public async Task<IdentityResult> RegisterParent(RegisterUserDTO user)
        {
            Parent userToInsert = UserToUserDTOConverters.RegisterUserDTOtoParent(user);

            logger.Info("Registering parent.");
            return await db.AuthRepository.RegisterParent(userToInsert, user.Password);
        }

        public async Task<IdentityResult> RegisterStudent(RegisterStudentDTO user)
        {
            Student userToInsert = UserToUserDTOConverters.RegisterStudentDTOtoStudent(user);

            logger.Info("Registering student.");
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
            if (user == null)
            {
                logger.Warn("No such user. {0}", id);
                return null;
            }
            logger.Info("Getting user with id {0}", id);
            
            UserViewWithRoleIdsDTO retVal = UserToUserDTOConverters.UserToUserViewWithRoleIds(user);

            return retVal;
        }

        public IEnumerable<UserSimpleViewDTO> GetAllAdmins()
        {
            IEnumerable<ApplicationUser> users = db.AdminsRepository.Get();
            logger.Info("Getting all admins.");

            IEnumerable<UserSimpleViewDTO> retVal = users.Select(x => UserToUserDTOConverters.UserToUserSimpleViewDTO(x));

            return retVal;
        }

        public IEnumerable<UserSimpleViewDTO> GetAllTeachers()
        {
            IEnumerable<ApplicationUser> users = db.TeachersRepository.Get();
            logger.Info("Getting all teachers.");

            IEnumerable<UserSimpleViewDTO> retVal = users.Select(x => UserToUserDTOConverters.UserToUserSimpleViewDTO(x));

            return retVal;
        }

        public IEnumerable<ParentSimpleViewDTO> GetAllParents()
        {
            IEnumerable<Parent> users = db.ParentsRepository.Get();
            logger.Info("Getting all parents.");

            IEnumerable<ParentSimpleViewDTO> retVal = users.Select(x => UserToUserDTOConverters.ParentToParentSimpleViewDTO(x));

            return retVal;
        }

        public IEnumerable<StudentWithParentDTO> GetAllStudents()
        {
            IEnumerable<Student> users = db.StudentsRepository.Get();
            logger.Info("Getting all students.");

            IEnumerable<StudentWithParentDTO> retVal = users.Select(x => UserToUserDTOConverters.StudentToStudentWithParentDTO(x));

            return retVal;
        }

        public UserSimpleViewDTO GetAdminById(string id)
        {
            Admin admin = db.AdminsRepository.Get(user => user.Id == id).FirstOrDefault();
            
            if (admin == null)
            {
                logger.Warn("No such admin. {0}", id);

                return null;
            }
            logger.Info("Getting admin with id {0}", id);
            UserSimpleViewDTO retVal = UserToUserDTOConverters.UserToUserSimpleViewDTO(admin);
            
            return retVal;
        }

        public UserSimpleViewDTO GetTeacherById(string id)
        {
            Teacher teacher = db.TeachersRepository.Get(user => user.Id == id).FirstOrDefault();

            if (teacher == null)
            {
                logger.Warn("No such teacher. {0}", id);
                return null;
            }
            logger.Info("Getting teacher with id {0}", id);

            UserSimpleViewDTO retVal = UserToUserDTOConverters.UserToUserSimpleViewDTO(teacher);

            return retVal;
        }

        public ParentSimpleViewDTO GetParentById(string id)
        {
            Parent parent = db.ParentsRepository.Get(p => p.Id == id).FirstOrDefault();

            if (parent == null)
            {
                logger.Warn("No such parent. {0}", id);
                return null;
            }
            logger.Info("Getting parent with id {0}", id);

            ParentSimpleViewDTO retVal = UserToUserDTOConverters.ParentToParentSimpleViewDTO(parent);

            return retVal;
        }

        public StudentWithParentDTO GetStudentById(string id)
        {
            Student student = db.StudentsRepository.Get(s => s.Id == id).FirstOrDefault();
            
            if (student == null)
            {
                logger.Warn("No such student. {0}", id);

                return null;
            }
            logger.Info("Getting student with id {0}", id);

            StudentWithParentDTO retVal = UserToUserDTOConverters.StudentToStudentWithParentDTO(student);

            return retVal;
        }

        #endregion

        #region DeletingUsers

        public UserSimpleViewDTO DeleteAdmin(string id)
        {
            Admin admin = db.AdminsRepository.Get(user => user.Id == id).FirstOrDefault();

            int adminCount = db.AdminsRepository.Get().Count();

            if (admin == null)
            {
                logger.Warn("Admin with id {0} not found.");
                return null;
            }

            if (adminCount < 2)
            {
                logger.Warn("Less than 2 admins present in the system. Can't delete the last one!");
                return null;
            }

            db.AdminsRepository.Delete(admin);
            db.Save();
            logger.Info("Deleting admin with id {0}", id);
            UserSimpleViewDTO retVal = UserToUserDTOConverters.UserToUserSimpleViewDTO(admin);

            return retVal;
        }

        public UserSimpleViewDTO DeleteTeacher(string id)
        {
            Teacher teacher = db.TeachersRepository.Get(user => user.Id == id).FirstOrDefault();

            if (teacher == null)
            {
                logger.Warn("Teacher with id {0} not found.", id);
                return null;
            }

            db.TeachersRepository.Delete(teacher);
            db.Save();

            logger.Info("Teacher with id {0} deleted.", id);
            UserSimpleViewDTO retVal = UserToUserDTOConverters.UserToUserSimpleViewDTO(teacher);

            return retVal;
        }

        public UserSimpleViewDTO DeleteParent(string id)
        {
            Parent parent = db.ParentsRepository.Get(user => user.Id == id).FirstOrDefault();
            
            IEnumerable<Student> students = db.StudentsRepository.Get(s => s.Parent.Id == parent.Id);

            if (parent == null)
            {
                logger.Warn("Parent with id {0} not found.", id);
                return null;
            }

            foreach (var item in students)
            {
                logger.Info("Removing child with id {0} from parents' children list. Parent id {1}", item.Id, id);
                item.Parent = null;
            }

            db.ParentsRepository.Delete(parent);
            db.Save();

            logger.Info("Parent with id {0} deleted.", id);
            UserSimpleViewDTO retVal = UserToUserDTOConverters.UserToUserSimpleViewDTO(parent);

            return retVal;            
        }

        public UserSimpleViewDTO DeleteStudent(string id)
        {
            Student student = db.StudentsRepository.Get(user => user.Id == id).FirstOrDefault();

            if (student == null)
            {
                logger.Warn("Student with id {0} not found.", id);
                return null;
            }

            db.StudentsRepository.Delete(student);
            db.Save();
            logger.Info("Student with id {0} deleted.", id);
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
                logger.Info("Editing admin with id {0}.", id);
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
                logger.Info("Editing teacher with id {0}.", id);
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
                logger.Info("Editing parent with id {0}.", id);
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
                logger.Info("Editing student with id {0}.", id);
            }

            return await db.AuthRepository.EditStudent(student);
        }

        #endregion
    }
}