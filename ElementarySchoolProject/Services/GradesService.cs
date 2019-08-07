using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ElementarySchoolProject.Models;
using ElementarySchoolProject.Models.DTOs;
using ElementarySchoolProject.Repositories;
using ElementarySchoolProject.Utilities;
using NLog;

namespace ElementarySchoolProject.Services
{
    public class GradesService : IGradesService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IUnitOfWork db;

        public GradesService(IUnitOfWork db)
        {
            this.db = db;
        }        

        public GradeDTO CreateGrade(string teacherId, GradeCreateAndEditDTO dto)
        {
            Teacher teacher = db.TeachersRepository.GetByID(teacherId);
            Student student = db.StudentsRepository.GetByID(dto.StudentId);
            SchoolSubject subject = db.SchoolSubjectsRepository.GetByID(dto.SchoolSubjectId);
            SchoolClass sc = db.SchoolClassesRepository.GetByID(student.SchoolClass.Id);
            
            if (!(teacher.TeacherSchoolSubjects.Select(x => x.SchoolSubject).Any(y => y.Id == subject.Id))
                || !(teacher.TeacherSchoolSubjects.Any(x => x.SchoolClassTeacherSchoolSubjects.Any(y => y.SchoolClass.Id == sc.Id))))                
            {
                throw new Exception("You fucked up!");
                //TODO 99: fix exceptions!
            }

            TeacherSchoolSubject tss = db.TeacherSchoolSubjectSRepository.Get()
                .Where(x => x.SchoolSubject.Id == subject.Id && x.Teacher.Id == teacher.Id).FirstOrDefault();

            SchoolClassTeacherSchoolSubject sctss = db.SchoolClassTeacherSchoolSubjectRepository.Get()
                .Where(x => x.TeacherSchoolSubject.Id == tss.Id && x.SchoolClass.Id == sc.Id).FirstOrDefault();

            Grade grade = new Grade()
            {
                Value = dto.Value,
                DateOfGrading = dto.DateOfGrading,
                SchoolClassTeacherSchoolSubject = sctss,
                Student = student            
            };

            db.GradesRepository.Insert(grade);
            db.Save();

            string toEmail = student.Parent.Email;
            string parentName = student.Parent.FirstName + " " + student.Parent.LastName;
            string studentName = student.FirstName + " " + student.LastName;
            string date = grade.DateOfGrading.Day + "." + grade.DateOfGrading.Month + "." + grade.DateOfGrading.Year;
            string subjectName = subject.Name;
            int gradeValue = grade.Value;

            EmailSenders.EmailGradingEventToParent(toEmail, parentName, studentName, date, subjectName, gradeValue);

            return GradeToGradeDTOConverters.GradeToGradeDTO(grade);
        }

        public GradeDTO DeleteGrade(int id)
        {
            Grade grade = db.GradesRepository.GetByID(id);

            db.GradesRepository.Delete(grade);
            db.Save();

            return GradeToGradeDTOConverters.GradeToGradeDTO(grade);
        }

        public GradeDTO EditGrade(int id, GradeCreateAndEditDTO dto)
        {
            Grade grade = db.GradesRepository.GetByID(id);

            Student student = db.StudentsRepository.GetByID(dto.StudentId);
            //TeacherSchoolSubject ts = db.TeacherSchoolSubjectSRepository.GetByID(dto.TeacherSchoolSubjectId);

            SchoolClass sc = db.SchoolClassesRepository.GetByID(student.SchoolClass.Id);

            //TODO 11.13: exception if Student is not correct type
            //TODO 11.14: exception if Student does not exist
            //TODO 11.15: exception if TeacherSchoolSubject does not exist
            //TODO 11.16: exception if ts.SchoolClasses.Any(x => x.Id != sc.Id)

            grade.Value = dto.Value;
            grade.DateOfGrading = dto.DateOfGrading;
            grade.Student = student;            

            db.GradesRepository.Update(grade);
            db.Save();

            return GradeToGradeDTOConverters.GradeToGradeDTO(grade);
        }

        public IEnumerable<GradeDTO> GetAll()
        {
            var retVal = db.GradesRepository.Get().Select(x => GradeToGradeDTOConverters.GradeToGradeDTO(x));
            logger.Info("Getting all grades.");
            return retVal;
        }

        public GradeDTO GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}