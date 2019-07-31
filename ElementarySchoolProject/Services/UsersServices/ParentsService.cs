using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
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

        public IEnumerable<ParentSimpleViewDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public ParentSimpleViewDTO GetById(string id)
        {
            Parent parent = db.ParentsRepository.GetByID(id);

            if (parent == null || !(parent is Parent))
            {
                throw new KeyNotFoundException();
            }

            return Utilities.UserToUserDTOConverters.ParentToParentSimpleViewDTO(parent);
        }        
    }
}