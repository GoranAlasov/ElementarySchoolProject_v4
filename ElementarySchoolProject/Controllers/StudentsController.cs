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
    [RoutePrefix("api/students")]
    public class StudentsController : ApiController
    {
        private IStudentsService service;

        public StudentsController(IStudentsService service)
        {
            this.service = service;
        }

        [Route("me")]
        [Authorize(Roles = "student")]
        [HttpGet]
        public IHttpActionResult GetMyself()
        {
            string userId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;

            StudentWithGradesView retVal = service.GetById(userId);

            if (retVal == null)
            {
                return NotFound();
            }

            return Ok(retVal);
        }

    }
}
