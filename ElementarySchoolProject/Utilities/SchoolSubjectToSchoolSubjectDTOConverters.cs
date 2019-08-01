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

        public static SchoolSubject SchoolSubjectWithWeeklyClassesDTOToSchoolSubject(SchoolSubjectWithWeeklyClassesDTO dto)
        {
            SchoolSubject retVal = new SchoolSubject()
            {
                Id = dto.Id,
                Name = dto.Name,
                WeeklyClasses = dto.WeeklyClasses
            };

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