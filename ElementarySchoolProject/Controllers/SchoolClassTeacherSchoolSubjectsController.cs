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
    public class SchoolClassTeacherSchoolSubjectsController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        ISchoolClassTeacherSchoolSubjectService service;
        public SchoolClassTeacherSchoolSubjectsController(ISchoolClassTeacherSchoolSubjectService service)
        {
            this.service = service;
        }



        //GET: api/schoolclassteacherschoolsubjects
        [HttpGet]
        public IEnumerable<SchoolClassTeacherSchoolSubjectDTO> GetSchoolClassTeacherSchoolSubjects()
        {
            return service.GetAll();
        }

        //GET: api/schoolclassteacherschoolsubjects/6
        [HttpGet]
        [ResponseType(typeof(SchoolClassTeacherSchoolSubjectDTO))]
        public IHttpActionResult GetSchoolClassTeacherSchoolSubject(int id)
        {
            SchoolClassTeacherSchoolSubjectDTO retVal = service.GetById(id);
            if (retVal == null)
            {
                return NotFound();
            }

            return Ok(retVal);
        }

        //PUT: api/schoolclassteacherschoolsubjects/4
        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult PutSchoolClassTeacherSchoolSubject(int id, [FromBody] SchoolClassTeacherSchoolSubjectCreateAndEditDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            service.EditSchoolClassTeacherSchoolSubject(id, dto);

            return StatusCode(HttpStatusCode.NoContent);
        }

        //POST: api/schoolclassteacherschoolsubject
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult PostSchoolClassTeacherSchoolSubject([FromBody] SchoolClassTeacherSchoolSubjectCreateAndEditDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SchoolClassTeacherSchoolSubjectDTO retVal = service.CreateSchoolClassTeacherSchoolSubject(dto);

            return CreatedAtRoute("DefaultApi", new { id = retVal.Id }, retVal);
        }

        //DELETE: api/schoolclassteacherschoolsubject/6
        [ResponseType(typeof(SchoolClassTeacherSchoolSubjectDTO))]
        [HttpDelete]
        public IHttpActionResult DeleteSchoolClassTeacherSchoolSubject(int id)
        {
            SchoolClassTeacherSchoolSubjectDTO retVal = service.DeleteSchoolClassTeacherSchoolSubject(id);

            return Ok(retVal);
        }
    }
}
