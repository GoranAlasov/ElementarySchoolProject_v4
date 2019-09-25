using ElementarySchoolProject.Models;
using ElementarySchoolProject.Models.DTOs;
using ElementarySchoolProject.Services;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ElementarySchoolProject.Controllers
{
    [RoutePrefix("api/teacherschoolsubjects")]
    public class TeacherSchoolSubjectsController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private ITeacherSchoolSubjectsService service;

        public TeacherSchoolSubjectsController(ITeacherSchoolSubjectsService service)
        {
            this.service = service;
        }


        //GET: api/teacherschoolsubjects
        [Authorize(Roles = "admin")]
        [Route("")]
        [HttpGet]
        public IEnumerable<TeacherSchoolSubjectDTO> GetTeacherSchoolSubjects()
        {
            logger.Info("Returning all teacherschoolsubjects to front.");
            return service.GetAll();
        }

        //GET: api/teacherschoolsubjects/5
        [ResponseType(typeof(TeacherSchoolSubjectDTO))]
        [Authorize(Roles = "admin")]
        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult GetTeacherSchoolSubjectById(int id)
        {
            try
            {
                TeacherSchoolSubjectDTO retVal = service.GetById(id);
                if (retVal == null)
                {
                    logger.Warn("Not found teacherschoolsubject");
                    return NotFound();
                }

                logger.Info("Returning OK to front.");
                return Ok(retVal);
            }
            catch (Exception e)
            {
                logger.Warn("Exception {0}", e.Message);
                return NotFound();
            }            
        }

        //PUT: api/teacherschoolsubjects/6
        [ResponseType(typeof(void))]
        [Authorize(Roles = "admin")]
        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult PutTeacherSchoolSubject(int id, [FromBody] TeacherSchoolSubjectCreateAndEditDTO dto)
        {
            if (!ModelState.IsValid)
            {
                logger.Warn("Invalid model state, teacherschoolsubject edit failed. returning bad request");
                return BadRequest();
            }

            service.EditTeacherSchoolSubject(id, dto);
            logger.Info("Successfully edited teacherschoolsubject. returning ok to front");
            return StatusCode(HttpStatusCode.NoContent);
        }

        //POST: api/teacherschoolsubjects
        [ResponseType(typeof(void))]
        [Authorize(Roles = "admin")]
        [Route("")]
        [HttpPost] 
        public IHttpActionResult PostTeacherSchoolSubject([FromBody] TeacherSchoolSubjectCreateAndEditDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    logger.Warn("Invalid model state, teacherschoolsubject create failed. returning bad request");
                    return BadRequest(ModelState);
                }

                TeacherSchoolSubjectDTO retVal = service.CreateTeacherSchoolSubject(dto);
                logger.Info("Successfully created teacherschoolsubject. returning ok to front");
                return Created("", retVal);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }        

        //DELETE: api/teacherschoolsubject/8
        [ResponseType(typeof(TeacherSchoolSubjectDTO))]    
        [Authorize(Roles = "admin")]
        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteTeacherSchoolSubject(int id)
        {
            service.DeleteTeacherSchoolSubject(id);
            logger.Info("Successfully deleted teacherschoolsubject. returning ok to front");
            return Ok();
        }
    }
}
