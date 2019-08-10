using ElementarySchoolProject.Models;
using ElementarySchoolProject.Repositories;
using ElementarySchoolProject.Utilities.Exceptions;
using ElementarySchoolProject.Services.UsersServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ElementarySchoolProject.Utilities;
using ElementarySchoolProject.Models.DTOs.UserDTOs;
using NLog;

namespace ElementarySchoolProject.Services.UsersServices
{
    public class StudentsService : IStudentsService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        IUnitOfWork db;

        public StudentsService(IUnitOfWork db)
        {
            this.db = db;
        }



        public IEnumerable<StudentWithParentDTO> GetAll()
        {
            var students = db.StudentsRepository.Get();
            logger.Info("Getting all entries of type {0}.", students.GetType());

            return students.Select(x => UserToUserDTOConverters.StudentToStudentWithParentDTO(x));
        }

        public IEnumerable<StudentWithParentDTO> GetAllByGradeDates(DateTime minValue, DateTime maxValue)
        {            
            if (minValue < DateTime.MinValue || maxValue > DateTime.MaxValue || minValue > maxValue)
            {
                logger.Error("Date range incorrect. Date from: {0}, date to: {1}. Throwing DatesRangeException.", minValue, maxValue);
                throw new DatesRangeException();
            }

            var students = db.StudentsRepository
                .Get(s => s.Grades.Any(grade => grade.DateOfGrading >= minValue && grade.DateOfGrading <= maxValue));
            logger.Info("Getting students graded between {0} and {1}.", minValue, maxValue);

            var retVal = students.Select(
                s => UserToUserDTOConverters.StudentToStudentWithParentDTO(s)
                );

            return retVal;
        }

        public IEnumerable<StudentWithParentDTO> GetAllByGradeDatesAndTeacherId(DateTime minValue, DateTime maxValue, string teacherId)
        {
            if (minValue < DateTime.MinValue || maxValue > DateTime.MaxValue || minValue > maxValue)
            {
                logger.Error("Date range incorrect. Date from: {0}, date to: {1}. Throwing DatesRangeException.", minValue, maxValue);
                throw new DatesRangeException();
            }

            var teacher = db.TeachersRepository.GetByID(teacherId);
            if (teacher == null)
            {
                logger.Warn("Getting teacher with id {0} failed. No such teacher", teacherId);
                throw new ArgumentException("No teacher with that id!");
            }

            var students = db.StudentsRepository
                .Get(s => s.Grades.Any(grade => grade.DateOfGrading >= minValue && grade.DateOfGrading <= maxValue) 
                && s.SchoolClass.SchoolClassTeacherSchoolSubjects.Any( x => x.TeacherSchoolSubject.Teacher.Id == teacherId));

            logger.Info("Getting students graded between {0} and {1} by teacher with id {2}.", minValue, maxValue, teacherId);

            var retVal = students.Select(
                s => UserToUserDTOConverters.StudentToStudentWithParentDTO(s)
                );

            return retVal;
        }

        public IEnumerable<StudentWithParentDTO> GetAllBySchoolClassGrade(int grade)
        {
            //TODO 11.2 **DONE** exception if grade not between 1-8
            if (grade < 1 || grade > 8)
            {
                logger.Error("Grade parameter value ({0}) incorrect. Must be in range 1-8. Throwing ArgumentOutOfRangeException.", grade);
                throw new ArgumentOutOfRangeException("Grade must be between 1 and 8!", new ArgumentOutOfRangeException());
            }

            var students = db.StudentsRepository
                .Get(s => s.SchoolClass.SchoolGrade == grade);
            logger.Info("Getting students in grade: {0}.", grade);

            return students
                .Select(s => UserToUserDTOConverters.StudentToStudentWithParentDTO(s));
        }

        public IEnumerable<StudentWithParentDTO> GetAllBySchoolClassGradeAndTeacherId(int grade, string id)
        {
            //TODO 11.3 **DONE** exception if grade not between 1-8
            //TODO 11.4 **DONE** exception if teacher id nonexistant
            //TODO 11.5 **DONE** exception if teacher id of another user type

            if (grade < 1 || grade > 8)
            {
                logger.Error("Grade parameter value ({0}) incorrect. Must be in range 1-8. Throwing ArgumentOutOfRangeException.", grade);
                throw new ArgumentOutOfRangeException("Grade must be between 1 and 8!", new ArgumentOutOfRangeException());
            }

            if (id == null)
            {
                logger.Error("TeacherId parameter missing. Throwing ArgumentNullException.");
                throw new ArgumentNullException("TeacherId must be specified.");
            }

            try
            {                
                Teacher teacher = db.TeachersRepository.GetByID(id);

                if (teacher == null || !(teacher is Teacher))
                {
                    logger.Error("Teacher with id {0} not fount, or of a wrong user type. Throwing KeyNotFoundException.", id);
                    throw new KeyNotFoundException("Teacher is missing or is of a wrong user type!");
                }                

                var students = db.StudentsRepository                
                .Get(s => s.SchoolClass.SchoolGrade == grade &&
                s.SchoolClass.SchoolClassTeacherSchoolSubjects.Any(x => x.TeacherSchoolSubject.Teacher.Id == id));
                logger.Info("Getting all students in grade {0} who are taught by teacher id={1}", grade, id);

                return students
                .Select(s => UserToUserDTOConverters.StudentToStudentWithParentDTO(s));
            }
            catch (Exception e)
            {
                logger.Error("Caught exception {0}!", e.Message);
                throw e;
            }
        }

        public IEnumerable<StudentWithParentDTO> GetAllBySchoolClassId(int id)
        {
            //TODO 11.6 **DONE** exception if schoolClass id nonexistant

            var schoolClass = db.SchoolClassesRepository.GetByID(id);

            if (schoolClass == null)
            {
                logger.Error("SchoolClassId {0} doesn't exist.", id);
                throw new KeyNotFoundException("That SchoolClassId doesn't exist. Throwing KeyNotFoundException.");
            }

            var students = db.StudentsRepository
            .Get(s => s.SchoolClass.Id == id);
            logger.Info("Getting all students from class with id {0}");

            return students
                .Select(s => UserToUserDTOConverters.StudentToStudentWithParentDTO(s));
        }

        public IEnumerable<StudentWithParentDTO> GetAllBySchoolClassIdAndTeacherId(int classId, string teacherId)
        {
            var schoolClass = db.SchoolClassesRepository.GetByID(classId);
            var teacher = db.TeachersRepository.GetByID(teacherId);

            if (schoolClass == null)
            {
                logger.Error("SchoolClassId {0} doesn't exist.", classId);
                throw new KeyNotFoundException("That SchoolClassId doesn't exist. Throwing KeyNotFoundException.");
            }

            if (teacher == null)
            {
                logger.Error("TeacherID {0} doesn't exist.", teacherId);
                throw new KeyNotFoundException("That TeacherId doesn't exist. Throwing KeyNotFoundException.");
            }

            var students = db.StudentsRepository
            .Get(s => s.SchoolClass.Id == classId 
            && s.SchoolClass.SchoolClassTeacherSchoolSubjects
            .Any(x => x.TeacherSchoolSubject.Teacher.Id == teacherId));
            logger.Info("Getting all students from class with id {0}, taught by teacher with ID {1}", classId, teacherId);

            return students
                .Select(s => UserToUserDTOConverters.StudentToStudentWithParentDTO(s));
        }        

        public IEnumerable<StudentWithParentDTO> GetAllByTeacherId(string id)
        {
            //TODO 11.8 **DONE** exception if teacherid nonexistant
            //TODO 11.9 **DONE** exception if teacherid of another user type

            var teacher = db.TeachersRepository.GetByID(id);

            if (teacher == null || !(teacher is Teacher))
            {
                logger.Error("Teacher with id {0} not fount, or of a wrong user type. Throwing ArgumentException.", id);
                throw new ArgumentException("TeacherId is not correct.", new ArgumentException());
            }

            var students = db.StudentsRepository            
            .Get(s => s.SchoolClass.SchoolClassTeacherSchoolSubjects.Any(x => x.TeacherSchoolSubject.Teacher.Id == id));
            logger.Info("Getting students who are taught by {0} {1}, id : {2}", teacher.FirstName, teacher.LastName, id);

            return students
                .Select(s => UserToUserDTOConverters.StudentToStudentWithParentDTO(s));
        }        

        public StudentWithParentGradesClassDTO GetById(string id)
        {
            Student student = db.StudentsRepository.GetByID(id);

            if (student == null || !(student is Student))
            {
                logger.Error("Student with id {0} not found, or of a wrong user type. Throwing KeyNotFoundException.", id);
                throw new KeyNotFoundException();
            }

            logger.Info("Getting student with id {0}", id);
            return UserToUserDTOConverters.StudentToStudentWithParentGradesClassDTO(student);
        }

        public StudentWithParentGradesClassDTO GetByIdAndParentId(string studentId, string parentId)
        {
            Student student = db.StudentsRepository.GetByID(studentId);

            if (student == null || !(student is Student))
            {
                logger.Error("Student with id {0} not found, or of a wrong user type. Throwing KeyNotFoundException.", studentId);
                throw new KeyNotFoundException();
            }

            if (student.Parent.Id != parentId)
            {
                logger.Error("student {0} {1} is not child of parent with id {2}", student.FirstName, student.LastName, parentId);
                throw new ArgumentException("That is not your child!");
            }

            logger.Info("Getting StudentId {0}, child of {1}", studentId, parentId);
            return UserToUserDTOConverters.StudentToStudentWithParentGradesClassDTO(student);
        }

        public IEnumerable<StudentWithParentGradesClassDTO> GetAllByParentId(string parentId)
        {
            //TODO 11.11: **DONE** exception if parentId nonexistant
            //TODO 11.12: **DONE** exception if parentId is of other user type
            var parent = db.ParentsRepository.GetByID(parentId);

            if (parent == null)
            {
                logger.Error("Parent with id {0} doesn't exist.", parentId);
                throw new ArgumentException("That parent does not exist.");
            }

            var students = db.StudentsRepository
                .Get(s => s.Parent.Id == parentId);
            logger.Info("Getting all children of {0} {1}.", parent.FirstName, parent.LastName);

            return students
                .Select(s => UserToUserDTOConverters.StudentToStudentWithParentGradesClassDTO(s));
        }        

        public StudentWithParentGradesClassDTO GetByIdAndTeacherId(string studentId, string teacherId)
        {
            Student student = db.StudentsRepository.GetByID(studentId);

            if (student == null || !(student is Student))
            {
                logger.Error("Student with id {0} not found, or of a wrong user type. Throwing KeyNotFoundException.", studentId);
                throw new KeyNotFoundException();
            }

            if (!student.SchoolClass.SchoolClassTeacherSchoolSubjects.Any( x=> x.TeacherSchoolSubject.Teacher.Id == teacherId))
            {
                logger.Error("student {0} {1} is not a student of teacher with id {2}", student.FirstName, student.LastName, teacherId);
                throw new ArgumentException("You are not that childs' teacher.");
            }

            logger.Info("Getting StudentId {0}, taught by of {1}", studentId, teacherId);
            return UserToUserDTOConverters.StudentToStudentWithParentGradesClassDTO(student);
        }

        public IEnumerable<StudentWithParentDTO> GetAllByStudentName(string name)
        {
            if (name == null)
            {
                throw new NullReferenceException();
            }

            var students = db.StudentsRepository.Get().Where(x => (x.FirstName + " " + x.LastName).ToLower().Contains(name.ToLower()));

            if (students == null)
            {
                logger.Warn("No students' name or surname contain the given string parameter \"{0}\"", name);
                throw new KeyNotFoundException("student with that string in their name or surname not found.");
            }
                        
            logger.Info("Getting all students whose name or surname contain \"{0}\"", name);

            return students
                .Select(s => UserToUserDTOConverters.StudentToStudentWithParentDTO(s));
        }

        public IEnumerable<StudentWithParentDTO> GetAllByStudentNameAndTeacherID(string name, string teacherId)
        {
            if (name == null)
            {
                throw new NullReferenceException();
            }

            var students = db.StudentsRepository.Get().Where(x => (x.FirstName + " " + x.LastName).ToLower().Contains(name.ToLower()));
            var teacher = db.TeachersRepository.GetByID(teacherId);

            if (students == null)
            {
                logger.Warn("No students' name or surname contain the given string parameter \"{0}\"", name);
                throw new KeyNotFoundException("student with that string in their name or surname not found.");
            }

            if (teacher == null)
            {
                logger.Warn("No teacher with id {0} found.", teacherId);
                throw new KeyNotFoundException("Teacher with that id not found.");
            } 

            var retVal = students.Where(x => x.SchoolClass.SchoolClassTeacherSchoolSubjects.Any( y => y.TeacherSchoolSubject.Teacher.Id == teacherId));
            logger.Info("Getting all students whose name or surname contain \"{0}\", and who have teacher id {1}", name, teacherId);

            return retVal
                .Select(s => UserToUserDTOConverters.StudentToStudentWithParentDTO(s));
        }
                
        public IEnumerable<StudentWithParentDTO> GetAllByTeacherName(string teacherName)
        {
            if (teacherName == null)
            {
                throw new NullReferenceException();
            }

            var students = db.StudentsRepository.Get(st => st.SchoolClass.SchoolClassTeacherSchoolSubjects
            .Any(sctss => (sctss.TeacherSchoolSubject.Teacher.FirstName + " " + sctss.TeacherSchoolSubject.Teacher.LastName).ToLower().Contains(teacherName.ToLower())));

            if (students == null)
            {
                logger.Warn("No students found whose one of the teachers' name or surname contain the given string parameter \"{0}\"", teacherName);
                throw new KeyNotFoundException("teacher with that string in their name or surname not found.");
            }

            logger.Info("Getting all students one of whos' teachers' name or surname contain \"{0}\"", teacherName);

            return students
                .Select(s => UserToUserDTOConverters.StudentToStudentWithParentDTO(s));
        }

        public IEnumerable<StudentWithParentDTO> GetAllByTeacherNameAndTeacherId(string teacherName, string teacherId)
        {
            if (teacherName == null)
            {
                throw new NullReferenceException();
            }

            var students = db.StudentsRepository.Get(st => st.SchoolClass.SchoolClassTeacherSchoolSubjects
            .Any(sctss => sctss.TeacherSchoolSubject.Teacher.Id == teacherId))
            .Where(x => x.SchoolClass.SchoolClassTeacherSchoolSubjects
            .Any(y => (y.TeacherSchoolSubject.Teacher.FirstName + " " + y.TeacherSchoolSubject.Teacher.LastName).ToLower().Contains(teacherName.ToLower())));

            if (students == null)
            {
                logger.Warn("No students found whose one of the teachers' name or surname contain the given string parameter \"{0}\"", teacherName);
                throw new KeyNotFoundException("teacher with that string in their name or surname not found.");
            }

            logger.Info("Getting all students one of whos' teachers' name or surname contain \"{0}\"", teacherName);

            return students
                .Select(s => UserToUserDTOConverters.StudentToStudentWithParentDTO(s));
        }

        public IEnumerable<StudentWithParentDTO> GetAllBySchoolSubjectId(int id)
        {
            //TODO 11.7 **DONE** exception if school subject id nonexistant

            var schoolSubject = db.SchoolClassesRepository.GetByID(id);

            if (schoolSubject == null)
            {
                logger.Error("SchoolSubjectId {0} doesn't exist. Throwing KeyNotFoundException.", id);
                throw new KeyNotFoundException("That SchoolSubjectId doesn't exist.");
            }

            var students = db.StudentsRepository
            .Get(s => s.SchoolClass.SchoolClassTeacherSchoolSubjects.Any(x => x.TeacherSchoolSubject.SchoolSubject.Id == id));
            logger.Info("Getting all students who have school subject with id {0}.", id);

            return students
                .Select(s => UserToUserDTOConverters.StudentToStudentWithParentDTO(s));
        }

        public IEnumerable<StudentWithParentDTO> GetAllBySchoolSubjectIdAndTeacherId(int subjectId, string teacherId)
        {
            var schoolSubject = db.SchoolSubjectsRepository.GetByID(subjectId);
            var teacher = db.TeachersRepository.GetByID(teacherId);

            if (schoolSubject == null)
            {
                logger.Error("SchoolSubjectId {0} doesn't exist. Throwing KeyNotFoundException.", subjectId);
                throw new KeyNotFoundException("That SchoolSubjectId doesn't exist.");
            }

            if (teacher == null)
            {
                logger.Warn("No teacher with id {0} found.", teacherId);
                throw new KeyNotFoundException("Teacher with that id not found.");
            }

            var students = db.StudentsRepository
            .Get(s => s.SchoolClass.SchoolClassTeacherSchoolSubjects
            .Any(x => x.TeacherSchoolSubject.SchoolSubject.Id == subjectId 
            && x.TeacherSchoolSubject.Teacher.Id == teacherId));

            logger.Info("Getting all students who have school subject with id {0} and who are taught by teacher with id {1}.", subjectId, teacherId);

            return students
                .Select(s => UserToUserDTOConverters.StudentToStudentWithParentDTO(s));
        }

        public IEnumerable<StudentWithParentDTO> GetAllBySchoolSubjectName(string subjectName)
        {
            if (subjectName == null)
            {
                throw new NullReferenceException();
            }

            var students = db.StudentsRepository.Get(s => s.SchoolClass.SchoolClassTeacherSchoolSubjects
            .Any(x => x.TeacherSchoolSubject.SchoolSubject.Name.ToLower().Contains(subjectName.ToLower())));

            if (students == null)
            {
                logger.Warn("No students of subject with that name {0}", subjectName);
                throw new KeyNotFoundException("Schoolsubject with that name not found.");
            }

            logger.Info("Getting all students having school subject containing {0}", subjectName);
            return students.Select(x => UserToUserDTOConverters.StudentToStudentWithParentDTO(x));
        }

        public IEnumerable<StudentWithParentDTO> GetAllBySchoolSubjectNameAndTeacherId(string subjectName, string teacherId)
        {
            if (subjectName == null)
            {
                throw new NullReferenceException();
            }

            var students = db.StudentsRepository.Get(s => s.SchoolClass.SchoolClassTeacherSchoolSubjects
            .Any(x => x.TeacherSchoolSubject.SchoolSubject.Name.ToLower().Contains(subjectName.ToLower())
            && x.TeacherSchoolSubject.Teacher.Id == teacherId));

            if (students == null)
            {
                logger.Warn("No students of subject with that name {0}", subjectName);
                throw new KeyNotFoundException("Schoolsubject with that name not found.");
            }

            logger.Info("Getting all students having school subject containing {0}", subjectName);
            return students.Select(x => UserToUserDTOConverters.StudentToStudentWithParentDTO(x));
        }

        public IEnumerable<StudentWithParentDTO> GetAllByTeacherSchoolSubjectId(int id)
        {
            //TODO 11.10 **DONE** exception if teacherschoolsubject nonexistant

            TeacherSchoolSubject ts = db.TeacherSchoolSubjectSRepository.GetByID(id);

            if (ts == null)
            {
                logger.Error("That teacher + school subject combination is nonexistant, id : {0}. Throwing ArgumentException.", id);
                throw new ArgumentException("TeacherSchoolSubjectId is not correct.", new ArgumentException());
            }
            var students = db.StudentsRepository
            .Get(s => s.SchoolClass.SchoolClassTeacherSchoolSubjects.Any(x => x.TeacherSchoolSubject.Id == id));
            logger.Info("Getting students taught by {0} {1}, subject: {2}. TeacherSchoolSubjectId : {3}",
                ts.Teacher.FirstName, ts.Teacher.LastName, ts.SchoolSubject.Name, id);

            return students
                .Select(s => UserToUserDTOConverters.StudentToStudentWithParentDTO(s));
        }

        public IEnumerable<StudentWithParentDTO> GetAllByTeacherSchoolSubjectIdAndTeacherId(int id, string teacherId)
        {
            TeacherSchoolSubject ts = db.TeacherSchoolSubjectSRepository.GetByID(id);
            var teacher = db.TeachersRepository.GetByID(teacherId);

            if (teacher == null)
            {
                logger.Warn("No teacher with id {0} found.", teacherId);
                throw new KeyNotFoundException("Teacher with that id not found.");
            }

            if (ts == null)
            {
                logger.Error("That teacher + school subject combination is nonexistant, id : {0}. Throwing ArgumentException.", id);
                throw new ArgumentException("TeacherSchoolSubjectId is not correct.", new ArgumentException());
            }

            var students = db.StudentsRepository
            .Get(s => s.SchoolClass.SchoolClassTeacherSchoolSubjects.Any(x => x.TeacherSchoolSubject.Id == id
            && x.TeacherSchoolSubject.Teacher.Id == teacherId));
            logger.Info("Getting students taught by {0} {1}, subject: {2}. TeacherSchoolSubjectId : {3}",
                ts.Teacher.FirstName, ts.Teacher.LastName, ts.SchoolSubject.Name, id);

            return students
                .Select(s => UserToUserDTOConverters.StudentToStudentWithParentDTO(s));
        }
    }
}