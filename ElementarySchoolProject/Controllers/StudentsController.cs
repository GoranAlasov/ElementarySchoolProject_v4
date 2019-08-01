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

            try
            {
                StudentWithGradesView retVal = service.GetById(userId);
                return Ok(retVal);
            }
            catch (Exception)
            {
                return NotFound();
            }            
        }

        [Authorize(Roles = "parent")]
        [Route("{studentId}")]
        [HttpGet]
        public IHttpActionResult GetStudentByIdAndParentIdentity(string studentId)
        {
            string parentUserName = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserName").Value;

            try
            {
                StudentWithGradesView retVal = service.GetByIdAndParentUserName(parentUserName, studentId);
                return Ok(retVal);
            }
            catch (Exception e)
            {
                if (e is ArgumentException)
                {
                    return BadRequest(e.Message);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [AllowAnonymous]
        [Route("grade_dates")]
        public IHttpActionResult GetStudentsByGradeDates([FromUri]int d1, [FromUri]int m1, [FromUri]int y1, [FromUri]int d2, [FromUri]int m2, [FromUri]int y2)
        {
            DateTime date1 = new DateTime(y1, m1, d1);
            DateTime date2 = new DateTime(y2, m2, d2);

            var retVal = service.GetAllByGradeDates(date1, date2);

            return Ok(retVal);
        }

    }
}
