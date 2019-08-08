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

        [Authorize(Roles = "parent, teacher, admin")]
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            string role = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == ClaimTypes.Role).Value;

            try
            {
                switch (role)
                {
                    case "admin":
                        var retVal1 = service.GetAll();
                        return Ok(retVal1);

                    case "teacher":
                        string teacherId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        var retVal2 = service.GetAllByTeacherId(teacherId);
                        return Ok(retVal2);

                    case "parent":
                        string parentId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        var retVal3 = service.GetAllByParentId(parentId);
                        return Ok(retVal3);                    

                    default:
                        return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "parent, student, teacher, admin")]        
        [Route("{studentId}")]
        [HttpGet]
        public IHttpActionResult GetStudentById(string studentId)
        {            
            string role = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == ClaimTypes.Role).Value;            

            try
            {
                switch (role)
                {
                    case "admin":
                        StudentWithParentGradesClassDTO retVal1 = service.GetById(studentId);
                        return Ok(retVal1);

                    case "teacher":
                        string teacherId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        StudentWithParentGradesClassDTO retVal2 = service.GetByIdAndTeacherId(studentId, teacherId);
                        return Ok(retVal2);

                    case "parent":
                        string parentId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        StudentWithParentGradesClassDTO retVal3 = service.GetByIdAndParentId(studentId, parentId);
                        return Ok(retVal3);

                    case "student":
                        string userId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        StudentWithParentGradesClassDTO retVal4 = service.GetById(userId);
                        return Ok(retVal4);

                    default:
                        return BadRequest();
                }
                
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
        
        [Authorize(Roles = "teacher, admin")]
        [Route("grade_dates")]
        [HttpGet]
        public IHttpActionResult GetStudentsByGradeDates([FromUri]DateTime d1, [FromUri]DateTime d2)
        {
            string role = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == ClaimTypes.Role).Value;

            try
            {
                switch (role)
                {
                    case "admin":
                        var retVal1 = service.GetAllByGradeDates(d1, d2);
                        return Ok(retVal1);

                    case "teacher":
                        string teacherId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        var retVal2 = service.GetAllByGradeDatesAndTeacherId(d1, d2, teacherId);
                        return Ok(retVal2);                    

                    default:
                        return BadRequest();
                }
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
