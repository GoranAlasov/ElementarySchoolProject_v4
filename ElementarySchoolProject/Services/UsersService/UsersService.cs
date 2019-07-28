using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ElementarySchoolProject.Models;
using ElementarySchoolProject.Models.Users.UserDTOs;
using ElementarySchoolProject.Repositories;
using Microsoft.AspNet.Identity;

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
            Admin userToInsert = Utilities.UserToUserDTOConverters.RegisterUserDTOtoAdmin(user);

            return await db.AuthRepository.RegisterAdmin(userToInsert, user.Password);
        }

        public async Task<IdentityResult> RegisterTeacher(RegisterUserDTO user)
        {
            Teacher userToInsert = Utilities.UserToUserDTOConverters.RegisterUserDTOtoTeacher(user);

            return await db.AuthRepository.RegisterTeacher(userToInsert, user.Password);
        }

        public async Task<IdentityResult> RegisterParent(RegisterUserDTO user)
        {
            Parent userToInsert = Utilities.UserToUserDTOConverters.RegisterUserDTOtoParent(user);

            return await db.AuthRepository.RegisterParent(userToInsert, user.Password);
        }

        public async Task<IdentityResult> RegisterStudent(RegisterUserDTO user)
        {
            Student userToInsert = Utilities.UserToUserDTOConverters.RegisterUserDTOtoStudent(user);

            return await db.AuthRepository.RegisterStudent(userToInsert, user.Password);
        }

        public async Task<IList<ApplicationUser>> GetAllUsers()
        {
            IList<ApplicationUser> retVal = await db.AuthRepository.GetAllUsers();

            return retVal;
        }
    }
}