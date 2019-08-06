using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ElementarySchoolProject.Models;
using ElementarySchoolProject.Models.DTOs;
using ElementarySchoolProject.Repositories;
using ElementarySchoolProject.Utilities;

namespace ElementarySchoolProject.Services
{
    public class GradesService : IGradesService
    {
        private IUnitOfWork db;

        public GradesService(IUnitOfWork db)
        {
            this.db = db;
        }



        public GradeDTO CreateGrade(GradeCreateAndEditDTO dto)
        {
            Grade grade = GradeToGradeDTOConverters.GradeCreateAndEditDTOToGrade(dto);

            Student student = db.StudentsRepository.GetByID(dto.StudentId);
            TeacherSchoolSubject ts = db.TeacherSchoolSubjectSRepository.GetByID(dto.TeacherSubjectId);

            SchoolClass sc = db.SchoolClassesRepository.GetByID(student.SchoolClass.Id);

            //TODO 11.13: exception if Student is not correct type
            //TODO 11.14: exception if Student does not exist
            //TODO 11.15: exception if TeacherSchoolSubject does not exist
            //TODO 11.16: exception if ts.SchoolClasses.Any(x => x.Id != sc.Id)

            db.GradesRepository.Insert(grade);
            db.Save();

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
            TeacherSchoolSubject ts = db.TeacherSchoolSubjectSRepository.GetByID(dto.TeacherSubjectId);

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

        public GradeDTO GetAll()
        {
            throw new NotImplementedException();
        }

        public GradeDTO GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}