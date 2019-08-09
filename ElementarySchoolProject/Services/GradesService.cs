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

        public AverageGradeDTO AverageGradeByStudentId(string id)
        {
            var student = db.StudentsRepository.GetByID(id);

            if (student == null)
            {
                logger.Warn("Student with id {0} not found.");
                return null;
            }

            logger.Info("Getting avarage grade of student with id {0}, name {1} {2}.", id, student.FirstName, student.LastName);
            var grades = db.GradesRepository.Get(x => x.SchoolClassTeacherSchoolSubject.SchoolClass.Students.Any(y => y.Id == id));

            var retVal = GradeToGradeDTOConverters.GradeCollectionToAverageGradeDTO(grades);

            return retVal;
        }

        public AverageGradeDTO AverageGradeByStudentIdAndSubjectId(string studentId, int subjectId)
        {
            var student = db.StudentsRepository.GetByID(studentId);
            var subject = db.SchoolSubjectsRepository.GetByID(subjectId);

            if (student == null)
            {
                logger.Warn("Student with id {0} not found.", studentId);
                return null;
            }
            if (subject == null)
            {
                logger.Warn("Subject with id {0} not found.", subjectId);
                return null;
            }

            logger.Info("Getting avarage grade of student with id {0}, name {1} {2} in subject {3}", studentId, student.FirstName, student.LastName, subject.Name);
            var grades = db.GradesRepository.Get(x => x.SchoolClassTeacherSchoolSubject.SchoolClass.Students.Any(y => y.Id == studentId) 
            && x.SchoolClassTeacherSchoolSubject.TeacherSchoolSubject.Id == subjectId);

            var retVal = GradeToGradeDTOConverters.GradeCollectionToAverageGradeDTO(grades);

            return retVal;
        }

        

        public IEnumerable<GradeDTO> GetAll()
        {
            var retVal = db.GradesRepository.Get().Select(x => GradeToGradeDTOConverters.GradeToGradeDTO(x));
            logger.Info("Getting all grades.");
            return retVal;
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

        public IEnumerable<GradeDTO> GetAllByParentId(string id)
        {
            var parent = db.ParentsRepository.GetByID(id);

            if (parent == null)
            {
                logger.Warn("Parent with id {0} not found.");
                return null;
            }

            logger.Info("Getting all grades given to all students, children of parent with id {0}, name {1} {2}.", id, parent.FirstName, parent.LastName);
            var retVal = db.GradesRepository.Get(x => x.SchoolClassTeacherSchoolSubject.SchoolClass.Students.Any(y => y.Parent.Id == id));

            return retVal.Select(x => GradeToGradeDTOConverters.GradeToGradeDTO(x));
        }

        public IEnumerable<GradeDTO> GetAllByStudentId(string id)
        {
            var student = db.StudentsRepository.GetByID(id);

            if (student == null)
            {
                logger.Warn("Student with id {0} not found.", id);
                return null;
            }

            logger.Info("Getting all grades given to student with id {0}, name {1} {2}.", id, student.FirstName, student.LastName);
            var retVal = db.GradesRepository.Get(x => x.SchoolClassTeacherSchoolSubject.SchoolClass.Students.Any(y => y.Id == id));

            return retVal.Select(x => GradeToGradeDTOConverters.GradeToGradeDTO(x));
        }



        public IEnumerable<GradeDTO> GetAllByStudentName(string firstName, string lastName)
        {
            var student = db.StudentsRepository.Get(x => x.FirstName.ToLower() == firstName.ToLower() && x.LastName.ToLower() == lastName.ToLower()).FirstOrDefault();

            if (student == null)
            {
                logger.Warn("Student with name {0} and surname {1} not found.", firstName, lastName);
                return null;
            }

            logger.Info("Getting all grades given to student with name {0} {1}.", firstName, lastName);
            var retVal = student.Grades;

            return retVal.Select(x => GradeToGradeDTOConverters.GradeToGradeDTO(x));
        }

        public IEnumerable<GradeDTO> GetAllByStudentNameAndParentId(string firstName, string lastName, string parentId)
        {
            var student = db.StudentsRepository.Get(x => x.FirstName.ToLower() == firstName.ToLower() 
            && x.LastName.ToLower() == lastName.ToLower()
            && x.Parent.Id == parentId).FirstOrDefault();

            if (student == null)
            {
                logger.Warn("Student with name {0} and surname {1}, and parent id {2} not found.", firstName, lastName, parentId);
                return null;
            }

            logger.Info("Getting all grades given to student with name {0} {1}, child of parent {2}.", firstName, lastName, parentId);
            var retVal = student.Grades;

            return retVal.Select(x => GradeToGradeDTOConverters.GradeToGradeDTO(x));
        }

        public IEnumerable<GradeDTO> GetAllByStudentNameAndTeacherId(string firstName, string lastName, string teacherId)
        {
            var student = db.StudentsRepository.Get(x => x.FirstName.ToLower() == firstName.ToLower()
            && x.LastName.ToLower() == lastName.ToLower()
            && x.SchoolClass.SchoolClassTeacherSchoolSubjects.Any(y => y.TeacherSchoolSubject.Teacher.Id == teacherId)).FirstOrDefault();

            if (student == null)
            {
                logger.Warn("Student with name {0} and surname {1}, and teacher id {2} not found.", firstName, lastName, teacherId);
                return null;
            }

            logger.Info("Getting all grades given to student with name {0} {1}, child of parent {2}.", firstName, lastName, teacherId);
            var retVal = student.Grades;

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

        public IEnumerable<GradeDTO> GetAllByValueRangeAndParentId(int min, int max, string parentId)
        {
            if (max < min)
            {
                logger.Warn("Minimal grade value ({0}) must be less or equal than maximal grade value ({1}).", min, max);
                throw new ArgumentOutOfRangeException();
            }

            logger.Info("Getting all grades in range {0}-{1}, given to children of parent {2}", min, max, parentId);
            var retVal = db.GradesRepository.Get(x => x.Value <= max && x.Value >= min && x.Student.Parent.Id == parentId);

            return retVal.Select(x => GradeToGradeDTOConverters.GradeToGradeDTO(x));
        }

        public IEnumerable<GradeDTO> GetAllByValueRangeAndStudentId(int min, int max, string studentId)
        {
            if (max < min)
            {
                logger.Warn("Minimal grade value ({0}) must be less or equal than maximal grade value ({1}).", min, max);
                throw new ArgumentOutOfRangeException();
            }

            logger.Info("Getting all grades in range {0}-{1}, given to student {2}", min, max, studentId);
            var retVal = db.GradesRepository.Get(x => x.Value <= max && x.Value >= min && x.Student.Id == studentId);

            return retVal.Select(x => GradeToGradeDTOConverters.GradeToGradeDTO(x));
        }

        public IEnumerable<GradeDTO> GetAllByValueRangeAndTeacherId(int min, int max, string teacherId)
        {
            if (max < min)
            {
                logger.Warn("Minimal grade value ({0}) must be less or equal than maximal grade value ({1}).", min, max);
                throw new ArgumentOutOfRangeException();
            }

            logger.Info("Getting all grades in range {0}-{1}, given by teacher id {2}", min, max, teacherId);
            var retVal = db.GradesRepository.Get(x => x.Value <= max && x.Value >= min 
            && x.SchoolClassTeacherSchoolSubject.TeacherSchoolSubject.Teacher.Id == teacherId);

            return retVal.Select(x => GradeToGradeDTOConverters.GradeToGradeDTO(x));
        }

        

        public IEnumerable<GradeDTO> GetAllByGradingDateRange(DateTime min, DateTime max)
        {
            if (min > max)
            {
                logger.Warn("Date range incorrect.Date from: {0}, date to: {1}. Throwing DatesRangeException.", min, max);
                throw new DatesRangeException();
            }

            logger.Info("Getting all grades given between {0} and {1}", min, max);
            var retVal = db.GradesRepository.Get(x => x.DateOfGrading >= min && x.DateOfGrading <= max);

            return retVal.Select(x => GradeToGradeDTOConverters.GradeToGradeDTO(x));
        }

        public IEnumerable<GradeDTO> GetAllByGradignDateRangeAndTeacherId(DateTime min, DateTime max, string teacherId)
        {
            if (min > max)
            {
                logger.Warn("Date range incorrect.Date from: {0}, date to: {1}. Throwing DatesRangeException.", min, max);
                throw new DatesRangeException();
            }

            logger.Info("Getting all grades given between {0} and {1} by teacher id {2}", min, max, teacherId);
            var retVal = db.GradesRepository.Get(x => x.DateOfGrading >= min && x.DateOfGrading <= max 
            && x.SchoolClassTeacherSchoolSubject.TeacherSchoolSubject.Teacher.Id == teacherId); 

            return retVal.Select(x => GradeToGradeDTOConverters.GradeToGradeDTO(x));
        }

        public IEnumerable<GradeDTO> GetAllByGradingDateRangeAndParentId(DateTime min, DateTime max, string parentId)
        {
            if (min > max)
            {
                logger.Warn("Date range incorrect.Date from: {0}, date to: {1}. Throwing DatesRangeException.", min, max);
                throw new DatesRangeException();
            }

            logger.Info("Getting all grades given between {0} and {1} to children of parent id {2}", min, max, parentId);
            var retVal = db.GradesRepository.Get(x => x.DateOfGrading >= min && x.DateOfGrading <= max
            && x.Student.Parent.Id == parentId);

            return retVal.Select(x => GradeToGradeDTOConverters.GradeToGradeDTO(x));
        }

        public IEnumerable<GradeDTO> GetAllByGradingDateRangeAndStudentId(DateTime min, DateTime max, string studentId)
        {
            if (min > max)
            {
                logger.Warn("Date range incorrect.Date from: {0}, date to: {1}. Throwing DatesRangeException.", min, max);
                throw new DatesRangeException();
            }

            logger.Info("Getting all grades given between {0} and {1} to student id {2}", min, max, studentId);
            var retVal = db.GradesRepository.Get(x => x.DateOfGrading >= min && x.DateOfGrading <= max
            && x.Student.Id == studentId);

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

        public IEnumerable<GradeDTO> GetAllBySchoolSubjectIdAndParentId(int subjectId, string parentId)
        {
            var subject = db.SchoolSubjectsRepository.GetByID(subjectId);
            var parent = db.ParentsRepository.GetByID(parentId);

            if (subject == null)
            {
                logger.Warn("No subject with id {0}", subjectId);
                return null;
            }
            if (parent == null)
            {
                logger.Warn("No parent with id {0}", parentId);
                return null;
            }

            logger.Info("Getting all grades from school subject with id {0}, name {1}, given to children of parent id {2}", subjectId, subject.Name, parentId);
            var retVal = db.GradesRepository.Get(x => x.SchoolClassTeacherSchoolSubject.TeacherSchoolSubject.SchoolSubject.Id == subjectId
            && x.Student.Parent.Id == parentId);

            return retVal.Select(x => GradeToGradeDTOConverters.GradeToGradeDTO(x));
        }

        public IEnumerable<GradeDTO> GetAllBySchoolSubjectIdAndStudentId(int subjectId, string studentId)
        {
            var subject = db.SchoolSubjectsRepository.GetByID(subjectId);
            var student = db.StudentsRepository.GetByID(studentId);

            if (subject == null)
            {
                logger.Warn("No subject with id {0}", subjectId);
                return null;
            }
            if (student == null)
            {
                logger.Warn("No student with id {0}", studentId);
                return null;
            }

            logger.Info("Getting all grades from school subject with id {0}, name {1}, given to student id {2}", subjectId, subject.Name, studentId);
            var retVal = db.GradesRepository.Get(x => x.SchoolClassTeacherSchoolSubject.TeacherSchoolSubject.SchoolSubject.Id == subjectId
            && x.Student.Id == studentId);

            return retVal.Select(x => GradeToGradeDTOConverters.GradeToGradeDTO(x));
        }

        public IEnumerable<GradeDTO> GetAllBySchoolSubjectIdAndTeacherId(int subjectId, string teacherId)
        {
            var subject = db.SchoolSubjectsRepository.GetByID(subjectId);
            var teacher = db.TeachersRepository.GetByID(teacherId);

            if (subject == null)
            {
                logger.Warn("No subject with id {0}", subjectId);
                return null;
            }
            if (teacher == null)
            {
                logger.Warn("No teacher with id {0}", teacherId);
                return null;
            }

            logger.Info("Getting all grades from school subject with id {0}, name {1}, given by teacher id {2}", subjectId, subject.Name, teacherId);
            var retVal = db.GradesRepository.Get(x => x.SchoolClassTeacherSchoolSubject.TeacherSchoolSubject.SchoolSubject.Id == subjectId
            && x.SchoolClassTeacherSchoolSubject.TeacherSchoolSubject.Teacher.Id == teacherId);

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

            Grade grade = new Grade()
            {
                Value = dto.Value,
                DateOfGrading = DateTime.Now,
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
            grade.DateOfGrading = DateTime.Now;
            grade.Student = student;
            grade.SchoolClassTeacherSchoolSubject = sctss;

            logger.Info("Updating grade");
            db.GradesRepository.Update(grade);
            db.Save();

            return GradeToGradeDTOConverters.GradeToGradeDTO(grade);
        }
    }
}