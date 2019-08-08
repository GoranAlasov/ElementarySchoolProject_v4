using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ElementarySchoolProject.Models.DTOs.UserDTOs;
using ElementarySchoolProject.Repositories;
using ElementarySchoolProject.Utilities;
using NLog;

namespace ElementarySchoolProject.Services.UsersServices
{
    public class TeachersService : ITeachersService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        IUnitOfWork db;
        public TeachersService(IUnitOfWork db)
        {
            this.db = db;
        }



        public IEnumerable<TeacherBasicDTO> GetAll()
        {
            var retVal = db.TeachersRepository.Get();

            logger.Info("Getting all teachers.");
            return retVal.Select(x => UserToUserDTOConverters.TeacherToTeacherBasicDTO(x));
        }

        public IEnumerable<TeacherBasicDTO> GetAllTeachingASubject(int id)
        {
            var retVal = db.TeachersRepository.Get()
                .Where(x => x.TeacherSchoolSubjects.Any(y => y.SchoolSubject.Id == id));

            if (retVal == null)
            {
                logger.Error("Subject with id {0} is non existant.", id);
                throw new ArgumentException("No subject with that id.");
            }

            logger.Info("Getting teachers teaching subject with id {0}", id);
            return retVal.Select(x => UserToUserDTOConverters.TeacherToTeacherBasicDTO(x));
        }

        public IEnumerable<TeacherBasicDTO> GetAllTeachingToAGrade(int grade)
        {
            var retVal = db.TeachersRepository.Get()
                .Where(x => x.TeacherSchoolSubjects.Any(y => y.SchoolClassTeacherSchoolSubjects.Any(z => z.SchoolClass.SchoolGrade == grade)));

            if (retVal == null)
            {
                logger.Error("No teachers assinged to {0} grade.", grade);
                throw new ArgumentException("No teachers teach to that grade.");
            }

            logger.Info("Getting teachers teaching to {0}th grade.", grade);
            return retVal.Select(x => UserToUserDTOConverters.TeacherToTeacherBasicDTO(x));
        }

        public IEnumerable<TeacherBasicDTO> GetAllTeachingToClass(int id)
        {
            var retVal = db.TeachersRepository.Get()
                .Where(x => x.TeacherSchoolSubjects.Any(y => y.SchoolClassTeacherSchoolSubjects.Any(z => z.SchoolClass.Id == id)));

            if (retVal == null)
            {
                logger.Error("Class with id {0} is non existant.", id);
                throw new ArgumentException("No class with that id.");
            }

            logger.Info("Getting teachers teaching to a class with id {0}", id);
            return retVal.Select(x => UserToUserDTOConverters.TeacherToTeacherBasicDTO(x));
        }

        public IEnumerable<TeacherBasicDTO> GetAllTeachingToStudent(string id)
        {
            var retVal = db.TeachersRepository.Get()
                .Where(x => x.TeacherSchoolSubjects.Any(y => y.SchoolClassTeacherSchoolSubjects.Any(z => z.SchoolClass.Students.Any(a => a.Id == id))));

            logger.Info("Getting all teachers teaching to student with id {0}.", id);
            return retVal.Select(x => UserToUserDTOConverters.TeacherToTeacherBasicDTO(x));
        }

        public TeacherBasicDTO GetById(string id)
        {
            var retVal = db.TeachersRepository.GetByID(id);

            logger.Info("Getting teacher with id {0}", id);
            return UserToUserDTOConverters.TeacherToTeacherBasicDTO(retVal);
        }
    }
}