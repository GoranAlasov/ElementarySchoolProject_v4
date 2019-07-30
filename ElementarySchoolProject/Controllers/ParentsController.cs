using ElementarySchoolProject.Models.Users.UserDTOs;
using ElementarySchoolProject.Services.UsersServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace ElementarySchoolProject.Controllers
{
    [RoutePrefix("api/parents")]
    public class ParentsController : ApiController
    {
        IParentsSerivce service;

        public ParentsController(IParentsSerivce service)
        {
            this.service = service;
        }


        [Authorize(Roles = "parent")]
        [Route("me")]
        [HttpGet]
        public IHttpActionResult GetMyself()
        {
            string userId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;

            ParentSimpleViewDTO retVal = service.GetById(userId);

            if (retVal == null)
            {
                return NotFound();
            }

            return Ok(retVal);
        }


        [Authorize(Roles = "parent")]
        [Route("student/{studentId}")]
        [HttpGet]
        public IHttpActionResult GetStudentById(string studentId)
        {
            string parentUserName = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserName").Value;

            try
            {
                StudentWithGradesView retVal = service.GetChildById(parentUserName, studentId);
                return Ok(retVal);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            
            //TODO 9: Add exception if wrong type of user found (Teacher, Admin, Parent)
        }
    }
}
