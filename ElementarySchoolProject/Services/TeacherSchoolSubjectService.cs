using ElementarySchoolProject.Models;
using ElementarySchoolProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ElementarySchoolProject.Utilities;
using ElementarySchoolProject.Models.DTOs;
using NLog;

namespace ElementarySchoolProject.Services
{
    public class TeacherSchoolSubjectService : ITeacherSchoolSubjectsService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

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

        public TeacherSchoolSubjectDTO EditTeacherSchoolSubject(int id, TeacherSchoolSubjectCreateAndEditDTO dto)
        {
            TeacherSchoolSubject ts = db.TeacherSchoolSubjectSRepository.GetByID(id);

            if (ts != null)
            {
                ts.SchoolSubjectId = dto.SchoolSubjectId;
                ts.TeacherId = dto.TeacherId;

                db.TeacherSchoolSubjectSRepository.Update(ts);
                db.Save();                
            }

            return TeacherSchoolSubjectToTeacherSchoolSubjectDTOConverters.TeacherSchoolSubjectToTeacherSchoolSubjectDTO(ts);
        }

        public TeacherSchoolSubjectDTO DeleteTeacherSchoolSubject(int id)
        {
            TeacherSchoolSubject ts = db.TeacherSchoolSubjectSRepository.GetByID(id);

            db.TeacherSchoolSubjectSRepository.Delete(ts);
            db.Save();

            return TeacherSchoolSubjectToTeacherSchoolSubjectDTOConverters.TeacherSchoolSubjectToTeacherSchoolSubjectDTO(ts);
        }
    }
}