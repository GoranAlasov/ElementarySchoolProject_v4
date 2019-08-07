using ElementarySchoolProject.Models;
using ElementarySchoolProject.Models.DTOs;
using ElementarySchoolProject.Repositories;
using ElementarySchoolProject.Utilities;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElementarySchoolProject.Services
{
    public class SchoolClassTeacherSchoolSubjectService : ISchoolClassTeacherSchoolSubjectService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IUnitOfWork db;
        public SchoolClassTeacherSchoolSubjectService(IUnitOfWork db)
        {
            this.db = db;
        }

        public SchoolClassTeacherSchoolSubjectDTO CreateSchoolClassTeacherSchoolSubject(SchoolClassTeacherSchoolSubjectCreateAndEditDTO dto)
        {
            SchoolClass sc = db.SchoolClassesRepository.GetByID(dto.SchoolClassId);
            TeacherSchoolSubject tss = db.TeacherSchoolSubjectSRepository.GetByID(dto.TeacherSchoolSubjectId);

            SchoolClassTeacherSchoolSubject sctss = SchoolClassTeacherSchoolSubjectToSchoolClassTeacherSchoolSubjectDTOConverters
                .SchoolClassTeacherSchoolSubjectCreateAndEditDTOToSchoolClassTeacherSchoolSubject(sc, tss);

            db.SchoolClassTeacherSchoolSubjectRepository.Insert(sctss);
            db.Save();

            return SchoolClassTeacherSchoolSubjectToSchoolClassTeacherSchoolSubjectDTOConverters
                .SchoolClassTeacherSchoolSubjectTo_SchoolClassTeacherSchoolSubjectDTO(sctss);
        }

        public SchoolClassTeacherSchoolSubjectDTO DeleteSchoolClassTeacherSchoolSubject(int id)
        {
            SchoolClassTeacherSchoolSubject sctss = db.SchoolClassTeacherSchoolSubjectRepository.GetByID(id);

            db.SchoolClassTeacherSchoolSubjectRepository.Delete(sctss);
            db.Save();

            return SchoolClassTeacherSchoolSubjectToSchoolClassTeacherSchoolSubjectDTOConverters
                .SchoolClassTeacherSchoolSubjectTo_SchoolClassTeacherSchoolSubjectDTO(sctss);
        }

        public SchoolClassTeacherSchoolSubjectDTO EditSchoolClassTeacherSchoolSubject(int id, SchoolClassTeacherSchoolSubjectCreateAndEditDTO dto)
        {
            SchoolClassTeacherSchoolSubject sctss = db.SchoolClassTeacherSchoolSubjectRepository.GetByID(id);

            if (sctss != null)
            {
                sctss.SchoolClass.Id = dto.SchoolClassId;
                sctss.TeacherSchoolSubject.Id = dto.TeacherSchoolSubjectId;

                db.SchoolClassTeacherSchoolSubjectRepository.Update(sctss);
                db.Save();
            }

            return SchoolClassTeacherSchoolSubjectToSchoolClassTeacherSchoolSubjectDTOConverters
                .SchoolClassTeacherSchoolSubjectTo_SchoolClassTeacherSchoolSubjectDTO(sctss);
        }

        public IEnumerable<SchoolClassTeacherSchoolSubjectDTO> GetAll()
        {
            return db.SchoolClassTeacherSchoolSubjectRepository.Get()
                .Select(x => SchoolClassTeacherSchoolSubjectToSchoolClassTeacherSchoolSubjectDTOConverters
                .SchoolClassTeacherSchoolSubjectTo_SchoolClassTeacherSchoolSubjectDTO(x));
        }

        public SchoolClassTeacherSchoolSubjectDTO GetById(int id)
        {
            return SchoolClassTeacherSchoolSubjectToSchoolClassTeacherSchoolSubjectDTOConverters
                .SchoolClassTeacherSchoolSubjectTo_SchoolClassTeacherSchoolSubjectDTO
                (db.SchoolClassTeacherSchoolSubjectRepository.GetByID(id));
        }


    }
}