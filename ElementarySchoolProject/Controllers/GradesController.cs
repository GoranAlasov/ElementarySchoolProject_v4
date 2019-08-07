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

        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<GradeDTO> GetAll()
        {
            return service.GetAll();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("{teacherId}")]
        public IHttpActionResult CreateGrade(string teacherId, [FromBody]GradeCreateAndEditDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                GradeDTO retVal = service.CreateGrade(teacherId, dto);

                return Ok(retVal);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
    }
}
