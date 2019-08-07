using ElementarySchoolProject.Models.DTOs.UserDTOs;
using ElementarySchoolProject.Services.UsersServices;
using NLog;
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
        private static Logger logger = LogManager.GetCurrentClassLogger();

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
                StudentWithParentGradesClassDTO retVal = service.GetById(userId);
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
                StudentWithParentGradesClassDTO retVal = service.GetByIdAndParentUserName(parentUserName, studentId);
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
        public IHttpActionResult GetStudentsByGradeDates([FromUri]DateTime d1, [FromUri]DateTime d2)
        {
            try
            {
                var retVal = service.GetAllByGradeDates(d1, d2);

                return Ok(retVal);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                
            }            
        }

        [Route("grade/{grade}")]
        public IHttpActionResult GetAllBySchoolClassGrade(int grade)
        {
            try
            {
                var retVal = service.GetAllBySchoolClassGrade(grade);

                return Ok(retVal);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("grade/{grade}/teacher/{teacherid}")]
        public IHttpActionResult GetAllGetAllBySchoolClassGradeAndTeacherId(int grade, string teacherId)
        {
            try
            {
                var retVal = service.GetAllBySchoolClassGradeAndTeacherId(grade, teacherId);

                return Ok(retVal);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("schoolclass/{id}")]
        public IHttpActionResult GetAllBySchoolClassId(int id)
        {
            try
            {
                var retVal = service.GetAllBySchoolClassId(id);

                return Ok(retVal);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("teacher/{id}")]
        public IHttpActionResult GetAllByTeacherId (string id)
        {
            try
            {
                var retVal = service.GetAllByTeacherId(id);

                return Ok(retVal);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("teacherschoolsubject/{id}")]
        public IHttpActionResult GetAllByTeacherSchoolSubject (int id)
        {
            try
            {
                var retVal = service.GetAllByTeacherSchoolSubjectId(id);

                return Ok(retVal);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("parent/{id}")]
        public IHttpActionResult GetAllByParentId(string id)
        {
            try
            {
                var retVal = service.GetAllByParentId(id);

                return Ok(retVal);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("{studentId}/setparent/{parentId}")]
        [HttpPut]
        public IHttpActionResult ChangeParent(string studentId, string parentId)
        {
            try
            {
                var retVal = service.ChangeParent(studentId, parentId);

                return Ok(retVal);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
