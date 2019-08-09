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


        [Authorize(Roles = "student, parent, teacher, admin")]
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
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal1);

                    case "teacher":
                        string teacherId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling teacher access level StudentsService GetAllByTeacherId method. Teacher ID: {0}", teacherId);
                        var retVal2 = service.GetAllByTeacherId(teacherId);
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal2);

                    case "parent":
                        string parentId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling parent access level StudentsService GetAllByParentId method. Parent ID: {0}", parentId);
                        var retVal3 = service.GetAllByParentId(parentId);
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal3);

                    case "student":
                        string userId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Caling student access level StudentsService method GetById. Student ID: {0}", userId);
                        StudentWithParentGradesClassDTO retVal4 = service.GetById(userId);
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal4);

                    default:
                        logger.Warn("BadRequest. There is no method for this role! {0}", role);
                        return BadRequest();
                }
            }
            catch (Exception e)
            {
                logger.Warn("Caught exception with message {0}. Returning bad request.", e.Message);
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "parent, teacher, admin")]
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
                        if (retVal1 == null)
                        {
                            return NotFound();
                        }
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal1);

                    case "teacher":
                        string teacherId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling teacher access level StudentsService method GetByIdAndTeacherId. Teacher ID: {0}", teacherId);
                        StudentWithParentGradesClassDTO retVal2 = service.GetByIdAndTeacherId(studentId, teacherId);
                        if (retVal2 == null)
                        {
                            return NotFound();
                        }
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal2);

                    case "parent":
                        string parentId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling parent access level StudentsService method GetByIdAndParentId. Parent ID: {0}", parentId);
                        StudentWithParentGradesClassDTO retVal3 = service.GetByIdAndParentId(studentId, parentId);
                        if (retVal3 == null)
                        {
                            return NotFound();
                        }
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal3);                    

                    default:
                        logger.Warn("BadRequest. There is no method for this role! {0}", role);
                        return BadRequest();
                }

            }
            catch (Exception e)
            {
                if (e is ArgumentException)
                {
                    logger.Warn("Caught exception with message {0}. Returning bad request.", e.Message);
                    return BadRequest(e.Message);
                }
                else
                {
                    logger.Warn("Caught exception with message {0}. Returning bad request.", e.Message);
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
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal1);

                    case "teacher":
                        string teacherId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling teacher access level StudentsService method GetAllByGradeDatesAndTeacherId. Teacher ID: {0}", teacherId);
                        var retVal2 = service.GetAllByGradeDatesAndTeacherId(d1, d2, teacherId);
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal2);

                    default:
                        logger.Warn("BadRequest. There is no method for this role! {0}", role);
                        return BadRequest();
                }
            }
            catch (Exception e)
            {
                logger.Warn("Caught exception with message {0}. Returning bad request.", e.Message);
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
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal1);

                    case "teacher":
                        string teacherId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling teacher access level StudentsService method GetAllBySchoolClassGradeAndTeaherId. Teacher ID: {0}", teacherId);
                        var retVal2 = service.GetAllBySchoolClassGradeAndTeacherId(grade, teacherId);
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal2);

                    default:
                        logger.Warn("BadRequest. There is no method for this role! {0}", role);
                        return BadRequest();
                }
            }
            catch (Exception e)
            {
                logger.Warn("Caught exception with message {0}. Returning bad request.", e.Message);
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
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal1);

                    case "teacher":
                        string teacherId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling teacher access level StudentsService method GetAllBySchoolClassIdAndTeacherId. Teacher ID: {0}", teacherId);
                        var retVal2 = service.GetAllBySchoolClassIdAndTeacherId(id, teacherId);
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal2);

                    default:
                        logger.Warn("BadRequest. There is no method for this role! {0}", role);
                        return BadRequest();
                }
            }
            catch (Exception e)
            {
                logger.Warn("Caught exception with message {0}. Returning bad request.", e.Message);
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
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal1);

                    case "teacher":
                        string teacherId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling teacher access level StudentsService method GetAllByStudentNameAndTeacherId. Teacher ID: {0}", teacherId);
                        var retVal2 = service.GetAllByStudentNameAndTeacherID(name, teacherId);
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal2);

                    default:
                        logger.Warn("BadRequest. There is no method for this role! {0}", role);
                        return BadRequest();
                }
            }
            catch (Exception e)
            {
                logger.Warn("Caught exception with message {0}. Returning bad request.", e.Message);
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "admin, teacher")]
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAllByTeacherName([FromUri]string teacherName)
        {
            string role = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == ClaimTypes.Role).Value;

            try
            {
                switch (role)
                {
                    case "admin":
                        string adminId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling admin access level StudentsService method GetAllByTeacherName. Admin ID: {0}", adminId);
                        var retVal1 = service.GetAllByTeacherName(teacherName);
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal1);

                    case "teacher":
                        string teacherId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling teacher access level StudentsService method GetAllByTeacherNameAndTeacherId. Teacher ID: {0}", teacherId);
                        var retVal2 = service.GetAllByStudentNameAndTeacherID(teacherName, teacherId);
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal2);

                    default:
                        logger.Warn("BadRequest. There is no method for this role! {0}", role);
                        return BadRequest();
                }
            }
            catch (Exception e)
            {
                logger.Warn("Caught exception with message {0}. Returning bad request.", e.Message);
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "admin, teacher")]
        [Route("subject/{subjectId}")]
        [HttpGet]
        public IHttpActionResult GetAllBySchoolSubjectId(int subjectId)
        {
            string role = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == ClaimTypes.Role).Value;

            try
            {
                switch (role)
                {
                    case "admin":
                        string adminId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling admin access level StudentsService method GetAllBySchoolSubjectId. Admin ID: {0}", adminId);
                        var retVal1 = service.GetAllBySchoolSubjectId(subjectId);
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal1);

                    case "teacher":
                        string teacherId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling teacher access level StudentsService method GetAllBySchoolSubjectIdAndTeacherId. Teacher ID: {0}", teacherId);
                        var retVal2 = service.GetAllBySchoolSubjectIdAndTeacherId(subjectId, teacherId);
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal2);

                    default:
                        logger.Warn("BadRequest. There is no method for this role! {0}", role);
                        return BadRequest();
                }
            }
            catch (Exception e)
            {
                logger.Warn("Caught exception with message {0}. Returning bad request.", e.Message);
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "admin, teacher")]
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAllBySchoolSubjectName([FromUri]string subjectName)
        {
            string role = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == ClaimTypes.Role).Value;

            try
            {
                switch (role)
                {
                    case "admin":
                        string adminId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling admin access level StudentsService method GetAllBySchoolSubjectName. Admin ID: {0}", adminId);
                        var retVal1 = service.GetAllBySchoolSubjectName(subjectName);
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal1);

                    case "teacher":
                        string teacherId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling teacher access level StudentsService method GetAllBySchoolSubjectNameAndTeacherId. Teacher ID: {0}", teacherId);
                        var retVal2 = service.GetAllBySchoolSubjectNameAndTeacherId(subjectName, teacherId);
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal2);

                    default:
                        logger.Warn("BadRequest. There is no method for this role! {0}", role);
                        return BadRequest();
                }
            }
            catch (Exception e)
            {
                logger.Warn("Caught exception with message {0}. Returning bad request.", e.Message);
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "admin, teacher")]
        [Route("teacherschoolsubject/{id}")]
        [HttpGet]
        public IHttpActionResult GetAllByTeacherSchoolSubjectId(int id)
        {
            string role = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == ClaimTypes.Role).Value;

            try
            {
                switch (role)
                {
                    case "admin":
                        string adminId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling admin access level StudentsService method GetAllByTeacherSchoolSubjectId. Admin ID: {0}", adminId);
                        var retVal1 = service.GetAllByTeacherSchoolSubjectId(id);
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal1);

                    case "teacher":
                        string teacherId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling teacher access level StudentsService method GetAllByTeacherSchoolSubjectIdAndTeacherId. Teacher ID: {0}", teacherId);
                        var retVal2 = service.GetAllByTeacherSchoolSubjectIdAndTeacherId(id, teacherId);
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal2);

                    default:
                        logger.Warn("BadRequest. There is no method for this role! {0}", role);
                        return BadRequest();
                }
            }
            catch (Exception e)
            {
                logger.Warn("Caught exception with message {0}. Returning bad request.", e.Message);
                return BadRequest(e.Message);
            }
        }        
    }
}
