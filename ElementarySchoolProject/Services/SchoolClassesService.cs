using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ElementarySchoolProject.Models;
using ElementarySchoolProject.Models.DTOs;
using ElementarySchoolProject.Repositories;
using ElementarySchoolProject.Utilities;
using NLog;

namespace ElementarySchoolProject.Services
{
    public class SchoolClassesService : ISchoolClassesService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IUnitOfWork db;

        public SchoolClassesService(IUnitOfWork db)
        {
            this.db = db;
        }

        public SchoolClassDTO CreateSchoolClass(SchoolClassCreateAndEditDTO dto)
        {
            SchoolClass sc = SchoolClassToSchoolClassDTOConverters.SchoolClassCreateAndEditDTOToSchoolClass(dto);

            logger.Info("Creating new school class.");
            db.SchoolClassesRepository.Insert(sc);
            db.Save();

            return SchoolClassToSchoolClassDTOConverters.SchoolClassToSchoolClassDTO(sc);
        }

        public SchoolClassDTO DeleteSchoolClass(int id)
        {
            SchoolClass sc = db.SchoolClassesRepository.GetByID(id);

            logger.Info("Deleting school class with id {0}", id);
            db.SchoolClassesRepository.Delete(sc);
            db.Save();

            return SchoolClassToSchoolClassDTOConverters.SchoolClassToSchoolClassDTO(sc);
        }

        public SchoolClassDTO EditSchoolClass(int id, SchoolClassCreateAndEditDTO dto)
        {
            SchoolClass sc = db.SchoolClassesRepository.GetByID(id);

            if (sc != null)
            {
                sc.Name = dto.ClassName;
                sc.SchoolGrade = dto.SchoolGrade;

                logger.Info("Editing school class with id {0}", id);
                db.SchoolClassesRepository.Update(sc);
                db.Save();
            }            

            return SchoolClassToSchoolClassDTOConverters.SchoolClassToSchoolClassDTO(sc);
        }

        public IEnumerable<SchoolClassDTO> GetAll()
        {
            logger.Info("Getting all school classes.");
            return db.SchoolClassesRepository.Get()
                .Select(x => SchoolClassToSchoolClassDTOConverters.SchoolClassToSchoolClassDTO(x));
        }

        public IEnumerable<SchoolClassDTO> GetAllByTeacherId(string id)
        {
            var teacher = db.TeachersRepository.GetByID(id);

            if (teacher == null)
            {
                logger.Warn("Teacher with id {0} nonexistant. Throwing exception.", id);
                throw new KeyNotFoundException("No teacher with that id.");
            }

            logger.Info("Getting all school classes that teacher with id {0} teachers to.", id);
            return db.SchoolClassesRepository.Get().Where(sc => sc.SchoolClassTeacherSchoolSubjects.Any(y => y.TeacherSchoolSubject.Teacher.Id == id))
                .Select(x => SchoolClassToSchoolClassDTOConverters.SchoolClassToSchoolClassDTO(x));
        }


        public IEnumerable<SchoolClassDTO> GetBySchoolGrade(int grade)
        {
            IEnumerable<SchoolClass> sc = db.SchoolClassesRepository.Get(x => x.SchoolGrade == grade);

            logger.Info("Getting school classes by grade: {0}", grade);
            return sc.Select(x => SchoolClassToSchoolClassDTOConverters.SchoolClassToSchoolClassDTO(x));
        }

        public IEnumerable<SchoolClassDTO> GetBySchoolGradeAndTeacherId(int grade, string teacherId)
        {
            var teacher = db.TeachersRepository.GetByID(teacherId);

            if (teacher == null)
            {
                logger.Warn("Teacher with id {0} nonexistant. Throwing exception.", teacherId);
                throw new KeyNotFoundException("No teacher with that id.");
            }

            logger.Info("Getting school classes by grade: {0} and teacher id {1}", grade, teacherId);
            IEnumerable<SchoolClass> sc = db.SchoolClassesRepository.Get(x => x.SchoolGrade == grade 
            && x.SchoolClassTeacherSchoolSubjects.Any(y => y.TeacherSchoolSubject.Teacher.Id == teacherId));

            return sc.Select(x => SchoolClassToSchoolClassDTOConverters.SchoolClassToSchoolClassDTO(x));
        }


        public SchoolClassDetailsDTO GetById(int id)
        {
            logger.Info("Getting school class with id {0}", id);

            return SchoolClassToSchoolClassDTOConverters.SchoolClassToSchoolClassDetailsDTO(db.SchoolClassesRepository.GetByID(id));
        }

        public SchoolClassDetailsDTO GetByIdAndTeacherId(int id, string teacherId)
        {
            var teacher = db.TeachersRepository.GetByID(teacherId);            

            if (teacher == null)
            {
                logger.Warn("Teacher with id {0} nonexistant. Throwing exception.", teacherId);
                throw new KeyNotFoundException("No teacher with that id.");
            }

            var teacherClasses = db.SchoolClassesRepository.Get(x => x.SchoolClassTeacherSchoolSubjects.Any(y => y.TeacherSchoolSubject.Teacher.Id == teacherId));

            if (teacherClasses == null)
            {
                logger.Warn("Teacher with id {0} doesnt teach any classes. Throwing exception.", teacherId);
                throw new KeyNotFoundException("Teacher nto teaching any classes.");
            }

            logger.Info("Getting school class with id {0} and teacher id {1}.", id, teacherId);
            SchoolClass sc = teacherClasses.Where(x => x.Id == id).FirstOrDefault();

            return SchoolClassToSchoolClassDTOConverters.SchoolClassToSchoolClassDetailsDTO(sc);
        }
    }
}