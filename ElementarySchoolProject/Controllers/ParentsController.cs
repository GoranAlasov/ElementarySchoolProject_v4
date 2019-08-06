using ElementarySchoolProject.Models;
using ElementarySchoolProject.Models.DTOs.UserDTOs;
using ElementarySchoolProject.Services.UsersServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace ElementarySchoolProject.Controllers
{    
    [RoutePrefix("api/parents")]
    public class ParentsController : ApiController
    {
        IParentsSerivce service;

        public ParentsController(IParentsSerivce service)
        {
            this.service = service;
        }


        [Authorize(Roles = "parent")]
        [Route("me")]
        [HttpGet]
        public IHttpActionResult GetMyself()
        {
            string userId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;

            try
            {
                ParentSimpleViewDTO retVal = service.GetById(userId);
                return Ok(retVal);
            }
            catch (Exception)
            {
                return NotFound();
            }            
        }

        [Route("")]
        public IHttpActionResult GetAll()
        {
            try
            {
                var parents = service.GetAll();

                return Ok(parents);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("schoolclass/{schoolClassId}")]
        public IHttpActionResult GetAllByChildrenClass(int schoolClassId)
        {
            try
            {
                var parents = service.GetAllByChildrenClass(schoolClassId);

                return Ok(parents);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("numberOfChildren/{numberOfChildern}")]
        public IHttpActionResult GetAllByNumberOfChildren(int numberOfChildern)
        {
            try
            {
                var parents = service.GetAllByNumberOfChildren(numberOfChildern);

                return Ok(parents);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
