using ElementarySchoolProject.Models;
using ElementarySchoolProject.Models.Users.UserDTOs;
using ElementarySchoolProject.Repositories;
using ElementarySchoolProject.Services.UsersServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ElementarySchoolProject.Utilities;

namespace ElementarySchoolProject.Services.UsersServices
{
    public class StudentsService : IStudentsService
    {
        IUnitOfWork db;

        public StudentsService(IUnitOfWork db)
        {
            this.db = db;
        }



        public IEnumerable<StudentSimpleViewDTO> GetAll()
        {
            return db.StudentsRepository.Get().Select(x => UserToUserDTOConverters.StudentToStudentSimpleViewDTO(x));
        }

        public IEnumerable<StudentSimpleViewDTO> GetAllByGradeDates(DateTime minValue, DateTime maxValue)
        {
            //TODO 11.1: exception if dates out of range
            var students = db.StudentsRepository
                .Get(s => s.Grades.Any(grade => grade.DateOfGrading >= minValue && grade.DateOfGrading <= maxValue));

            var retVal = students.Select(
                s => UserToUserDTOConverters.StudentToStudentSimpleViewDTO(s)
                );

            return retVal;
        }        

        public IEnumerable<StudentSimpleViewDTO> GetAllBySchoolClassGrade(int grade)
        {
            //TODO 11.2 exception if grade not between 1-8
            var students = db.StudentsRepository
                .Get(s => s.SchoolClass.SchoolGrade == grade);

            return students
                .Select(s => UserToUserDTOConverters.StudentToStudentSimpleViewDTO(s));
        }

        public IEnumerable<StudentSimpleViewDTO> GetAllBySchoolClassGradeAndTeacherId(int grade, string id)
        {
            //TODO 11.3 exception if grade not between 1-8
            //TODO 11.4 exception if teacher id nonexistant
            //TODO 11.5 exception if teacher id of another user type
            
            var students = db.StudentsRepository
                .Get(s => s.SchoolClass.SchoolGrade == grade && s.SchoolClass.TeacherSchoolSubjects.Any(ts => ts.Teacher.Id == id));

            return students
                .Select(s => UserToUserDTOConverters.StudentToStudentSimpleViewDTO(s));                
        }

        public IEnumerable<StudentSimpleViewDTO> GetAllBySchoolClassId(int id)
        {
            //TODO 11.6 exception if schoolClass id nonexistant

            var students = db.StudentsRepository
                .Get(s => s.SchoolClass.Id == id);

            return students
                .Select(s => UserToUserDTOConverters.StudentToStudentSimpleViewDTO(s));
        }

        public IEnumerable<StudentSimpleViewDTO> GetAllBySchoolSubjectId(int id)
        {
            //TODO 11.7 exception if school subject id nonexistant
            var students = db.StudentsRepository
                .Get(s => s.SchoolClass.TeacherSchoolSubjects.Any(ts => ts.SchoolSubject.Id == id));

            return students
                .Select(s => UserToUserDTOConverters.StudentToStudentSimpleViewDTO(s));
        }

        public IEnumerable<StudentSimpleViewDTO> GetAllByTeacherId(string id)
        {
            //TODO 11.8 exception if teacherid nonexistant
            //TODO 11.9 exception if teacherid of another user type
            var students = db.StudentsRepository
                .Get(s => s.SchoolClass.TeacherSchoolSubjects.Any(ts => ts.Teacher.Id == id));

            return students
                .Select(s => UserToUserDTOConverters.StudentToStudentSimpleViewDTO(s));
        }

        public IEnumerable<StudentSimpleViewDTO> GetAllByTeacherSchoolSubjectId(int id)
        {
            //TODO 11.10 exception if teacherschoolsubject nonexistant
            var students = db.StudentsRepository
                .Get(s => s.SchoolClass.TeacherSchoolSubjects.Any(ts => ts.Id == id));
                
            return students
                .Select(s => UserToUserDTOConverters.StudentToStudentSimpleViewDTO(s));
        }

        public StudentWithGradesView GetById(string id)
        {
            Student student = db.StudentsRepository.GetByID(id);

            if (student == null || !(student is Student))
            {
                throw new KeyNotFoundException();
            }

            return UserToUserDTOConverters.StudentToStudentWithGradesView(student);
        }

        public StudentWithGradesView GetByIdAndParentUserName(string parentUserName, string childId)
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

            return Utilities.UserToUserDTOConverters.StudentToStudentWithGradesView(student);
        }

        public IEnumerable<StudentSimpleViewDTO> GetAllByParentId(string parentId)
        {
            //TODO 11.11: exception if parentId nonexistant
            //TODO 11.12 exception if parentId is of other user type
            var students = db.StudentsRepository
                .Get(s => s.Parent.Id == parentId);

            return students
                .Select(s => UserToUserDTOConverters.StudentToStudentSimpleViewDTO(s));
        }
    }
}