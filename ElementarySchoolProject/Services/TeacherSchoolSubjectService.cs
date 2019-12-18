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
            logger.Info("Getting all teacherschoolsubjects.");
            return db.TeacherSchoolSubjectSRepository.Get()
                .Select(x => TeacherSchoolSubjectToTeacherSchoolSubjectDTOConverters.TeacherSchoolSubjectToTeacherSchoolSubjectDTO(x));
        }

        public TeacherSchoolSubjectDTO GetById(int id)
        {
            logger.Info("Gettin teacherschoolsubject with id {0}.", id);

            var retVal = db.TeacherSchoolSubjectSRepository.GetByID(id);

            if (retVal == null)
            {
                throw new NullReferenceException();
            }

            return TeacherSchoolSubjectToTeacherSchoolSubjectDTOConverters.TeacherSchoolSubjectToTeacherSchoolSubjectDTO
                (
                retVal
                );                
        }

        public TeacherSchoolSubjectDTO CreateTeacherSchoolSubject(TeacherSchoolSubjectCreateAndEditDTO dto)
        {
            Teacher teacher = db.TeachersRepository.GetByID(dto.TeacherId);
            SchoolSubject subject = db.SchoolSubjectsRepository.GetByID(dto.SchoolSubjectId);

            if (teacher == null)
            {
                logger.Warn("No such teacher found. id {0}", dto.TeacherId);
                throw new KeyNotFoundException("No teacher with that id");
            }

            if (subject == null)
            {
                logger.Warn("No such subject found. id {0}", dto.SchoolSubjectId);
                throw new KeyNotFoundException("No subject with that id");
            }

            TeacherSchoolSubject tss = TeacherSchoolSubjectToTeacherSchoolSubjectDTOConverters.TeacherSchoolSubjectCreateAndEditDTOToTeacherSchoolSubject(teacher, subject);            

            db.TeacherSchoolSubjectSRepository.Insert(tss);
            db.Save();

            logger.Info("Creating new teacherschoolsubject. teacherId: {0}, subjectId: {1}", dto.TeacherId, dto.SchoolSubjectId);
            return TeacherSchoolSubjectToTeacherSchoolSubjectDTOConverters.TeacherSchoolSubjectToTeacherSchoolSubjectDTO(tss);
        }

        public TeacherSchoolSubjectDTO EditTeacherSchoolSubject(int id, TeacherSchoolSubjectCreateAndEditDTO dto)
        {
            TeacherSchoolSubject ts = db.TeacherSchoolSubjectSRepository.GetByID(id);

            if (ts != null)
            {
                var schoolSubject = db.SchoolSubjectsRepository.GetByID(dto.SchoolSubjectId);
                var teacher = db.TeachersRepository.GetByID(dto.TeacherId);

                ts.SchoolSubject = schoolSubject;
                ts.Teacher = teacher;

                db.TeacherSchoolSubjectSRepository.Update(ts);
                db.Save();
                logger.Info("Eddited teacherschoolsubject with id {0}", id);
            }

            return TeacherSchoolSubjectToTeacherSchoolSubjectDTOConverters.TeacherSchoolSubjectToTeacherSchoolSubjectDTO(ts);
        }

        public void DeleteTeacherSchoolSubject(int id)
        {
            TeacherSchoolSubject ts = db.TeacherSchoolSubjectSRepository.GetByID(id);

            foreach (var item in ts.SchoolClassTeacherSchoolSubjects)
            {
                logger.Info("Removing dependencies");
                item.TeacherSchoolSubject = null;
            }

            db.TeacherSchoolSubjectSRepository.Delete(ts);
            db.Save();
            logger.Info("Successfully deleted teacherschoolsubject.");

           
        }
    }
}