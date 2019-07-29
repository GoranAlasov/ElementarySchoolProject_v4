﻿using ElementarySchoolProject.Models;
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

        public static Student RegisterStudentDTOtoStudent(RegisterStudentDTO dto)
        {
            Student retVal = new Student();

            retVal.Email = dto.Email;
            retVal.UserName = dto.UserName;
            retVal.FirstName = dto.FirstName;
            retVal.LastName = dto.LastName;
            retVal.ParentId = dto.ParentId;

            return retVal;
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

        public static UserViewWithRoleIds UserToUserViewWithRoleIds(ApplicationUser user)
        {            
            return new UserViewWithRoleIds()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                RoleIds = user.Roles.Select(x => x.RoleId)
            };
        }
        
        public static UserSimpleViewDTO UserToUserSimpleViewDTO(ApplicationUser user)
        {
            return new UserSimpleViewDTO()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email
            };
        }

        public static ParentSimpleViewDTO ParentToParentSimpleViewDTO(Parent user)
        {
            ParentSimpleViewDTO retVal = new ParentSimpleViewDTO();

            retVal.Id = user.Id;
            retVal.FirstName = user.FirstName;
            retVal.LastName = user.LastName;
            retVal.UserName = user.UserName;
            retVal.Email = user.Email;
            retVal.Children = user.Children.Select(child =>
            {
                UserSimpleViewDTO dto = UserToUserSimpleViewDTO(child);
                return dto;
            }).ToList();

            return retVal;
        }
    }
}