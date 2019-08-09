using ElementarySchoolProject.Services.UsersServices;
using ElementarySchoolProject.Models.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NLog;
using System.Security.Claims;

namespace ElementarySchoolProject.Controllers
{
    [RoutePrefix("api/teachers")]
    public class TeachersController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        ITeachersService service;
        public TeachersController(ITeachersService service)
        {
            this.service = service;
        }


        //[AllowAnonymous]
        [Authorize(Roles = "admin, teacher")]
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllTeachers()
        {
            string role = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == ClaimTypes.Role).Value;

            try
            {
                switch (role)
                {
                    case "admin":
                        var retVal1 = service.GetAll();
                        logger.Info("Returning all teachers.");
                        return Ok(retVal1);

                    case "teacher":
                        string teacherId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        var retVal2 = service.GetById(teacherId);
                        logger.Info("Returning teacher with id {0} to self.", teacherId);
                        return Ok(retVal2);

                    default:
                        logger.Warn("BadRequest. There is no method for this role! {0}", role);
                        return BadRequest();
                }
            }
            catch (Exception e)
            {
                logger.Warn("Caught exception with message {0}", e.Message);
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        //[AllowAnonymous]
        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("subject/{subjectId}")]
        public IHttpActionResult GetAllTeachingASubject(int subjectId)
        {
            try
            {
                var retVal = service.GetAllTeachingASubject(subjectId);

                logger.Info("Returning all teachers teaching subject with id {0}", subjectId);
                return Ok(retVal);
            }
            catch (Exception e)
            {
                logger.Warn("Caught exception with message {0}", e.Message);
                return NotFound();
            }
        }

        //[AllowAnonymous]
        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("class/{classId}")]
        public IHttpActionResult GetAllTeachingToClass(int classId)
        {
            try
            {
                var retVal = service.GetAllTeachingToClass(classId);

                logger.Info("Returning all teachers teaching to class with id {0}", classId);
                return Ok(retVal);
            }
            catch (Exception e)
            {
                logger.Warn("Caught exception with message {0}", e.Message);
                return NotFound();
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("grade/{grade}")]
        public IHttpActionResult GetAllTeachingToGrade(int grade)
        {
            try
            {
                var retVal = service.GetAllTeachingToAGrade(grade);

                logger.Info("Returning all teachers teaching to grade {0}", grade);
                return Ok(retVal);
            }
            catch (Exception e)
            {
                logger.Warn("Caught exception with message {0}", e.Message);
                return NotFound();
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllTeachersByName([FromUri] string teacherName)
        {
            try
            {
                var retVal = service.GetAllByName(teacherName);

                logger.Info("Returning all teachers with name and/or surname containing \"{0}\"", teacherName);
                return Ok(retVal);
            }
            catch (Exception e)
            {
                logger.Warn("Caught exception with message {0}", e.Message);
                return NotFound();
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllTeachersBySubjectName([FromUri] string subjectName)
        {
            try
            {
                var retVal = service.GetAllBySubjectName(subjectName);

                logger.Info("Returning all teachers teaching any subjects with name  containing \"{0}\"", subjectName);
                return Ok(retVal);
            }
            catch (Exception e)
            {
                logger.Warn("Caught exception with message {0}", e.Message);
                return NotFound();
            }
        }

        //[AllowAnonymous]
        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllTeachingToStudent([FromUri]string studentId)
        {
            try
            {
                var retVal = service.GetAllTeachingToStudent(studentId);

                logger.Info("Returning all teachers teaching to student with id {0}", studentId);
                return Ok(retVal);
            }
            catch (Exception e)
            {
                logger.Warn("Caught exception with message {0}", e.Message);
                return NotFound();
            }            
        }

        //[AllowAnonymous]
        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetTeacherById(string id)
        {
            var retVal = service.GetById(id);

            if (retVal == null)
            {
                logger.Warn($"Teacher with id {id} not found. Bad request");
                return NotFound();
            }

            logger.Info("Returning teacher with id {0}", id);
            return Ok(retVal);
        }
    }
}
