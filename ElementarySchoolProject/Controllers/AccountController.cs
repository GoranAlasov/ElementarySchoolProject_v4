using ElementarySchoolProject.Models;
using ElementarySchoolProject.Models.DTOs.UserDTOs;
using ElementarySchoolProject.Services.UsersServices;
using Microsoft.AspNet.Identity;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ElementarySchoolProject.Controllers
{
    [RoutePrefix("api/accounts")]
    [Authorize(Roles = "admin")]
    public class AccountController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IUsersService service;

        public AccountController(IUsersService userService)
        {
            this.service = userService;
        }

        #region RegisteringUsers

        [Authorize(Roles = "admin")]
        //[AllowAnonymous]
        [Route("admins")]
        [HttpPost]
        public async Task<IHttpActionResult> RegisterAdmin(RegisterUserDTO userModel)
        {
            if (!ModelState.IsValid)
            {
                logger.Warn("RegisterAdmin returned BadRequest with invalid ModelState");
                return BadRequest(ModelState);
            }

            var result = await service.RegisterAdmin(userModel);

            if (result == null)
            {
                logger.Warn("RegisterAdmin returned BadRequest with null result");
                return BadRequest(ModelState);
            }

            logger.Info("RegisterAdmin finished OK. New Admin was created");
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        //[AllowAnonymous]
        [Route("teachers")]
        [HttpPost]
        public async Task<IHttpActionResult> RegisterTeacher(RegisterUserDTO userModel)
        {
            if (!ModelState.IsValid)
            {
                logger.Warn("RegisterTeacher returned BadRequest with invalid ModelState");
                return BadRequest(ModelState);
            }

            var result = await service.RegisterTeacher(userModel);

            if (result == null)
            {
                logger.Warn("RegisterTeacher returned BadRequest with null result");
                return BadRequest(ModelState);
            }

            logger.Info("RegisterTeacher finished OK. New Teacher was created");
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        //[AllowAnonymous]
        [Route("parents")]
        [HttpPost]
        public async Task<IHttpActionResult> RegisterParent(RegisterUserDTO userModel)
        {
            if (!ModelState.IsValid)
            {
                logger.Warn("RegisterParent returned BadRequest with invalid ModelState");
                return BadRequest(ModelState);
            }

            var result = await service.RegisterParent(userModel);

            if (result == null)
            {
                logger.Warn("RegisterParent returned BadRequest with null result");
                return BadRequest(ModelState);
            }

            logger.Info("RegisterParent finished OK. New Parent was created");
            return Ok(result);
        }


        [Authorize(Roles = "admin")]
        //[AllowAnonymous]
        [Route("students")]
        [HttpPost]
        public async Task<IHttpActionResult> RegisterStudent(RegisterStudentDTO userModel)
        {
            if (!ModelState.IsValid)
            {
                logger.Warn("RegisterStudent returned BadRequest with invalid ModelState");
                return BadRequest(ModelState);
            }

            var result = await service.RegisterStudent(userModel);

            if (result == null)
            {
                logger.Warn("RegisterStudent returned BadRequest with null result");
                return BadRequest(ModelState);
            }

            logger.Warn("RegisterStudent finished OK. New Studen was created");
            return Ok(result);
        }

        #endregion

        #region EditingUsers

        [Authorize(Roles = "admin")]
        [Route("admins/{id}")]
        [HttpPut]
        public async Task<IHttpActionResult> PutAdmin(string id, [FromBody]EditUserDTO user)
        {
            if (!ModelState.IsValid)
            {
                logger.Warn("PutAdmin returned BadRequest with invalid ModelState");
                return BadRequest(ModelState);
            }

            var result = await service.EditAdmin(id, user);

            logger.Info("PutAdmin finished Ok. Admin {0} was edited.", id);
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [Route("teachers/{id}")]
        [HttpPut]
        public async Task<IHttpActionResult> PutTeacher(string id, [FromBody]EditUserDTO user)
        {
            if (!ModelState.IsValid)
            {
                logger.Warn("PutTeacher returned BadRequest with invalid ModelState");
                return BadRequest(ModelState);
            }

            var result = await service.EditTeacher(id, user);

            logger.Info("PutTeacher finished Ok. Teacher {0} was edited.", id);
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [Route("parents/{id}")]
        [HttpPut]
        public async Task<IHttpActionResult> PutParent(string id, [FromBody]EditUserDTO user)
        {
            if (!ModelState.IsValid)
            {
                logger.Warn("PutParent returned BadRequest with invalid ModelState");
                return BadRequest(ModelState);
            }

            var result = await service.EditParent(id, user);

            logger.Info("PutParent finished Ok. Parent {0} was edited.", id);
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [Route("students/{id}")]
        [HttpPut]
        public async Task<IHttpActionResult> PutStudent(string id, [FromBody]EditUserDTO user)
        {
            if (!ModelState.IsValid)
            {
                logger.Warn("PutStudent returned BadRequest with invalid ModelState");
                return BadRequest(ModelState);
            }

            var result = await service.EditStudent(id, user);

            logger.Info("PutStudent finished Ok. Student {0} was edited.", id);
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [Route("students/{studentId}/setparent/{parentId}")]
        [HttpPut]
        public IHttpActionResult ChangeParent(string studentId, string parentId)
        {
            try
            {
                var retVal = service.ChangeParent(studentId, parentId);
                logger.Info("Changed parent of student {0} to {1}", studentId, parentId);
                return Ok(retVal);
            }
            catch (Exception e)
            {
                logger.Warn("Exception caught with message {0}. Returning bad request.", e.Message);
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "admin")]
        [Route("students/{studentId}/setclass/{classId}")]
        [HttpPut]
        public IHttpActionResult AssignStudentToClass(string studentId, int classId)
        {
            try
            {
                var retVal = service.AddStudentToClass(studentId, classId);
                logger.Info("Added student {0} to class {1}", studentId, classId);
                return Ok(retVal);
            }
            catch (Exception e)
            {
                logger.Warn("Exception caught with message {0}. Returning bad request.", e.Message);
                return BadRequest(e.Message);
            }
        }

        #endregion

        #region GettingUsers

        [Authorize(Roles = "admin")]
        //[AllowAnonymous]
        [Route("me")]
        [HttpGet]
        public IHttpActionResult GetMySelfAdmin()
        {
            string userId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;

            UserSimpleViewDTO retVal = service.GetAdminById(userId);

            if (retVal == null)
            {
                logger.Info("Not found admin with id {0}", userId);
                return NotFound();
            }

            logger.Info("Returning admin with id {0}", userId);
            return Ok(retVal);
        }

        [Route("")]
        [Authorize(Roles = "admin")]
        //[AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<UserViewWithRoleIdsDTO>> GetAllUsers()
        {
            var retVal = await service.GetAllUsers();

            logger.Info("Getting all users");
            return retVal;
        }

        [Route("admins")]
        //[AllowAnonymous]
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IEnumerable<UserSimpleViewDTO> GetAllAdmins()
        {
            var retVal = service.GetAllAdmins();

            logger.Info("Getting all admins.");
            return retVal;
        }

        [Route("teachers")]
        [Authorize(Roles = "admin")]
        //[AllowAnonymous]
        [HttpGet]
        public IEnumerable<UserSimpleViewDTO> GetAllTeachers()
        {
            var retVal = service.GetAllTeachers();

            logger.Info("Getiing all teahcers");
            return retVal;
        }

        [Route("parents")]
        [Authorize(Roles = "admin")]
        //[AllowAnonymous]
        [HttpGet]
        public IEnumerable<ParentSimpleViewDTO> GetAllParents()
        {
            var retVal = service.GetAllParents();

            logger.Info("Getting all parents.");
            return retVal;
        }

        [Route("students")]
        [Authorize(Roles = "admin")]
        //[AllowAnonymous]
        [HttpGet]
        public IEnumerable<StudentWithParentDTO> GetAllStudents()
        {
            var retVal = service.GetAllStudents();

            logger.Info("Getting all students.");
            return retVal;
        }

        [Route("{id}")]
        [Authorize(Roles = "admin")]
        //[AllowAnonymous]
        [HttpGet]
        public async Task<UserViewWithRoleIdsDTO> GetUserById(string id)
        {
            UserViewWithRoleIdsDTO retVal = await service.GetUserById(id);

            logger.Info("Getting user with id {0}", id);
            return retVal;
        }

        [Route("admins/{id}")]
        [Authorize(Roles = "admin")]
        //[AllowAnonymous]
        [HttpGet]
        public IHttpActionResult GetAdminById(string id)
        {
            UserSimpleViewDTO retVal = service.GetAdminById(id);

            if (retVal == null)
            {
                logger.Warn("Admin with id {0} not found", id);
                return NotFound();
            }

            logger.Info("Getting admin with id {0}", id);
            return Ok(retVal);
        }

        [Route("teachers/{id}")]
        [Authorize(Roles = "admin")]
        //[AllowAnonymous]
        [HttpGet]
        public IHttpActionResult GetTeacherById(string id)
        {
            UserSimpleViewDTO retVal = service.GetTeacherById(id);

            if (retVal == null)
            {
                logger.Warn("Teacher with id {0} not found", id);
                return NotFound();
            }

            logger.Info($"Getting teacher with id {id}");
            return Ok(retVal);
        }

        [Route("parents/{id}")]
        [Authorize(Roles = "admin")]
        //[AllowAnonymous]
        [HttpGet]
        public IHttpActionResult GetParentById(string id)
        {
            ParentSimpleViewDTO retVal = service.GetParentById(id);

            if (retVal == null)
            {
                logger.Warn($"Parent with id {id} not found.");
                return NotFound();
            }

            logger.Info($"Getting parent with id {id}");
            return Ok(retVal);
        }

        [Route("students/{id}")]
        [Authorize(Roles = "admin")]
        //[AllowAnonymous]
        [HttpGet]
        public IHttpActionResult GetStudentById(string id)
        {
            StudentWithParentDTO retVal = service.GetStudentById(id);

            if (retVal == null)
            {
                logger.Warn($"Student with id {id} not found.");
                return NotFound();
            }

            logger.Info($"Getting student with id {id}");
            return Ok(retVal);
        }

        #endregion

        #region DeletingUsers

        [Route("admins/{id}")]
        //[AllowAnonymous]
        [Authorize(Roles = "admin")]
        [HttpDelete]
        public IHttpActionResult DeleteAdmin(string id)
        {
            UserSimpleViewDTO retVal = service.DeleteAdmin(id);

            if (retVal == null)
            {
                logger.Warn($"Admin with id {id} not found.");
                return BadRequest();
            }

            logger.Info($"Deleting admin with id {id}");
            return Ok(retVal);
        }

        [Route("teachers/{id}")]
        //[AllowAnonymous]
        [Authorize(Roles = "admin")]
        [HttpDelete]
        public IHttpActionResult DeleteTeacher(string id)
        {
            UserSimpleViewDTO retVal = service.DeleteTeacher(id);

            if (retVal == null)
            {
                logger.Warn($"Teacher with id {id} not found.");
                return BadRequest();
            }

            logger.Info($"Deleting teacher with id {id}");
            return Ok(retVal);
        }

        [Route("parents/{id}")]
        //[AllowAnonymous]
        [Authorize(Roles = "admin")]
        [HttpDelete]
        public IHttpActionResult DeleteParent(string id)
        {
            UserSimpleViewDTO retVal = service.DeleteParent(id);

            if (retVal == null)
            {
                logger.Warn($"Parent with id {id} nto found.");
                return BadRequest();
            }

            logger.Info($"Deleting parent with id {id}");
            return Ok(retVal);
        }

        [Route("students/{id}")]
        //[AllowAnonymous]
        [Authorize(Roles = "admin")]
        [HttpDelete]
        public IHttpActionResult DeleteStudent(string id)
        {
            UserSimpleViewDTO retVal = service.DeleteStudent(id);

            if (retVal == null)
            {
                logger.Warn($"Student with id {id} not found.");
                return BadRequest();
            }

            logger.Info($"Deleting student with id {id}");
            return Ok(retVal);
        }

        #endregion                
    }
}
