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

namespace ElementarySchoolProject.Controllers
{
    public class SchoolClassesController : ApiController
    {
        ISchoolClassesService service;
        public SchoolClassesController(ISchoolClassesService service)
        {
            this.service = service;
        }

        //GET: api/schoolclasses
        public IEnumerable<SchoolClassDTO> GetSchoolClasses()
        {
            return service.GetAll();                
        }

        //GET: api/schoolclasses/grade/4
        [Route("api/schoolclasses/grade/{grade}")]
        public IEnumerable<SchoolClassDTO> GetSchoolClassesByGrade(int grade)
        {
            return service.GetBySchoolGrade(grade);
        }

        //GET: api/schoolclasses/3
        [ResponseType(typeof(SchoolClassDetailsDTO))]
        public IHttpActionResult GetSchoolClassById(int id)
        {
            SchoolClassDetailsDTO retVal = service.GetById(id);
            if (retVal == null)
            {
                return NotFound();
            }

            return Ok(retVal);
        }        

        //POST: api/schoolclasses
        [ResponseType(typeof(void))]
        public IHttpActionResult PostSchoolClass([FromBody]SchoolClassCreateAndEditDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            SchoolClassDTO retVal = service.CreateSchoolClass(dto);

            return CreatedAtRoute("DefaultApi", new { id = retVal.Id }, retVal);
        }

        //PUT: api/schoolclasses/6
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSchoolClass(int id, [FromBody]SchoolClassCreateAndEditDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            service.EditSchoolClass(id, dto);

            return StatusCode(HttpStatusCode.NoContent);
        }

        //DELETE: api/schoolclasses/7
        [ResponseType(typeof(SchoolClassDTO))]
        public IHttpActionResult DeleteSchoolClass(int id)
        {
            SchoolClassDTO retVal = service.DeleteSchoolClass(id);

            return Ok(retVal);
        }
    }
}
