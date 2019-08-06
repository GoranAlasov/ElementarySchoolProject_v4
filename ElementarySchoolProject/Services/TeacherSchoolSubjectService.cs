using ElementarySchoolProject.Models;
using ElementarySchoolProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ElementarySchoolProject.Utilities;
using ElementarySchoolProject.Models.DTOs;

namespace ElementarySchoolProject.Services
{
    public class TeacherSchoolSubjectService : ITeacherSchoolSubjectsService
    {
        IUnitOfWork db;

        public TeacherSchoolSubjectService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IEnumerable<TeacherSchoolSubjectDTO> GetAll()
        {
            return db.TeacherSchoolSubjectSRepository.Get()
                .Select(x => TeacherSchoolSubjectToTeacherSchoolSubjectDTOConverters.TeacherSchoolSubjectToTeacherSchoolSubjectDTO(x));
        }

        public TeacherSchoolSubjectDTO GetById(int id)
        {
            return TeacherSchoolSubjectToTeacherSchoolSubjectDTOConverters.TeacherSchoolSubjectToTeacherSchoolSubjectDTO
                (
                db.TeacherSchoolSubjectSRepository.GetByID(id)
                );                
        }

        public TeacherSchoolSubjectDTO CreateTeacherSchoolSubject(TeacherSchoolSubjectCreateAndEditDTO dto)
        {
            Teacher teacher = db.TeachersRepository.GetByID(dto.TeacherId);
            SchoolSubject subject = db.SchoolSubjectsRepository.GetByID(dto.SchoolSubjectId);

            TeacherSchoolSubject tss = TeacherSchoolSubjectToTeacherSchoolSubjectDTOConverters.TeacherSchoolSubjectCreateAndEditDTOToTeacherSchoolSubject(teacher, subject);
            

            db.TeacherSchoolSubjectSRepository.Insert(tss);
            db.Save();

            return TeacherSchoolSubjectToTeacherSchoolSubjectDTOConverters.TeacherSchoolSubjectToTeacherSchoolSubjectDTO(tss);
        }
    }
}