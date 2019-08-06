using ElementarySchoolProject.Models;
using ElementarySchoolProject.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElementarySchoolProject.Utilities
{
    public static class GradeToGradeDTOConverters
    {
        public static GradeDTO GradeToGradeDTO (Grade grade)
        {
            GradeDTO retVal = new GradeDTO()
            {
                Id = grade.Id,
                Value = grade.Value,
                DateOfGrading = grade.DateOfGrading,
                Subject = SchoolSubjectToSchoolSubjectDTOConverters.SchoolSubjectToSchoolSubjectDTO(grade.TeacherSchoolSubject.SchoolSubject),
                GradingTeacher = UserToUserDTOConverters.UserToUserSimpleViewDTO(grade.TeacherSchoolSubject.Teacher)
            };

            return retVal;
        }

        public static Grade GradeCreateAndEditDTOToGrade(GradeCreateAndEditDTO dto)
        {
            Grade retVal = new Grade();

            retVal.Id = dto.Id;
            retVal.Value = dto.Value;
            retVal.DateOfGrading = dto.DateOfGrading;
            retVal.Student.Id = dto.StudentId;
            retVal.TeacherSchoolSubject.Id = dto.TeacherSubjectId;

            return retVal;
        }
    }
}