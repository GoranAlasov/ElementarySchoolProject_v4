using ElementarySchoolProject.Models;
using ElementarySchoolProject.Models.Users.UserDTOs;
using ElementarySchoolProject.Repositories;
using ElementarySchoolProject.Services.UsersServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElementarySchoolProject.Services.UsersServices
{
    public class StudentsService : IStudentsService
    {
        IUnitOfWork db;

        public StudentsService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IEnumerable<StudentSimpleViewDTO> GetAll()
        {
            return db.StudentsRepository.Get().Select(x => Utilities.UserToUserDTOConverters.StudentToStudentSimpleViewDTO(x));
        }

        public StudentWithGradesView GetById(string id)
        {
            Student student = db.StudentsRepository.GetByID(id);
            return Utilities.UserToUserDTOConverters.StudentToStudentWithGradesView(student);
        }        
    }
}