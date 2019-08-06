using ElementarySchoolProject.Models;
using ElementarySchoolProject.Models.DTOs;
using ElementarySchoolProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ElementarySchoolProject.Controllers
{
    public class TeacherSchoolSubjectsController : ApiController
    {
        private ITeacherSchoolSubjectsService service;

        public TeacherSchoolSubjectsController(ITeacherSchoolSubjectsService service)
        {
            this.service = service;
        }



        public IEnumerable<TeacherSchoolSubjectDTO> GetAll()
        {
            return service.GetAll();
        }

        public TeacherSchoolSubjectDTO GetById(int id)
        {
            return service.GetById(id);
        }

        public TeacherSchoolSubjectDTO PostTeacherSchoolSubject([FromBody] TeacherSchoolSubjectCreateAndEditDTO dto)
        {
            return service.CreateTeacherSchoolSubject(dto);
        }
    }
}
