using ElementarySchoolProject.Models;
using ElementarySchoolProject.Models.DTOs;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElementarySchoolProject.Utilities
{
    public static class SchoolClassToSchoolClassDTOConverters
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static SchoolClassDTO SchoolClassToSchoolClassDTO(SchoolClass sc) 
        {
            SchoolClassDTO retVal = new SchoolClassDTO()
            {
                Id = sc.Id,
                SchoolGrade = sc.SchoolGrade,
                ClassName = sc.Name
            };

            logger.Info("Converting SchoolClass to SchoolClassDTO");
            return retVal;
        }        

        public static SchoolClass SchoolClassCreateAndEditDTOToSchoolClass(SchoolClassCreateAndEditDTO dto)
        {
            SchoolClass retVal = new SchoolClass()
            {
                Name = dto.ClassName,
                SchoolGrade = dto.SchoolGrade
            };

            logger.Info("Converting SchoolClassCreateAndEditDTO to SchoolClass");
            return retVal;
        }        

        public static SchoolClassDetailsDTO SchoolClassToSchoolClassDetailsDTO(SchoolClass sc)
        {
            SchoolClassDetailsDTO retVal = new SchoolClassDetailsDTO()
            {
                Id = sc.Id,
                SchoolGrade = sc.SchoolGrade,
                ClassName = sc.Name,
                Students = sc.Students.Select(student => UserToUserDTOConverters.StudentToStudentBasicDTO(student))
            };

            logger.Info("Converting SchoolClass to SchoolClassDetaildSTO");
            return retVal;
        }
    }
}