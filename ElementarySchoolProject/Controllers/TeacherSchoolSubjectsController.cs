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
    public class TeacherSchoolSubjectsController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private ITeacherSchoolSubjectsService service;

        public TeacherSchoolSubjectsController(ITeacherSchoolSubjectsService service)
        {
            this.service = service;
        }


        //GET: api/teacherschoolsubjects
        public IEnumerable<TeacherSchoolSubjectDTO> GetTeacherSchoolSubjects()
        {
            return service.GetAll();
        }

        //GET: api/teacherschoolsubjects/5
        [ResponseType(typeof(TeacherSchoolSubjectDTO))]
        public IHttpActionResult GetTeacherSchoolSubjectById(int id)
        {
            TeacherSchoolSubjectDTO retVal = service.GetById(id);
            if (retVal == null)
            {
                return NotFound();
            }

            return Ok(retVal);
        }

        //PUT: api/teacherschoolsubjects/6
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTeacherSchoolSubject(int id, [FromBody] TeacherSchoolSubjectCreateAndEditDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            service.EditTeacherSchoolSubject(id, dto);

            return StatusCode(HttpStatusCode.NoContent);
        }

        //POST: api/teacherschoolsubjects
        [ResponseType(typeof(void))]
        public IHttpActionResult PostTeacherSchoolSubject([FromBody] TeacherSchoolSubjectCreateAndEditDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TeacherSchoolSubjectDTO retVal = service.CreateTeacherSchoolSubject(dto);

            return CreatedAtRoute("DefaultApi", new { id = retVal.Id }, retVal);
        }        

        //DELETE: api/teacherschoolsubject/8
        [ResponseType(typeof(TeacherSchoolSubjectDTO))]
        public IHttpActionResult DeleteTeacherSchoolSubject(int id)
        {
            TeacherSchoolSubjectDTO retVal = service.DeleteTeacherSchoolSubject(id);

            return Ok(retVal);
        }
    }
}
