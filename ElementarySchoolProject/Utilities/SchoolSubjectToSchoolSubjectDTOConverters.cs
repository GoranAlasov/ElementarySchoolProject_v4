using ElementarySchoolProject.Models;
using ElementarySchoolProject.Models.DTOs;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElementarySchoolProject.Utilities
{
    public static class SchoolSubjectToSchoolSubjectDTOConverters
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static SchoolSubjectBasicDTO SchoolSubjectToSchoolSubjectDTO(SchoolSubject subject)
        {
            SchoolSubjectBasicDTO retVal = new SchoolSubjectBasicDTO()
            {
                Id = subject.Id,
                Name = subject.Name
            };

            logger.Info("Converting SchoolSubject to SchoolSubjectDTO");
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

            logger.Info("Coverting SchoolSubject to SchoolSubjectWithWeeklyClassesDTO.");
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
                IEnumerable<ApplicationUser> teachers = subject.TeacherSchoolSubjects.Select(x => x.Teacher);
                retVal.Teachers = teachers.Select(x => UserToUserDTOConverters.UserToUserSimpleViewDTO(x));
            }           

            logger.Info("Converting SchoolSubject to SchoolSubjectWithWeeklyClassesAndTeachersDTO.");
            return retVal;                       
        }

        public static SchoolSubject SchoolSubjectWithWeeklyClassesDTOToSchoolSubject(SchoolSubjectWithWeeklyClassesDTO dto)
        {
            SchoolSubject retVal = new SchoolSubject();

            retVal.Id = dto.Id;
            retVal.Name = dto.Name;
            retVal.WeeklyClasses = dto.WeeklyClasses;

            logger.Info("Coverting SchoolSubjectWithWeeklyClassesDTO To SchoolSubject");
            return retVal;
        }

        public static SchoolSubject SchoolSubjectCreateAndEditDTOToSchoolSubject(SchoolSubjectCreateAndEditDTO dto)
        {
            SchoolSubject retVal = new SchoolSubject()
            {
                Name = dto.Name,
                WeeklyClasses = dto.WeeklyClasses
            };

            logger.Info("Converting SchoolSubjectCreateAndEditDTO to SchoolSubject.");
            return retVal;
        }
    }
}