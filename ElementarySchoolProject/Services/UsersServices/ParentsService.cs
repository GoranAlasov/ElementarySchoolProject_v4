using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ElementarySchoolProject.Models;
using ElementarySchoolProject.Models.Users.UserDTOs;
using ElementarySchoolProject.Repositories;

namespace ElementarySchoolProject.Services.UsersServices
{
    public class ParentsService : IParentsSerivce
    {
        IUnitOfWork db;

        public ParentsService(IUnitOfWork db)
        {
            this.db = db;
        }


        public ParentSimpleViewDTO GetById(string id)
        {
            Parent parent = db.ParentsRepository.GetByID(id);

            return Utilities.UserToUserDTOConverters.ParentToParentSimpleViewDTO(parent);
        }

        public StudentWithGradesView GetChildById(string parentUserName, string childId)
        {
            Student student = db.StudentsRepository.GetByID(childId);

            if (student == null)
            {
                throw new ArgumentException("No such student"); 
            }

            if (student.Parent.UserName != parentUserName)
            {
                throw new ArgumentException("That is not your child!");
            }

            return Utilities.UserToUserDTOConverters.StudentToStudentWithGradesView(student);
        }
    }
}