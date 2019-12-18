using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;
using ElementarySchoolProject.Infrastructure;
using ElementarySchoolProject.Models;
using ElementarySchoolProject.Models.DTOs;
using ElementarySchoolProject.Services;
using NLog;

namespace ElementarySchoolProject.Controllers
{
    [RoutePrefix("api/schoolsubjects")]
    public class SchoolSubjectsController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private ISchoolSubjectsService service;

        public SchoolSubjectsController(ISchoolSubjectsService service)
        {
            this.service = service;           
        }



        // GET: api/SchoolSubjects
        [Route("")]
        [Authorize(Roles = "teacher, admin")]
        [HttpGet]
        public IHttpActionResult GetSchoolSubjects()
        {
            string role = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == ClaimTypes.Role).Value;

            try
            {
                switch (role)
                {
                    case "admin":
                        string adminId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling admin access level SchoolSubjectsService method GetAll. Admin ID: {0}", adminId);
                        var retVal1 = service.GetAll();
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal1);

                    case "teacher":
                        string teacherId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling teacher access level SchoolSubjectsService method GetAllByTeacherId. Teacher ID: {0}", teacherId);
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
     

        //GET: api/SchoolSubjects/5
        [ResponseType(typeof(SchoolSubjectWithWeeklyClassesAndTeachersDTO))]
        [Route("{id}")]
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IHttpActionResult GetSchoolSubjectById(int id)
        {
            try
            {
                SchoolSubjectWithWeeklyClassesAndTeachersDTO schoolSubject = service.GetById(id);
                if (schoolSubject == null)
                {
                    logger.Warn("No school subject with id {0}", id);
                    return NotFound();
                }

                logger.Info("Returning ok to browser.");
                return Ok(schoolSubject);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);               
            }            
        }

        // PUT: api/SchoolSubjects/5
        [ResponseType(typeof(void))]
        [Route("{id}")]
        [Authorize(Roles = "admin")]
        [HttpPut]
        public IHttpActionResult PutSchoolSubject(int id, [FromBody]SchoolSubjectCreateAndEditDTO dto)
        {
            if (!ModelState.IsValid)
            {
                logger.Warn("Edit school subject failed, model state bad. Returning bad request.");
                return BadRequest(ModelState);
            }

            service.EditSchoolSubject(id, dto);

            logger.Info("School subject with id {0} successfully edited.", id);
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/SchoolSubjects
        [ResponseType(typeof(void))]
        [Route("")]
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IHttpActionResult PostSchoolSubject([FromBody]SchoolSubjectCreateAndEditDTO dto)
        {
            if (!ModelState.IsValid)
            {
                logger.Warn("Create school subject failed, model state bad. Returning bad request.");
                return BadRequest(ModelState);
            }

            SchoolSubjectWithWeeklyClassesDTO retVal = service.CreateSchoolSubject(dto);

            logger.Info("School subject successfully created.");
            return Created("", retVal);
        }

        // DELETE: api/SchoolSubjects/5
        [ResponseType(typeof(SchoolSubjectWithWeeklyClassesDTO))]
        [Route("{id}")]
        [Authorize(Roles = "admin")]
        [HttpDelete]
        public IHttpActionResult DeleteSchoolSubject(int id)
        {
            SchoolSubjectWithWeeklyClassesDTO retVal = service.DeleteSchoolSubject(id);

            logger.Info("School subject with id {0} successfulyl deleted.", id);
            return Ok(retVal);
        }
    }
}