using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ElementarySchoolProject.Infrastructure;
using ElementarySchoolProject.Models;
using ElementarySchoolProject.Models.DTOs;
using ElementarySchoolProject.Services;
using NLog;

namespace ElementarySchoolProject.Controllers
{
    public class SchoolSubjectsController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private ISchoolSubjectsService service;

        public SchoolSubjectsController(ISchoolSubjectsService service)
        {
            this.service = service;           
        }



        // GET: api/SchoolSubjects
        public IEnumerable<SchoolSubjectWithWeeklyClassesAndTeachersDTO> GetSchoolSubjects()
        {
            return service.GetAll();
        }

        //GET: api/SchoolSubjects/5
        [ResponseType(typeof(SchoolSubjectWithWeeklyClassesAndTeachersDTO))]
        public IHttpActionResult GetSchoolSubjectById(int id)
        {
            SchoolSubjectWithWeeklyClassesAndTeachersDTO schoolSubject = service.GetById(id);
            if (schoolSubject == null)
            {
                return NotFound();
            }

            return Ok(schoolSubject);
        }

        // PUT: api/SchoolSubjects/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSchoolSubject(int id, [FromBody]SchoolSubjectCreateAndEditDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            service.EditSchoolSubject(id, dto);                   

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/SchoolSubjects
        [ResponseType(typeof(void))]
        public IHttpActionResult PostSchoolSubject([FromBody]SchoolSubjectCreateAndEditDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SchoolSubjectWithWeeklyClassesDTO retVal = service.CreateSchoolSubject(dto);            

            return CreatedAtRoute("DefaultApi", new { id = retVal.Id }, retVal);
        }

        // DELETE: api/SchoolSubjects/5
        [ResponseType(typeof(SchoolSubjectWithWeeklyClassesDTO))]
        public IHttpActionResult DeleteSchoolSubject(int id)
        {
            SchoolSubjectWithWeeklyClassesDTO retVal = service.DeleteSchoolSubject(id);
            
            return Ok(retVal);
        }
    }
}