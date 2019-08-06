using ElementarySchoolProject.Models;
using ElementarySchoolProject.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElementarySchoolProject.Utilities
{
    public static class SchoolSubjectToSchoolSubjectDTOConverters
    {
        public static SchoolSubjectBasicDTO SchoolSubjectToSchoolSubjectDTO(SchoolSubject subject)
        {
            SchoolSubjectBasicDTO retVal = new SchoolSubjectBasicDTO()
            {
                Id = subject.Id,
                Name = subject.Name
            };

            return retVal;
        }

        public static SchoolSubjectWithWeeklyClassesDTO SchoolSubjectToSchoolSubjectWithWeeklyClassesDTO(SchoolSubject subject)
        {
            SchoolSubjectWithWeeklyClassesDTO retVal = new SchoolSubjectWithWeeklyClassesDTO()
            {
                Id = subject.Id,
                Name = subject.Name,
                WeeklyClasses = subject.WeeklyClasses
            };

            return retVal;
        }

        public static SchoolSubjectWithWeeklyClassesAndTeachersDTO SchoolSubjectToSchoolSubjectWithWeeklyClassesAndTeachersDTO(SchoolSubject subject)
        {
            SchoolSubjectWithWeeklyClassesAndTeachersDTO retVal = new SchoolSubjectWithWeeklyClassesAndTeachersDTO();

            retVal.Id = subject.Id;
            retVal.Name = subject.Name;
            retVal.WeeklyClasses = subject.WeeklyClasses;

            if (subject.TeacherSchoolSubjects.Count() < 0 )
            {
                retVal.Teachers = null;
            }
            else
            {
                
            }

            IEnumerable<ApplicationUser> teachers = subject.TeacherSchoolSubjects.Select(x => x.Teacher);
            retVal.Teachers = teachers.Select(x => UserToUserDTOConverters.UserToUserSimpleViewDTO(x));

            return retVal;                       
        }

        public static SchoolSubject SchoolSubjectWithWeeklyClassesDTOToSchoolSubject(SchoolSubjectWithWeeklyClassesDTO dto)
        {
            SchoolSubject retVal = new SchoolSubject();

            retVal.Id = dto.Id;
            retVal.Name = dto.Name;
            retVal.WeeklyClasses = dto.WeeklyClasses;
            
            return retVal;
        }

        public static SchoolSubject SchoolSubjectCreateAndEditDTOToSchoolSubject(SchoolSubjectCreateAndEditDTO dto)
        {
            SchoolSubject retVal = new SchoolSubject()
            {
                Name = dto.Name,
                WeeklyClasses = dto.WeeklyClasses
            };

            return retVal;
        }
    }
}