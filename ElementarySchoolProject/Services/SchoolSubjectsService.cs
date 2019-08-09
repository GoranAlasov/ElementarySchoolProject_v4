using ElementarySchoolProject.Models;
using ElementarySchoolProject.Models.DTOs;
using ElementarySchoolProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ElementarySchoolProject.Utilities;
using NLog;

namespace ElementarySchoolProject.Services
{
    public class SchoolSubjectsService : ISchoolSubjectsService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

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

            foreach (var item in subject.TeacherSchoolSubjects)
            {
                item.SchoolSubject = null;
            }

            db.SchoolSubjectsRepository.Delete(id);
            db.Save();

            return SchoolSubjectToSchoolSubjectDTOConverters.SchoolSubjectToSchoolSubjectWithWeeklyClassesDTO(subject);
        }

        public SchoolSubjectWithWeeklyClassesDTO EditSchoolSubject(int id, SchoolSubjectCreateAndEditDTO dto)
        {
            SchoolSubject subject = db.SchoolSubjectsRepository.GetByID(id);

            if (subject != null)
            {
                subject.Name = dto.Name;
                subject.WeeklyClasses = dto.WeeklyClasses;

                db.SchoolSubjectsRepository.Update(subject);
                db.Save();                
            }

            return SchoolSubjectToSchoolSubjectDTOConverters.SchoolSubjectToSchoolSubjectWithWeeklyClassesDTO(subject);
        }

        public IEnumerable<SchoolSubjectWithWeeklyClassesAndTeachersDTO> GetAll()
        {
            return db.SchoolSubjectsRepository.Get()
                .Select(ss => SchoolSubjectToSchoolSubjectDTOConverters.SchoolSubjectToSchoolSubjectWithWeeklyClassesAndTeachersDTO(ss));
        }

        public IEnumerable<SchoolSubjectWithWeeklyClassesAndTeachersDTO> GetAllByTeacherId(string teacherId)
        {
            var subjects = db.SchoolSubjectsRepository.Get(x => x.TeacherSchoolSubjects.Any(y => y.Teacher.Id == teacherId));

            return subjects.Select(ss => SchoolSubjectToSchoolSubjectDTOConverters.SchoolSubjectToSchoolSubjectWithWeeklyClassesAndTeachersDTO(ss));
        }

        public SchoolSubjectWithWeeklyClassesAndTeachersDTO GetById(int id)
        {
            return SchoolSubjectToSchoolSubjectDTOConverters.SchoolSubjectToSchoolSubjectWithWeeklyClassesAndTeachersDTO(db.SchoolSubjectsRepository.GetByID(id));
        }
    }
}