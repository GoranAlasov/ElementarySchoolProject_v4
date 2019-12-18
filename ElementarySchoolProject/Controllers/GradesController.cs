using ElementarySchoolProject.Models.DTOs;
using ElementarySchoolProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ElementarySchoolProject.Utilities;
using NLog;
using System.Security.Claims;

namespace ElementarySchoolProject.Controllers
{
    [RoutePrefix("api/grades")]
    public class GradesController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        IGradesService service;
        public GradesController(IGradesService service)
        {
            this.service = service;
        }

        [Authorize(Roles = "admin, teacher, parent, student")]
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
                        logger.Info("Calling admin access level GradesService GetAll metod. Admin ID: {0}", adminId);
                        var retVal1 = service.GetAll();
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal1);

                    case "teacher":
                        string teacherId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling teacher access level GradesService GetAllByTeacherId method. Teacher ID: {0}", teacherId);
                        var retVal2 = service.GetAllByTeacherId(teacherId);
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal2);

                    case "parent":
                        string parentId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling parent access level GradesService GetAllByParentId method. Parent ID: {0}", parentId);
                        var retVal3 = service.GetAllByParentId(parentId);
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal3);

                    case "student":
                        string userId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Caling student access level GradesService method GetAllByStudentId. Student ID: {0}", userId);
                        var retVal4 = service.GetAllByStudentId(userId);
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

        [Authorize(Roles = "admin, teacher, parent")]
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAllByStudentName([FromUri] string firstName, [FromUri] string lastName)
        {
            string role = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == ClaimTypes.Role).Value;

            try
            {
                switch (role)
                {
                    case "admin":
                        string adminId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling admin access level GradesService GetAllByStudentName metod. Admin ID: {0}", adminId);
                        var retVal1 = service.GetAllByStudentName(firstName, lastName);
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal1);

                    case "teacher":
                        string teacherId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling teacher access level GradesService GetAllByStudentNameAndTeacherId method. Teacher ID: {0}", teacherId);
                        var retVal2 = service.GetAllByStudentNameAndTeacherId(firstName, lastName, teacherId);
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal2);

                    case "parent":
                        string parentId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling parent access level GradesService GetAllByStudentNameAndParentId method. Parent ID: {0}", parentId);
                        var retVal3 = service.GetAllByStudentNameAndParentId(firstName, lastName, parentId);
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal3);

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

        [Authorize(Roles = "admin, teacher, parent, student")]
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAllByValueRange([FromUri] int min, [FromUri] int max)
        {
            string role = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == ClaimTypes.Role).Value;

            try
            {
                switch (role)
                {
                    case "admin":
                        string adminId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling admin access level GradesService GetAllByValueRange metod. Admin ID: {0}", adminId);
                        var retVal1 = service.GetAllByValueRange(min, max);
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal1);

                    case "teacher":
                        string teacherId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling teacher access level GradesService GetAllByValueRangeAndTeacherId method. Teacher ID: {0}", teacherId);
                        var retVal2 = service.GetAllByValueRangeAndTeacherId(min, max, teacherId);
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal2);

                    case "parent":
                        string parentId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling parent access level GradesService GetAllByValueRangeAndParentId method. Parent ID: {0}", parentId);
                        var retVal3 = service.GetAllByValueRangeAndParentId(min, max, parentId);
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal3);

                    case "student":
                        string userId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Caling student access level GradesService method GetAllByValueRangeAndStudentId. Student ID: {0}", userId);
                        var retVal4 = service.GetAllByValueRangeAndStudentId(min, max, userId);
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

        [Authorize(Roles = "admin, teacher, parent, student")]
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAllByGradingDateRange([FromUri] DateTime minDate, [FromUri] DateTime maxDate)
        {
            string role = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == ClaimTypes.Role).Value;

            try
            {
                switch (role)
                {
                    case "admin":
                        string adminId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling admin access level GradesService GetAllByGradingDateRange metod. Admin ID: {0}", adminId);
                        var retVal1 = service.GetAllByGradingDateRange(minDate, maxDate);
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal1);

                    case "teacher":
                        string teacherId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling teacher access level GradesService GetAllByGradignDateRangeAndTeacherId method. Teacher ID: {0}", teacherId);
                        var retVal2 = service.GetAllByGradignDateRangeAndTeacherId(minDate, maxDate, teacherId);
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal2);

                    case "parent":
                        string parentId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling parent access level GradesService GetAllByGradingDateRangeAndParentId method. Parent ID: {0}", parentId);
                        var retVal3 = service.GetAllByGradingDateRangeAndParentId(minDate, maxDate, parentId);
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal3);

                    case "student":
                        string userId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Caling student access level GradesService method GetAllByGradingDateRangeAndStudentId. Student ID: {0}", userId);
                        var retVal4 = service.GetAllByGradingDateRangeAndStudentId(minDate, maxDate, userId);
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

        [Authorize(Roles = "admin, teacher, parent, student")]
        [Route("subject/{id}")]
        [HttpGet]
        public IHttpActionResult GetAllBySchoolSubjectId(int id)
        {
            string role = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == ClaimTypes.Role).Value;

            try
            {
                switch (role)
                {
                    case "admin":
                        string adminId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling admin access level GradesService GetAllBySchoolSubjectId metod. Admin ID: {0}", adminId);
                        var retVal1 = service.GetAllBySchoolSubjectId(id);
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal1);

                    case "teacher":
                        string teacherId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling teacher access level GradesService GetAllBySchoolSubjectIdAndTeacherId method. Teacher ID: {0}", teacherId);
                        var retVal2 = service.GetAllBySchoolSubjectIdAndTeacherId(id, teacherId);
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal2);

                    case "parent":
                        string parentId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling parent access level GradesService GetAllBySchoolSubjectIdAndParentId method. Parent ID: {0}", parentId);
                        var retVal3 = service.GetAllBySchoolSubjectIdAndParentId(id, parentId);
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal3);

                    case "student":
                        string userId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Caling student access level GradesService method GetAllBySchoolSubjectIdAndStudentId. Student ID: {0}", userId);
                        var retVal4 = service.GetAllBySchoolSubjectIdAndStudentId(id, userId);
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

        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("student/{id}/avg")]
        public IHttpActionResult GetAvgGradeByStudent(string id)
        {
            var avg = service.AverageGradeByStudentId(id);

            logger.Info("Got average grade of student id {0}", id);
            return Ok(avg);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("student/{studentId}/subject/{subjectId}/avg")]
        public IHttpActionResult GetAvgGradeByStudentAndSubject(string studentId, int subjectId)
        {
            var avg = service.AverageGradeByStudentIdAndSubjectId(studentId, subjectId);

            logger.Info("Got average grade of student id {0} for subject id {2}", studentId, subjectId);
            return Ok(avg);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("{teacherId}")]
        public IHttpActionResult CreateGradeAdmin(string teacherId, [FromBody]GradeCreateAndEditDTO dto)
        {
            if (!ModelState.IsValid)
            {
                logger.Warn("Bad model state.");
                return BadRequest();
            }

            try
            {
                GradeDTO retVal = service.CreateGrade(teacherId, dto);

                logger.Info("Admin creted grade. status OK");
                return Ok(retVal);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Authorize(Roles = "teacher")]
        [HttpPost]
        [Route("grading")]
        public IHttpActionResult CreateGradeTeacher([FromBody]GradeCreateAndEditDTO dto)
        {
            string id = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;

            if (!ModelState.IsValid)
            {
                logger.Warn("Bad model state.");
                return BadRequest();
            }

            try
            {
                GradeDTO retVal = service.CreateGrade(id, dto);

                logger.Info("Teacher creted grade. status OK");
                return Ok(retVal);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        [Route("{id}/teacher/{teacherId}")]
        public IHttpActionResult EditGradeAdmin(int id, string teacherId, [FromBody]GradeCreateAndEditDTO dto)
        {
            if (!ModelState.IsValid)
            {
                logger.Warn("Bad model state.");
                return BadRequest();
            }

            try
            {
                GradeDTO retVal = service.EditGrade(id, teacherId, dto);

                logger.Info("Admin edited grade. status OK");
                return Ok(retVal);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Authorize(Roles = "teacher")]
        [HttpPut]
        [Route("grading/{id}")]
        public IHttpActionResult EditGradeTeacher(int id, [FromBody]GradeCreateAndEditDTO dto)
        {
            string teacherId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;

            if (!ModelState.IsValid)
            {
                logger.Warn("Bad model state.");
                return BadRequest();
            }

            try
            {
                GradeDTO retVal = service.EditGrade(id, teacherId, dto);

                logger.Info("Teacher edited grade. status OK");
                return Ok(retVal);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult DeleteGrade(int id)
        {
            try
            {
                GradeDTO retVal = service.DeleteGrade(id);

                logger.Info("Admin deleted grade");
                return Ok(retVal);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
