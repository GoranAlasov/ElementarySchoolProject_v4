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
                        string adminId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling admin access level StudentsService GetAll metod. Admin ID: {0}", adminId);
                        var retVal1 = service.GetAll();
                        return Ok(retVal1);

                    case "teacher":
                        string teacherId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling teacher access level StudentsService GetAllByTeacherId method. Teacher ID: {0}", teacherId);
                        var retVal2 = service.GetAllByTeacherId(teacherId);
                        return Ok(retVal2);

                    case "parent":
                        string parentId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling parent access level StudentsService GetAllByParentId method. Parent ID: {0}", parentId);
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
                        string adminId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling admin access level StudentsService method GetById. Admin ID: {0}", adminId);
                        StudentWithParentGradesClassDTO retVal1 = service.GetById(studentId);
                        return Ok(retVal1);

                    case "teacher":
                        string teacherId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling teacher access level StudentsService method GetByIdAndTeacherId. Teacher ID: {0}", teacherId);
                        StudentWithParentGradesClassDTO retVal2 = service.GetByIdAndTeacherId(studentId, teacherId);
                        return Ok(retVal2);

                    case "parent":
                        string parentId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling parent access level StudentsService method GetByIdAndParentId. Parent ID: {0}", parentId);
                        StudentWithParentGradesClassDTO retVal3 = service.GetByIdAndParentId(studentId, parentId);
                        return Ok(retVal3);

                    case "student":
                        string userId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Caling student access level StudentsService method GetById. Student ID: {0}", studentId);
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
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetStudentsByGradeDates([FromUri]DateTime d1, [FromUri]DateTime d2)
        {
            string role = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == ClaimTypes.Role).Value;

            try
            {
                switch (role)
                {
                    case "admin":
                        string adminId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling admin access level StudentsService method GetAllByGradeDates. Admin ID: {0}", adminId);
                        var retVal1 = service.GetAllByGradeDates(d1, d2);
                        return Ok(retVal1);

                    case "teacher":
                        string teacherId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling teacher access level StudentsService method GetAllByGradeDatesAndTeacherId. Teacher ID: {0}", teacherId);
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

        [Authorize(Roles = "teacher, admin")]
        [Route("schoolgrade/{grade}")]
        [HttpGet]
        public IHttpActionResult GetAllBySchoolClassGrade(int grade)
        {
            string role = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == ClaimTypes.Role).Value;

            try
            {
                switch (role)
                {
                    case "admin":
                        string adminId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling admin access level StudentsService method GetAllBySchoolClassGrade. Admin ID: {0}", adminId);
                        var retVal1 = service.GetAllBySchoolClassGrade(grade);
                        return Ok(retVal1);

                    case "teacher":
                        string teacherId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling teacher access level StudentsService method GetAllBySchoolClassGradeAndTeaherId. Teacher ID: {0}", teacherId);
                        var retVal2 = service.GetAllBySchoolClassGradeAndTeacherId(grade, teacherId);
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

        [Authorize(Roles = "admin, teacher")]
        [Route("schoolclass/{id}")]
        [HttpGet]
        public IHttpActionResult GetAllBySchoolClassId(int id)
        {
            string role = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == ClaimTypes.Role).Value;

            try
            {
                switch (role)
                {
                    case "admin":
                        string adminId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling admin access level StudentsService method GetAllBySchoolClassId. Admin ID: {0}", adminId);
                        var retVal1 = service.GetAllBySchoolClassId(id);
                        return Ok(retVal1);

                    case "teacher":
                        string teacherId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling teacher access level StudentsService method GetAllBySchoolClassIdAndTeacherId. Teacher ID: {0}", teacherId);
                        var retVal2 = service.GetAllBySchoolClassIdAndTeacherId(id, teacherId);
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

        [Authorize(Roles = "admin, teacher")]
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAllByName([FromUri]string name)
        {
            string role = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == ClaimTypes.Role).Value;

            try
            {
                switch (role)
                {
                    case "admin":
                        string adminId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling admin access level StudentsService method GetAllByStudentName. Admin ID: {0}", adminId);
                        var retVal1 = service.GetAllByStudentName(name);
                        return Ok(retVal1);

                    case "teacher":
                        string teacherId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling teacher access level StudentsService method GetAllByStudentNameAndTeacherId. Teacher ID: {0}", teacherId);
                        var retVal2 = service.GetAllByStudentNameAndTeacherID(name, teacherId);
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

        [Authorize(Roles = "admin, teacher")]
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAllByTeacherName([FromUri]string teacherName)
        {
            return null;
        }

        [Authorize(Roles = "admin, teacher")]
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAllBySchoolSubjectId([FromUri]string subjectName)
        {
            return null;
        }

        [Authorize(Roles = "admin, teacher")]
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAllBySchoolSubjectName([FromUri]string subjectName)
        {
            return null;
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
