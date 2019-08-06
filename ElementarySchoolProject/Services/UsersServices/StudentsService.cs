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

namespace ElementarySchoolProject.Services.UsersServices
{
    public class StudentsService : IStudentsService
    {
        IUnitOfWork db;

        public StudentsService(IUnitOfWork db)
        {
            this.db = db;
        }



        public IEnumerable<StudentWithParentDTO> GetAll()
        {
            return db.StudentsRepository.Get().Select(x => UserToUserDTOConverters.StudentToStudentWithParentDTO(x));
        }

        public IEnumerable<StudentWithParentDTO> GetAllByGradeDates(DateTime minValue, DateTime maxValue)
        {            
            if (minValue < DateTime.MinValue || maxValue > DateTime.MaxValue || minValue > maxValue)
            {
                throw new DatesRangeException();
            }

            var students = db.StudentsRepository
                .Get(s => s.Grades.Any(grade => grade.DateOfGrading >= minValue && grade.DateOfGrading <= maxValue));

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
                throw new ArgumentOutOfRangeException("Grade must be between 1 and 8!", new ArgumentOutOfRangeException());
            }

            var students = db.StudentsRepository
                .Get(s => s.SchoolClass.SchoolGrade == grade);

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
                throw new ArgumentOutOfRangeException("Grade must be between 1 and 8!", new ArgumentOutOfRangeException());
            }

            if (id == null)
            {
                throw new ArgumentNullException("TeacherId must be specified.");
            }

            try
            {                
                Teacher teacher = db.TeachersRepository.GetByID(id);

                if (teacher == null || !(teacher is Teacher))
                {
                    throw new KeyNotFoundException("Teacher is missing or is of a wrong user type!");
                }                

                var students = db.StudentsRepository
                //.Get(s => s.SchoolClass.SchoolGrade == grade && s.SchoolClass.TeacherSchoolSubjects.Any(ts => ts.Teacher.Id == id));
                .Get(s => s.SchoolClass.SchoolGrade == grade &&
                s.SchoolClass.SchoolClassTeacherSchoolSubjects.Any(x => x.TeacherSchoolSubject.Teacher.Id == id));

                return students
                .Select(s => UserToUserDTOConverters.StudentToStudentWithParentDTO(s));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<StudentWithParentDTO> GetAllBySchoolClassId(int id)
        {
            //TODO 11.6 **DONE** exception if schoolClass id nonexistant

            var schoolClass = db.SchoolClassesRepository.GetByID(id);

            if (schoolClass == null)
            {
                throw new KeyNotFoundException("That SchoolClassId doesn't exist.");
            }

            var students = db.StudentsRepository
            .Get(s => s.SchoolClass.Id == id);

            return students
                .Select(s => UserToUserDTOConverters.StudentToStudentWithParentDTO(s));
        }

        public IEnumerable<StudentWithParentDTO> GetAllBySchoolSubjectId(int id)
        {
            //TODO 11.7 **DONE** exception if school subject id nonexistant

            var schoolSubject = db.SchoolClassesRepository.GetByID(id);

            if (schoolSubject == null)
            {
                throw new KeyNotFoundException("That SchoolSubjectId doesn't exist.");
            }
            var students = db.StudentsRepository
            //.Get(s => s.SchoolClass.TeacherSchoolSubjects.Any(ts => ts.SchoolSubject.Id == id));
            .Get(s => s.SchoolClass.SchoolClassTeacherSchoolSubjects.Any(x => x.TeacherSchoolSubject.SchoolSubject.Id == id));

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
                throw new ArgumentException("TeacherId is not correct.", new ArgumentException());
            }
            var students = db.StudentsRepository
            //.Get(s => s.SchoolClass.TeacherSchoolSubjects.Any(ts => ts.Teacher.Id == id));
            .Get(s => s.SchoolClass.SchoolClassTeacherSchoolSubjects.Any(x => x.TeacherSchoolSubject.Teacher.Id == id));

            return students
                .Select(s => UserToUserDTOConverters.StudentToStudentWithParentDTO(s));
        }

        public IEnumerable<StudentWithParentDTO> GetAllByTeacherSchoolSubjectId(int id)
        {
            //TODO 11.10 **DONE** exception if teacherschoolsubject nonexistant


            TeacherSchoolSubject ts = db.TeacherSchoolSubjectSRepository.GetByID(id);

            if (ts == null)
            {
                throw new ArgumentException("TeacherSchoolSubjectId is not correct.", new ArgumentException());
            }
            var students = db.StudentsRepository
            //.Get(s => s.SchoolClass.TeacherSchoolSubjects.Any(ts => ts.Id == id));
            .Get(s => s.SchoolClass.SchoolClassTeacherSchoolSubjects.Any(x => x.TeacherSchoolSubject.Id == id));

            return students
                .Select(s => UserToUserDTOConverters.StudentToStudentWithParentDTO(s));
        }

        public StudentWithParentGradesClassDTO GetById(string id)
        {
            Student student = db.StudentsRepository.GetByID(id);

            if (student == null || !(student is Student))
            {
                throw new KeyNotFoundException();
            }

            return UserToUserDTOConverters.StudentToStudentWithParentGradesClassDTO(student);
        }

        public StudentWithParentGradesClassDTO GetByIdAndParentUserName(string parentUserName, string childId)
        {
            Student student = db.StudentsRepository.GetByID(childId);

            if (student == null || !(student is Student))
            {
                throw new KeyNotFoundException();
            }

            if (student.Parent.UserName != parentUserName)
            {
                throw new ArgumentException("That is not your child!");
            }

            return Utilities.UserToUserDTOConverters.StudentToStudentWithParentGradesClassDTO(student);
        }

        public IEnumerable<StudentWithParentGradesClassDTO> GetAllByParentId(string parentId)
        {
            //TODO 11.11: **DONE** exception if parentId nonexistant
            //TODO 11.12: **DONE** exception if parentId is of other user type
            var parent = db.ParentsRepository.GetByID(parentId);

            if (parent == null)
            {
                throw new ArgumentException("That parent does not exist.");
            }

            var students = db.StudentsRepository
                .Get(s => s.Parent.Id == parentId);            

            return students
                .Select(s => UserToUserDTOConverters.StudentToStudentWithParentGradesClassDTO(s));
        }
    }
}