using ElementarySchoolProject.Models;
using ElementarySchoolProject.Models.DTOs;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElementarySchoolProject.Utilities
{
    public static class TeacherSchoolSubjectToTeacherSchoolSubjectDTOConverters
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static TeacherSchoolSubjectDTO TeacherSchoolSubjectToTeacherSchoolSubjectDTO(TeacherSchoolSubject tss)
        {
            TeacherSchoolSubjectDTO retVal = new TeacherSchoolSubjectDTO();

            retVal.Id = tss.Id;
            retVal.SubjectId = tss.SchoolSubject.Id;
            retVal.SubjectName = tss.SchoolSubject.Name;
            retVal.TeacherId = tss.Teacher.Id;
            retVal.TeacherFirstName = tss.Teacher.FirstName;
            retVal.TeacherLastName = tss.Teacher.LastName;

            return retVal;
        }

        public static TeacherSchoolSubject TeacherSchoolSubjectCreateAndEditDTOToTeacherSchoolSubject(Teacher teacher, SchoolSubject subject)
        {
            TeacherSchoolSubject retVal = new TeacherSchoolSubject();

            retVal.Teacher = teacher;
            retVal.SchoolSubject = subject;

            return retVal;
        }
    }
}