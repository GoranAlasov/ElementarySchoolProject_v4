using ElementarySchoolProject.Models;
using ElementarySchoolProject.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElementarySchoolProject.Utilities
{
    public static class SchoolClassToSchoolClassDTOConverters
    {
        public static SchoolClassDTO SchoolClassToSchoolClassDTO(SchoolClass sc) 
        {
            SchoolClassDTO retVal = new SchoolClassDTO()
            {
                Id = sc.Id,
                SchoolGrade = sc.SchoolGrade,
                ClassName = sc.Name
            };

            return retVal;
        }        

        public static SchoolClass SchoolClassCreateAndEditDTOToSchoolClass(SchoolClassCreateAndEditDTO dto)
        {
            SchoolClass retVal = new SchoolClass()
            {
                Name = dto.Name,
                SchoolGrade = dto.SchoolGrade
            };

            return retVal;
        }        
    }
}