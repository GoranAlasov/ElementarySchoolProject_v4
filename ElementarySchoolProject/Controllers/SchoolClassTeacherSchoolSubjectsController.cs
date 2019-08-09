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
    [RoutePrefix("api/schoolclassteacherschoolsubjects")]
    public class SchoolClassTeacherSchoolSubjectsController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        ISchoolClassTeacherSchoolSubjectService service;
        public SchoolClassTeacherSchoolSubjectsController(ISchoolClassTeacherSchoolSubjectService service)
        {
            this.service = service;
        }



        //GET: api/schoolclassteacherschoolsubjects
        [Route("")]
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IEnumerable<SchoolClassTeacherSchoolSubjectDTO> GetSchoolClassTeacherSchoolSubjects()
        {
            logger.Info("Returning all schoolclassteacherschoolsubjects");
            return service.GetAll();
        }

        //GET: api/schoolclassteacherschoolsubjects/6
        [HttpGet]
        [ResponseType(typeof(SchoolClassTeacherSchoolSubjectDTO))]
        [Route("{id}")]
        [Authorize(Roles = "admin")]
        public IHttpActionResult GetSchoolClassTeacherSchoolSubject(int id)
        {
            SchoolClassTeacherSchoolSubjectDTO retVal = service.GetById(id);
            if (retVal == null)
            {
                logger.Warn("No schoolclassteacherschoolsubject with id {0}", id);
                return NotFound();
            }

            logger.Info("Returning schoolclassteacherschoolsubject with id {0}", id);
            return Ok(retVal);
        }

        //PUT: api/schoolclassteacherschoolsubjects/4
        [ResponseType(typeof(void))]
        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "admin")]
        public IHttpActionResult PutSchoolClassTeacherSchoolSubject(int id, [FromBody] SchoolClassTeacherSchoolSubjectCreateAndEditDTO dto)
        {
            if (!ModelState.IsValid)
            {
                logger.Warn("schoolclassteacherschoolsubject with id {0} not edited. Bad model state. Returning bad request.", id);
                return BadRequest(ModelState);
            }

            service.EditSchoolClassTeacherSchoolSubject(id, dto);
            logger.Info("Edited schoolclassteacherschoolsubject with id {0}", id);
            return StatusCode(HttpStatusCode.NoContent);
        }

        //POST: api/schoolclassteacherschoolsubject
        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("")]
        [Authorize(Roles = "admin")]
        public IHttpActionResult PostSchoolClassTeacherSchoolSubject([FromBody] SchoolClassTeacherSchoolSubjectCreateAndEditDTO dto)
        {
            if (!ModelState.IsValid)
            {
                logger.Warn("schoolclassteacherschoolsubject not created. Bad model state. Returning bad request.");
                return BadRequest(ModelState);
            }

            SchoolClassTeacherSchoolSubjectDTO retVal = service.CreateSchoolClassTeacherSchoolSubject(dto);
            logger.Info("Created new schoolclassteacherschoolsubject");
            return CreatedAtRoute("DefaultApi", new { id = retVal.Id }, retVal);
        }

        //DELETE: api/schoolclassteacherschoolsubject/6
        [ResponseType(typeof(SchoolClassTeacherSchoolSubjectDTO))]
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "admin")]
        public IHttpActionResult DeleteSchoolClassTeacherSchoolSubject(int id)
        {
            SchoolClassTeacherSchoolSubjectDTO retVal = service.DeleteSchoolClassTeacherSchoolSubject(id);
            logger.Info("Deleted schoolclassteacherschoolsubject with id {0}", id);
            return Ok(retVal);
        }
    }
}
