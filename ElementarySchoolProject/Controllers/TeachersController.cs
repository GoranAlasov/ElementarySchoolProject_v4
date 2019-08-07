using ElementarySchoolProject.Services.UsersServices;
using ElementarySchoolProject.Models.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NLog;

namespace ElementarySchoolProject.Controllers
{
    [RoutePrefix("api/teachers")]
    public class TeachersController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        ITeachersService service;
        public TeachersController(ITeachersService service)
        {
            this.service = service;
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        public IEnumerable<TeacherBasicDTO> GetAllTeachers()
        {
            var retVal = service.GetAll();

            return retVal;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        public IEnumerable<TeacherBasicDTO> GetAllTeachingASubject([FromUri]int subjectId)
        {
            var retVal = service.GetAllTeachingASubject(subjectId);

            return retVal;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        public IEnumerable<TeacherBasicDTO> GetAllTeachingToClass([FromUri]int classId)
        {
            var retVal = service.GetAllTeachingToClass(classId);

            return retVal;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        public IEnumerable<TeacherBasicDTO> GetAllTeachingToStudent([FromUri]string studentId)
        {
            var retVal = service.GetAllTeachingToStudent(studentId);

            return retVal;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetTeacherById(string id)
        {
            var retVal = service.GetById(id);
            if (retVal == null)
            {
                return NotFound();
            }

            return Ok(retVal);
        }
    }
}
