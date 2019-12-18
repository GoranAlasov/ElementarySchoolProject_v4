using ElementarySchoolProject.Services;
using ElementarySchoolProject.Models.DTOs;
using ElementarySchoolProject.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using NLog;
using System.Security.Claims;

namespace ElementarySchoolProject.Controllers
{
    [RoutePrefix("api/schoolclasses")]
    public class SchoolClassesController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        ISchoolClassesService service;
        public SchoolClassesController(ISchoolClassesService service)
        {
            this.service = service;
        }


        //GET: api/schoolclasses
        [Authorize(Roles = "admin, teacher")]
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAllSchoolClasses()
        {
            string role = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == ClaimTypes.Role).Value;

            try
            {
                switch (role)
                {
                    case "admin":
                        string adminId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling admin access level SchoolClassesService method GetAll. Admin ID: {0}", adminId);
                        var retVal1 = service.GetAll();
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal1);

                    case "teacher":
                        string teacherId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling teacher access level SchoolClassesService method GetAllByTeacherId. Teacher ID: {0}", teacherId);
                        var retVal2 = service.GetAllByTeacherId(teacherId);
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

        //GET: api/schoolclasses/grade/4
        [Authorize(Roles = "admin, teacher")]
        [Route("grade/{grade}")]
        [HttpGet]
        public IHttpActionResult GetSchoolClassesByGrade(int grade)
        {
            string role = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == ClaimTypes.Role).Value;

            try
            {
                switch (role)
                {
                    case "admin":
                        string adminId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling admin access level SchoolClassesService method GetBySchoolGrade. Admin ID: {0}", adminId);
                        var retVal1 = service.GetBySchoolGrade(grade);
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal1);

                    case "teacher":
                        string teacherId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling teacher access level SchoolClassesService method GetBySchoolGradeAndTeacherId. Teacher ID: {0}", teacherId);
                        var retVal2 = service.GetBySchoolGradeAndTeacherId(grade, teacherId);
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

        //GET: api/schoolclasses/3
        [Authorize(Roles = "admin, teacher")]
        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult GetSchoolClassById(int id)
        {
            string role = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == ClaimTypes.Role).Value;

            try
            {
                switch (role)
                {
                    case "admin":
                        string adminId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling admin access level SchoolClassesService method GetById. Admin ID: {0}", adminId);
                        var retVal1 = service.GetById(id);
                        if (retVal1 == null)
                        {
                            logger.Info("School class with id {0} not found.", id);
                            return NotFound();
                        }
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal1);

                    case "teacher":
                        string teacherId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling teacher access level SchoolClassesService method GetByIdAndTeacherId. Teacher ID: {0}", teacherId);
                        var retVal2 = service.GetByIdAndTeacherId(id, teacherId);
                        if (retVal2 == null)
                        {
                            logger.Info("School class with id {0} not found.", id);
                            return NotFound();
                        }
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

        //POST: api/schoolclasses
        [ResponseType(typeof(void))]
        [Authorize(Roles = "admin")]
        [Route("")]
        [HttpPost]
        public IHttpActionResult PostSchoolClass([FromBody]SchoolClassCreateAndEditDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    logger.Warn("Bad model state. Returning bad request to browser.");
                    return BadRequest(ModelState);
                }

                SchoolClassDTO retVal = service.CreateSchoolClass(dto);
                logger.Info("New school class created.");

                return Created("", retVal);
            }
            catch (Exception e)
            {
                logger.Warn("Caught exception with message {0}. Returning bad request to browser.", e.Message);
                return BadRequest(e.Message);
            }            
        }

        //PUT: api/schoolclasses/6
        [ResponseType(typeof(void))]
        [Authorize(Roles = "admin")]
        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult PutSchoolClass(int id, [FromBody]SchoolClassCreateAndEditDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    logger.Warn("Bad model state. Returning bad request to browser.");
                    return BadRequest(ModelState);
                }

                service.EditSchoolClass(id, dto);
                logger.Info("School class with id {0} edited.");

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception e)
            {
                logger.Warn("Caught exception with message {0}. Returning bad request to browser.", e.Message);
                return BadRequest(e.Message);
            }            
        }

        //DELETE: api/schoolclasses/7
        [ResponseType(typeof(SchoolClassDTO))]
        [Authorize(Roles = "admin")]
        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteSchoolClass(int id)
        {
            try
            {
                SchoolClassDTO retVal = service.DeleteSchoolClass(id);

                logger.Info("SchoolClass with id {0} successfully deleted.");
                return Ok(retVal);
            }
            catch (Exception e)
            {
                logger.Warn("Caught exception with message {0}. Returning bad request to browser.", e.Message);
                return BadRequest(e.Message);
            }
            
        }
    }
}
