using ElementarySchoolProject.Models;
using ElementarySchoolProject.Models.DTOs.UserDTOs;
using ElementarySchoolProject.Services.UsersServices;
using NLog;
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
        private static Logger logger = LogManager.GetCurrentClassLogger();

        IParentsSerivce service;

        public ParentsController(IParentsSerivce service)
        {
            this.service = service;
        }        

        [Authorize(Roles = "admin, parent")]
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            string role = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == ClaimTypes.Role).Value;

            try
            {
                switch (role)
                {
                    case "admin":
                        string adminId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling admin access level ParentsService method GetAll. Admin ID: {0}", adminId);
                        var retVal1 = service.GetAll();
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal1);

                    case "parent":
                        string parentId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;
                        logger.Info("Calling parent access level ParentsService method GetById. Admin ID: {0}", parentId);
                        var retVal2 = service.GetById(parentId);
                        logger.Info("Returning ok to browser.");
                        return Ok(retVal2);

                    default:
                        logger.Warn("BadRequest. There is no method for this role! {0}", role);
                        return BadRequest();
                }
            }
            catch (Exception e)
            {
                logger.Warn("Caught exception with message {0}. Returning bad request.", e.Message);
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "admin")]
        [Route("schoolclass/{schoolClassId}")]
        [HttpGet]
        public IHttpActionResult GetAllByChildrenClass(int schoolClassId)
        {
            try
            {
                var parents = service.GetAllByChildrenClass(schoolClassId);

                logger.Info("Returtning all parents of children in school class with id {0}.", schoolClassId);
                return Ok(parents);
            }
            catch (Exception e)
            {
                logger.Warn("Caught exception with message {0}. Returning bad request.", e.Message);
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "admin")]
        [Route("numberOfChildren/{numberOfChildern}")]
        [HttpGet]
        public IHttpActionResult GetAllByNumberOfChildren(int numberOfChildern)
        {
            try
            {
                var parents = service.GetAllByNumberOfChildren(numberOfChildern);

                logger.Info("Returtning all parents with {0} children", numberOfChildern);
                return Ok(parents);
            }
            catch (Exception e)
            {
                logger.Warn("Caught exception with message {0}. Returning bad request.", e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
