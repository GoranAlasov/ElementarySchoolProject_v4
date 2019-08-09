using ElementarySchoolProject.Models;
using ElementarySchoolProject.Models.DTOs;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElementarySchoolProject.Utilities
{
    public static class GradeToGradeDTOConverters
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static GradeDTO GradeToGradeDTO (Grade grade)
        {
            GradeDTO retVal = new GradeDTO()
            {
                Id = grade.Id,
                Value = grade.Value,
                DateOfGrading = grade.DateOfGrading,

                Subject = SchoolSubjectToSchoolSubjectDTOConverters
                .SchoolSubjectToSchoolSubjectDTO(grade.SchoolClassTeacherSchoolSubject.TeacherSchoolSubject.SchoolSubject),

                GradingTeacher = UserToUserDTOConverters
                .UserToUserSimpleViewDTO(grade.SchoolClassTeacherSchoolSubject.TeacherSchoolSubject.Teacher),

                Student = UserToUserDTOConverters
                .StudentToStudentBasicDTO(grade.Student)
            };

            logger.Info("Converting Grade to GradeDTO.");
            return retVal;
        }

        public static Grade GradeCreateAndEditDTOToGrade(GradeCreateAndEditDTO dto)
        {
            Grade retVal = new Grade();

            retVal.Id = dto.Id;
            retVal.Value = dto.Value;
            retVal.DateOfGrading = DateTime.Now;
            retVal.Student.Id = dto.StudentId;
            //retVal.

            logger.Info("Converting GradeCreateAndEditDTO to Grade.");
            return retVal;
        }

        public static AverageGradeDTO GradeCollectionToAverageGradeDTO(IEnumerable<Grade> gradeList)
        {
            AverageGradeDTO avg = new AverageGradeDTO();
            IEnumerable<int> grades = gradeList.Select(x => x.Value);

            avg.AvgGrade = grades.Average(x => x);

            logger.Info("Converting collection of grades to average grade.");
            return avg;
        }
    }
}