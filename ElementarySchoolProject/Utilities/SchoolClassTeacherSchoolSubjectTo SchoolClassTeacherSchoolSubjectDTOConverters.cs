using ElementarySchoolProject.Models;
using ElementarySchoolProject.Models.DTOs;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElementarySchoolProject.Utilities
{
    public static class SchoolClassTeacherSchoolSubjectToSchoolClassTeacherSchoolSubjectDTOConverters
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static SchoolClassTeacherSchoolSubjectDTO SchoolClassTeacherSchoolSubjectTo_SchoolClassTeacherSchoolSubjectDTO(SchoolClassTeacherSchoolSubject sctss)
        {
            SchoolClassTeacherSchoolSubjectDTO retVal = new SchoolClassTeacherSchoolSubjectDTO()
            {
                Id = sctss.Id,
                SchoolClassId = sctss.SchoolClass.Id,
                SchoolClassName = String.Format($"{sctss.SchoolClass.SchoolGrade} {sctss.SchoolClass.Name}"),
                TeacherSchoolSubjectId = sctss.TeacherSchoolSubject.Id,
                TeacherName = String.Format($"{sctss.TeacherSchoolSubject.Teacher.FirstName} {sctss.TeacherSchoolSubject.Teacher.LastName}"),
                SchoolSubjectName = sctss.TeacherSchoolSubject.SchoolSubject.Name
            };

            logger.Info("Converting SchoolClassTeacherSchoolSubject to SchoolClassTeacherSchoolSubjectDTO.");
            return retVal;
        }

        public static SchoolClassTeacherSchoolSubject 
            SchoolClassTeacherSchoolSubjectCreateAndEditDTOToSchoolClassTeacherSchoolSubject(SchoolClass sc, TeacherSchoolSubject tss)
        {
            SchoolClassTeacherSchoolSubject retVal = new SchoolClassTeacherSchoolSubject()
            {
                SchoolClass = sc,
                TeacherSchoolSubject = tss
            };

            logger.Info("Converting SchoolClassTeacherSchoolSubjectCreateAndEditDTO to SchoolClassTeacherSchoolSubject.");
            return retVal;
        }
    }
}