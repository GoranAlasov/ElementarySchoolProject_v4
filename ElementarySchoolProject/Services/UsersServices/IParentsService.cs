using ElementarySchoolProject.Models.Users.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementarySchoolProject.Services.UsersServices
{
    public interface IParentsSerivce
    {
        ParentSimpleViewDTO GetById(string id);
        StudentWithGradesView GetChildById(string parentId, string childId);
    }
}
