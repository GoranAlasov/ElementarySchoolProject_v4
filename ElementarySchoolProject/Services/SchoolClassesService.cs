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
    public class SchoolClassesService : ISchoolClassesService
    {
        private IUnitOfWork db;

        public SchoolClassesService(IUnitOfWork db)
        {
            this.db = db;
        }

        public SchoolClassDTO CreateSchoolClass(SchoolClassCreateAndEditDTO dto)
        {
            SchoolClass sc = SchoolClassToSchoolClassDTOConverters.SchoolClassCreateAndEditDTOToSchoolClass(dto);

            db.SchoolClassesRepository.Insert(sc);
            db.Save();

            return SchoolClassToSchoolClassDTOConverters.SchoolClassToSchoolClassDTO(sc);
        }

        public SchoolClassDTO DeleteSchoolClass(int id)
        {
            SchoolClass sc = db.SchoolClassesRepository.GetByID(id);

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

                db.SchoolClassesRepository.Update(sc);
                db.Save();
            }            

            return SchoolClassToSchoolClassDTOConverters.SchoolClassToSchoolClassDTO(sc);
        }

        public IEnumerable<SchoolClassDTO> GetAll()
        {
            return db.SchoolClassesRepository.Get()
                .Select(x => SchoolClassToSchoolClassDTOConverters.SchoolClassToSchoolClassDTO(x));
        }

        public IEnumerable<SchoolClassDTO> GetBySchoolGrade(int grade)
        {
            IEnumerable<SchoolClass> sc = db.SchoolClassesRepository.Get(x => x.SchoolGrade == grade);

            return sc.Select(x => SchoolClassToSchoolClassDTOConverters.SchoolClassToSchoolClassDTO(x));
        }

        public SchoolClassDetailsDTO GetById(int id)
        {
            return SchoolClassToSchoolClassDTOConverters.SchoolClassToSchoolClassDetailsDTO(db.SchoolClassesRepository.GetByID(id));
        }
    }
}