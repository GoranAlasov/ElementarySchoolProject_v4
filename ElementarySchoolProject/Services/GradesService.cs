using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ElementarySchoolProject.Models;
using ElementarySchoolProject.Models.DTOs;
using ElementarySchoolProject.Repositories;
using ElementarySchoolProject.Utilities;
using ElementarySchoolProject.Utilities.Exceptions;
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

            if (teacher == null)
            {
                logger.Warn("Teacher with id {0} not found.", teacherId);
                throw new KeyNotFoundException("teacherId doesn't exist.");
            }

            if (student == null)
            {
                logger.Warn("Student with id {0} not found.", dto.StudentId);
                throw new KeyNotFoundException("dto.StudentId doesn't exist.");
            }

            if (subject==null)
            {
                logger.Warn("Subject with id {0} not found.", dto.SchoolSubjectId);
                throw new KeyNotFoundException("dto.SchoolSubjectId doesn't exist.");
            }
            
            if (!teacher.TeacherSchoolSubjects.Select(x => x.SchoolSubject).Any(y => y.Id == subject.Id))                                
            {
                logger.Warn("Teacher {0} {1} (id: {2}) does not teach subject {3} (id: {4}). Cannot grade the student.",
                    teacher.FirstName, teacher.LastName, teacher.Id, subject.Name, subject.Id);
                throw new Exception("Teacher doesn't teach the given subject.");
                //TODO 99: fix exceptions!
            }

            if (!teacher.TeacherSchoolSubjects.Any(x => x.SchoolClassTeacherSchoolSubjects.Any(y => y.SchoolClass.Id == sc.Id)))
            {
                logger.Warn("Teacher {0} {1} (id: {2}) doesn't teach the subject {3} (id: {4}) to student {5} {6} (id: {7}). Cannot grade the student.",
                    teacher.FirstName, teacher.LastName, teacher.Id, subject.Name, subject.Id, student.FirstName, student.LastName, student.Id);
                throw new Exception("Teacher doesn't teach the subject to the given student");
            }

            TeacherSchoolSubject tss = db.TeacherSchoolSubjectSRepository.Get()
                .Where(x => x.SchoolSubject.Id == subject.Id && x.Teacher.Id == teacher.Id).FirstOrDefault();

            if (tss == null)
            {
                logger.Warn("TeacherSchoolSubject combination with teacherId {0} and subject id {1} not found.", teacher.Id, subject.Id);
                throw new KeyNotFoundException("Given TeacherSchoolSubject combination doesn't exist. Please make one.");
            }

            SchoolClassTeacherSchoolSubject sctss = db.SchoolClassTeacherSchoolSubjectRepository.Get()
                .Where(x => x.TeacherSchoolSubject.Id == tss.Id && x.SchoolClass.Id == sc.Id).FirstOrDefault();

            if (sctss == null)
            {
                logger.Warn("SchoolClassTeacherSchoolSubject combination with schoolClassId {0} and teacherSchoolSubjectId {1} not found.", sc.Id, tss.Id);
                throw new KeyNotFoundException("Given SchoolClassTeacherSchoolSubject combination doesn't exist. Please make one.");
            }

            Grade grade = new Grade()
            {
                Value = dto.Value,
                DateOfGrading = dto.DateOfGrading,
                SchoolClassTeacherSchoolSubject = sctss,
                Student = student            
            };

            logger.Info("Creating grade. teacherId: {0}, studentId: {1}, subjectId: {2}", teacher.Id, student.Id, subject.Id);

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

            logger.Info("Deleting grade id {0}.", id);

            return GradeToGradeDTOConverters.GradeToGradeDTO(grade);
        }

        public GradeDTO EditGrade(int id, string teacherId, GradeCreateAndEditDTO dto)
        {
            Grade grade = db.GradesRepository.GetByID(id);

            Teacher teacher = db.TeachersRepository.GetByID(teacherId);
            Student student = db.StudentsRepository.GetByID(dto.StudentId);
            SchoolSubject subject = db.SchoolSubjectsRepository.GetByID(dto.SchoolSubjectId);
            SchoolClass sc = db.SchoolClassesRepository.GetByID(student.SchoolClass.Id);

            if (teacher == null)
            {
                logger.Warn("Teacher with id {0} not found.", teacherId);
                throw new KeyNotFoundException("teacherId doesn't exist.");
            }

            if (student == null)
            {
                logger.Warn("Student with id {0} not found.", dto.StudentId);
                throw new KeyNotFoundException("dto.StudentId doesn't exist.");
            }

            if (subject == null)
            {
                logger.Warn("Subject with id {0} not found.", dto.SchoolSubjectId);
                throw new KeyNotFoundException("dto.SchoolSubjectId doesn't exist.");
            }

            if (!teacher.TeacherSchoolSubjects.Select(x => x.SchoolSubject).Any(y => y.Id == subject.Id))
            {
                logger.Warn("Teacher {0} {1} (id: {2}) does not teach subject {3} (id: {4}). Cannot grade the student.",
                    teacher.FirstName, teacher.LastName, teacher.Id, subject.Name, subject.Id);
                throw new Exception("Teacher doesn't teach the given subject.");
                //TODO 99: fix exceptions!
            }

            if (!teacher.TeacherSchoolSubjects.Any(x => x.SchoolClassTeacherSchoolSubjects.Any(y => y.SchoolClass.Id == sc.Id)))
            {
                logger.Warn("Teacher {0} {1} (id: {2}) doesn't teach the subject {3} (id: {4}) to student {5} {6} (id: {7}). Cannot grade the student.",
                    teacher.FirstName, teacher.LastName, teacher.Id, subject.Name, subject.Id, student.FirstName, student.LastName, student.Id);
                throw new Exception("Teacher doesn't teach the subject to the given student");
            }

            TeacherSchoolSubject tss = db.TeacherSchoolSubjectSRepository.Get()
                .Where(x => x.SchoolSubject.Id == subject.Id && x.Teacher.Id == teacher.Id).FirstOrDefault();

            if (tss == null)
            {
                logger.Warn("TeacherSchoolSubject combination with teacherId {0} and subject id {1} not found.", teacher.Id, subject.Id);
                throw new KeyNotFoundException("Given TeacherSchoolSubject combination doesn't exist. Please make one.");
            }

            SchoolClassTeacherSchoolSubject sctss = db.SchoolClassTeacherSchoolSubjectRepository.Get()
                .Where(x => x.TeacherSchoolSubject.Id == tss.Id && x.SchoolClass.Id == sc.Id).FirstOrDefault();

            if (sctss == null)
            {
                logger.Warn("SchoolClassTeacherSchoolSubject combination with schoolClassId {0} and teacherSchoolSubjectId {1} not found.", sc.Id, tss.Id);
                throw new KeyNotFoundException("Given SchoolClassTeacherSchoolSubject combination doesn't exist. Please make one.");
            }

            grade.Value = dto.Value;
            grade.DateOfGrading = dto.DateOfGrading;
            grade.Student = student;
            grade.SchoolClassTeacherSchoolSubject = sctss;

            logger.Info("Updating grade");
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

        public IEnumerable<GradeDTO> GetAllByGradingDateRange(DateTime min, DateTime max)
        {
            if (min > max)
            {
                logger.Warn("Date range incorrect.Date from: {0}, date to: {1}. Throwing DatesRangeException.", min, max);
                throw new DatesRangeException();
            }

            logger.Info("Getting all grades between {0} and {1}", min, max);
            var retVal = db.GradesRepository.Get(x => x.DateOfGrading >= min && x.DateOfGrading <= max);

            return retVal.Select(x => GradeToGradeDTOConverters.GradeToGradeDTO(x));
        }

        public IEnumerable<GradeDTO> GetAllBySchoolSubjectId(int id)
        {
            var subject = db.SchoolSubjectsRepository.GetByID(id);

            if (subject == null)
            {
                logger.Warn("No subject with id {0}", id);
                return null;
            }

            logger.Info("Getting all grades from school subject with id {0}, name {1}", id, subject.Name);
            var retVal = db.GradesRepository.Get(x => x.SchoolClassTeacherSchoolSubject.TeacherSchoolSubject.SchoolSubject.Id == id);

            return retVal.Select(x => GradeToGradeDTOConverters.GradeToGradeDTO(x));
        }

        public IEnumerable<GradeDTO> GetAllByStudentId(string id)
        {
            var student = db.StudentsRepository.GetByID(id);

            if (student == null)
            {
                logger.Warn("Student with id {0} not found.");
                return null;
            }

            logger.Info("Getting all grades given to student with id {0}, name {1} {2}.", id, student.FirstName, student.LastName);
            var retVal = db.GradesRepository.Get(x => x.SchoolClassTeacherSchoolSubject.SchoolClass.Students.Any(y => y.Id == id));

            return retVal.Select(x => GradeToGradeDTOConverters.GradeToGradeDTO(x));
        }

        public IEnumerable<GradeDTO> GetAllByTeacherId(string id)
        {
            var teacher = db.TeachersRepository.GetByID(id);

            if (teacher == null)
            {
                logger.Warn("Teacher with id {0} not found", id);
                return null;
            }

            logger.Info("Getting all grades given by teacher with id {0}, name {1} {2}.", id, teacher.FirstName, teacher.LastName);
            var retVal = db.GradesRepository.Get(x => x.SchoolClassTeacherSchoolSubject.TeacherSchoolSubject.Teacher.Id == id);

            return retVal.Select(x => GradeToGradeDTOConverters.GradeToGradeDTO(x));
        }

        public IEnumerable<GradeDTO> GetAllByTeacherIdAndStudentId(string teacherId, string studentId)
        {
            var teacher = db.TeachersRepository.GetByID(teacherId);
            if (teacher == null)
            {
                logger.Warn("No teacher with id {0} found.", teacherId);
                return null;
            }

            var student = db.StudentsRepository.GetByID(studentId);
            if (student == null)
            {
                logger.Warn("No student with id {0} found.", studentId);
                return null;
            }

            logger.Info("Getting all grades given by teacher with id {0}, name {1} {2} to student with id {3}, name {4} {5}.",
                teacherId, teacher.FirstName, teacher.LastName, studentId, student.FirstName, student.LastName);

            var retVal = db.GradesRepository.Get(x => x.SchoolClassTeacherSchoolSubject.TeacherSchoolSubject.Teacher.Id == teacherId
            && x.SchoolClassTeacherSchoolSubject.SchoolClass.Students.Any(y => y.Id == studentId));

            return retVal.Select(x => GradeToGradeDTOConverters.GradeToGradeDTO(x));
        }

        public IEnumerable<GradeDTO> GetAllByValueRange(int min, int max)
        {
            if (max < min)
            {
                logger.Warn("Minimal grade value ({0}) must be less or equal than maximal grade value ({1}).", min, max);
                throw new ArgumentOutOfRangeException();
            }

            logger.Info("Getting all grades in range {0}-{1}", min, max);
            var retVal = db.GradesRepository.Get(x => x.Value <= max && x.Value >= min);

            return retVal.Select(x => GradeToGradeDTOConverters.GradeToGradeDTO(x));
        }

        public GradeDTO GetById(int id)
        {
            var grade = db.GradesRepository.GetByID(id);

            if (grade == null)
            {
                logger.Warn("Grade with id {0} doesn't exist.", id);
                return null;
            }

            logger.Info("Getting grade with id {0}", id);
            return GradeToGradeDTOConverters.GradeToGradeDTO(grade);
        }
    }
}