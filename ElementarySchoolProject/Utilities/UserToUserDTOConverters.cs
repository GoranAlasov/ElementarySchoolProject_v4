using ElementarySchoolProject.Models;
using ElementarySchoolProject.Models.DTOs.UserDTOs;
using ElementarySchoolProject.Repositories;
using Microsoft.AspNet.Identity;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace ElementarySchoolProject.Utilities
{
    public static class UserToUserDTOConverters
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

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

        public static Admin EditUserDTOToAdmin(EditUserDTO dto)
        {
            return new Admin()
            {
                Email = dto.Email,
                UserName = dto.UserName,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };
        }

        public static Teacher EditUserDTOToTeacher(EditUserDTO dto)
        {
            return new Teacher()
            {
                Email = dto.Email,
                UserName = dto.UserName,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };
        }

        public static Parent EditUserDTOToParent(EditUserDTO dto)
        {
            return new Parent()
            {
                Email = dto.Email,
                UserName = dto.UserName,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };
        }

        public static Student EditUserDTOToStudent(EditUserDTO dto)
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
            retVal.Parent.Id = dto.ParentId;

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

        public static UserViewWithRoleIdsDTO UserToUserViewWithRoleIds(ApplicationUser user)
        {            
            return new UserViewWithRoleIdsDTO()
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

            if (user.Students.Count < 0)
            {
                retVal.Students = null;
            }
            else
            {
                retVal.Students = user.Students.Select(child =>
                {
                    UserSimpleViewDTO dto = UserToUserSimpleViewDTO(child);
                    return dto;
                }).ToList();
            }            

            return retVal;
        }

        public static TeacherBasicDTO TeacherToTeacherBasicDTO(Teacher teacher)
        {
            TeacherBasicDTO retVal = new TeacherBasicDTO();

            retVal.Id = teacher.Id;
            retVal.FirstName = teacher.FirstName;
            retVal.LastName = teacher.LastName;
            retVal.UserName = teacher.UserName;
            retVal.TeachesSubjects = teacher.TeacherSchoolSubjects
                .Where(x => x.Teacher.Id == teacher.Id)
                .Select(x => SchoolSubjectToSchoolSubjectDTOConverters.SchoolSubjectToSchoolSubjectDTO(x.SchoolSubject));

            return retVal;
        }

        public static StudentBasicDTO StudentToStudentBasicDTO(Student user)
        {
            StudentBasicDTO retVal = new StudentBasicDTO();

            retVal.Id = user.Id;
            retVal.FirstName = user.FirstName;
            retVal.LastName = user.LastName;
            retVal.UserName = user.UserName;
            retVal.Email = user.Email;

            return retVal;
        }

        public static StudentWithParentDTO StudentToStudentWithParentDTO(Student user)
        {
            StudentWithParentDTO retVal = new StudentWithParentDTO();

            retVal.Id = user.Id;
            retVal.FirstName = user.FirstName;
            retVal.LastName = user.LastName;
            retVal.UserName = user.UserName;
            retVal.Email = user.Email;

            if (user.Parent == null)
            {
                retVal.Parent = null;
            }
            else
            {
                retVal.Parent = UserToUserSimpleViewDTO(user.Parent);
            }            

            return retVal;
        }

        public static StudentWithParentGradesClassDTO StudentToStudentWithParentGradesClassDTO(Student user)
        {
            StudentWithParentGradesClassDTO retVal = new StudentWithParentGradesClassDTO();

            retVal.Id = user.Id;
            retVal.FirstName = user.FirstName;
            retVal.LastName = user.LastName;
            retVal.UserName = user.UserName;
            retVal.Email = user.Email;

            if (user.Parent == null)
            {
                retVal.Parent = null;
            }
            else
            {
                retVal.Parent = UserToUserSimpleViewDTO(user.Parent);
            }

            if (user.SchoolClass == null)
            {
                retVal.Class = null;
            }
            else
            {
                retVal.Class = SchoolClassToSchoolClassDTOConverters.SchoolClassToSchoolClassDTO(user.SchoolClass);
            }

            if (user.Grades.Count() < 1)
            {
                retVal.Grades = null;
            }
            else
            {
                retVal.Grades = user.Grades.Select(x => GradeToGradeDTOConverters.GradeToGradeDTO(x));
            }

            return retVal;
        }
    }
}