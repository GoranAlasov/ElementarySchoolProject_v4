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
using NLog;

namespace ElementarySchoolProject.Services.UsersServices
{
    public class ParentsService : IParentsSerivce
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        IUnitOfWork db;

        public ParentsService(IUnitOfWork db)
        {
            this.db = db;
        }



        public IEnumerable<ParentSimpleViewDTO> GetAll()
        {
            var parents = db.ParentsRepository.Get();
            logger.Info("Getting all entries of type {0}", parents.GetType());
        
            return parents
                .Select(x => UserToUserDTOConverters.ParentToParentSimpleViewDTO(x));
        }

        public IEnumerable<ParentSimpleViewDTO> GetAllByChildrenClass(int schoolClassId)
        {
            //TODO 11.13: **DONE** exception if school class id nonexistant
            var sc = db.SchoolClassesRepository.GetByID(schoolClassId);            

            if (sc == null)
            {
                logger.Error("schoolClassId with value {0} not found. Throwing KeyNotFoundException.", schoolClassId);
                throw new KeyNotFoundException("That school class does not exist.");
            }

            var parents = db.ParentsRepository
                .Get(p => p.Students.Any(s => s.SchoolClass.Id == schoolClassId));
            logger.Info("Getting parents with childern schoolClassId {0}.", schoolClassId);

            return parents
                .Select(x => UserToUserDTOConverters.ParentToParentSimpleViewDTO(x));
        }        

        public IEnumerable<ParentSimpleViewDTO> GetAllByNumberOfChildren(int numberOfChildern)
        {
            //TODO 11.16 **DONE** exception if number of children < 1
            if (numberOfChildern < 1)
            {
                logger.Error("Cannot have {0} children! Throwing ArgumentOutOfRangeException.", numberOfChildern);

                throw new ArgumentOutOfRangeException("Can't have less than 1 child!", new ArgumentOutOfRangeException());
            }

            var parents = db.ParentsRepository
                .Get(p => p.Students.Count() == numberOfChildern);
            logger.Info("Getting parents with {0} children.", numberOfChildern);

            return parents.
                Select(x => UserToUserDTOConverters.ParentToParentSimpleViewDTO(x));
        }

        public ParentSimpleViewDTO GetById(string id)
        {
            Parent parent = db.ParentsRepository.GetByID(id);
            logger.Info("Getting parent with id {0}.", id);

            if (parent == null || !(parent is Parent))
            {
                logger.Error("Parent with id {0} does not exist. Throwing key not found exception.", id);
                throw new KeyNotFoundException();
            }

            return UserToUserDTOConverters.ParentToParentSimpleViewDTO(parent);
        }        
    }
}