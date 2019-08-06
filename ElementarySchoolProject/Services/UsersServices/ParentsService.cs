using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ElementarySchoolProject.Models;
using ElementarySchoolProject.Models.DTOs.UserDTOs;
using ElementarySchoolProject.Repositories;
using ElementarySchoolProject.Utilities;

namespace ElementarySchoolProject.Services.UsersServices
{
    public class ParentsService : IParentsSerivce
    {
        IUnitOfWork db;

        public ParentsService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IEnumerable<ParentSimpleViewDTO> GetAll()
        {
            var parents = db.ParentsRepository.Get();

            return parents
                .Select(x => UserToUserDTOConverters.ParentToParentSimpleViewDTO(x));
        }

        public IEnumerable<ParentSimpleViewDTO> GetAllByChildrenClass(int schoolClassId)
        {
            //TODO 11.13: exception if school class id nonexistant
            var parents = db.ParentsRepository
                .Get(p => p.Students.Any(s => s.SchoolClass.Id == schoolClassId));

            return parents
                .Select(x => UserToUserDTOConverters.ParentToParentSimpleViewDTO(x));
        }

        public IEnumerable<ParentSimpleViewDTO> GetAllByChildrenGradeRange(int gradeLow, int gradeHigh)
        {
            //TODO 11.14: exception if grade not in range 1-8
            //TODO 11.15: TEST MISSING PARAMETERS EXCEPTION EVERYWHERE!!

            var parents = db.ParentsRepository
                .Get(p => p.Students.Any(s => s.SchoolClass.SchoolGrade >= gradeLow && s.SchoolClass.SchoolGrade <= gradeHigh));

            return parents
                .Select(x => UserToUserDTOConverters.ParentToParentSimpleViewDTO(x));
        }

        public IEnumerable<ParentSimpleViewDTO> GetAllByNumberOfChildren(int numberOfChildern)
        {
            //TODO 11.16 exception if number of children < 1

            var parents = db.ParentsRepository
                .Get(p => p.Students.Count() == numberOfChildern);

            return parents.
                Select(x => UserToUserDTOConverters.ParentToParentSimpleViewDTO(x));
        }

        public ParentSimpleViewDTO GetById(string id)
        {
            Parent parent = db.ParentsRepository.GetByID(id);

            if (parent == null || !(parent is Parent))
            {
                throw new KeyNotFoundException();
            }

            return UserToUserDTOConverters.ParentToParentSimpleViewDTO(parent);
        }        
    }
}