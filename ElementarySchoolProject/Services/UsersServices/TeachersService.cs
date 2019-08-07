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

            return retVal.Select(x => UserToUserDTOConverters.TeacherToTeacherBasicDTO(x));
        }

        public IEnumerable<TeacherBasicDTO> GetAllTeachingASubject(int id)
        {
            var retVal = db.TeachersRepository.Get()
                .Where(x => x.TeacherSchoolSubjects.Any(y => y.SchoolSubject.Id == id));

            return retVal.Select(x => UserToUserDTOConverters.TeacherToTeacherBasicDTO(x));
        }

        public IEnumerable<TeacherBasicDTO> GetAllTeachingToClass(int id)
        {
            var retVal = db.TeachersRepository.Get()
                .Where(x => x.TeacherSchoolSubjects.Any(y => y.SchoolClassTeacherSchoolSubjects.Any(z => z.SchoolClass.Id == id)));

            return retVal.Select(x => UserToUserDTOConverters.TeacherToTeacherBasicDTO(x));
        }

        public IEnumerable<TeacherBasicDTO> GetAllTeachingToStudent(string id)
        {
            var retVal = db.TeachersRepository.Get()
                .Where(x => x.TeacherSchoolSubjects.Any(y => y.SchoolClassTeacherSchoolSubjects.Any(z => z.SchoolClass.Students.Any(a => a.Id == id))));

            return retVal.Select(x => UserToUserDTOConverters.TeacherToTeacherBasicDTO(x));
        }

        public TeacherBasicDTO GetById(string id)
        {
            var retVal = db.TeachersRepository.GetByID(id);

            return UserToUserDTOConverters.TeacherToTeacherBasicDTO(retVal);
        }
    }
}