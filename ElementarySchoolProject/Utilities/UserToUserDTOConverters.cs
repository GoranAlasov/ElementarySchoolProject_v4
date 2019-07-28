using ElementarySchoolProject.Models;
using ElementarySchoolProject.Models.Users.UserDTOs;
using ElementarySchoolProject.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace ElementarySchoolProject.Utilities
{
    public static class UserToUserDTOConverters
    {
        
        #region RegisterUserDTOs

        public static Admin RegisterUserDTOtoAdmin(RegisterUserDTO dto)
        {
            return new Admin()
            {
                Email = dto.Email,
                UserName = dto.UserName,
                FirstName = dto.FirstName,
                LastName = dto.LastName                
            };
        }

        public static Teacher RegisterUserDTOtoTeacher(RegisterUserDTO dto)
        {
            return new Teacher()
            {
                Email = dto.Email,
                UserName = dto.UserName,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };
        }

        public static Parent RegisterUserDTOtoParent(RegisterUserDTO dto)
        {
            return new Parent()
            {
                Email = dto.Email,
                UserName = dto.UserName,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };
        }

        public static Student RegisterUserDTOtoStudent(RegisterUserDTO dto)
        {
            return new Student()
            {
                Email = dto.Email,
                UserName = dto.UserName,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };
        }

        #endregion

        public static RegisterUserDTO AdminToRegisterUserDTO(Admin user)
        {
            return new RegisterUserDTO()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email
            };
        }

        public static UserAdminViewInfoDTO UserToAdminViewInfoDTO(ApplicationUser user)
        {            
            return new UserAdminViewInfoDTO()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                RoleIds = user.Roles.Select(x => x.RoleId)
            };
        }        
    }
}