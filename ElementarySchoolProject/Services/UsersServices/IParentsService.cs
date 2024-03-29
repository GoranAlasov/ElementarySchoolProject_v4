﻿using ElementarySchoolProject.Models.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementarySchoolProject.Services.UsersServices
{
    public interface IParentsSerivce
    {
        IEnumerable<ParentSimpleViewDTO> GetAll();
        ParentSimpleViewDTO GetById(string id);
        IEnumerable<ParentSimpleViewDTO> GetAllByNumberOfChildren(int numberOfChildren);        
        IEnumerable<ParentSimpleViewDTO> GetAllByChildrenClass(int schoolClassId);
    }
}
