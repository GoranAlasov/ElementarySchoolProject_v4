using ElementarySchoolProject.Models;
using ElementarySchoolProject.Models.DTOs;
using ElementarySchoolProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ElementarySchoolProject.Utilities;

namespace ElementarySchoolProject.Services
{
    public class SchoolSubjectsService : ISchoolSubjectsService
    {
        IUnitOfWork db;

        public SchoolSubjectsService(IUnitOfWork db)
        {
            this.db = db;
        }




        public SchoolSubjectWithWeeklyClassesDTO CreateSchoolSubject(SchoolSubjectCreateAndEditDTO dto)
        {
            SchoolSubject subject = SchoolSubjectToSchoolSubjectDTOConverters.SchoolSubjectCreateAndEditDTOToSchoolSubject(dto);

            db.SchoolSubjectsRepository.Insert(subject);
            db.Save();
            //TODO 12: check if the returned object has ID!!

            return SchoolSubjectToSchoolSubjectDTOConverters.SchoolSubjectToSchoolSubjectWithWeeklyClassesDTO(subject);
        }

        public SchoolSubjectWithWeeklyClassesDTO DeleteSchoolSubject(int id)
        {
            SchoolSubject subject = db.SchoolSubjectsRepository.GetByID(id);

            db.SchoolSubjectsRepository.Delete(id);
            db.Save();

            return SchoolSubjectToSchoolSubjectDTOConverters.SchoolSubjectToSchoolSubjectWithWeeklyClassesDTO(subject);
        }

        public SchoolSubjectWithWeeklyClassesDTO EditSchoolSubject(int id, SchoolSubjectCreateAndEditDTO dto)
        {
            SchoolSubject subject = db.SchoolSubjectsRepository.GetByID(id);

            subject = SchoolSubjectToSchoolSubjectDTOConverters.SchoolSubjectCreateAndEditDTOToSchoolSubject(dto);

            db.SchoolSubjectsRepository.Update(subject);
            db.Save();

            return SchoolSubjectToSchoolSubjectDTOConverters.SchoolSubjectToSchoolSubjectWithWeeklyClassesDTO(subject);
        }

        public IEnumerable<SchoolSubjectWithWeeklyClassesDTO> GetAll()
        {
            return db.SchoolSubjectsRepository.Get()
                .Select(ss => SchoolSubjectToSchoolSubjectDTOConverters.SchoolSubjectToSchoolSubjectWithWeeklyClassesDTO(ss));
        }

        public SchoolSubjectWithWeeklyClassesDTO GetById(int id)
        {
            return SchoolSubjectToSchoolSubjectDTOConverters.SchoolSubjectToSchoolSubjectWithWeeklyClassesDTO(db.SchoolSubjectsRepository.GetByID(id));
        }
    }
}