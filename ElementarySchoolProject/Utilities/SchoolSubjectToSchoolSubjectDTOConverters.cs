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
        public static SchoolSubjectDTO SchoolSubjectToSchoolSubjectDTO(SchoolSubject subject)
        {
            SchoolSubjectDTO retVal = new SchoolSubjectDTO()
            {
                Id = subject.Id,
                Name = subject.Name
            };

            return retVal;
        }
    }
}