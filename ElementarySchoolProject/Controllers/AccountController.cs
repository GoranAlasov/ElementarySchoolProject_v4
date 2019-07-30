using ElementarySchoolProject.Models;
using ElementarySchoolProject.Models.Users.UserDTOs;
using ElementarySchoolProject.Services.UsersServices;
using Microsoft.AspNet.Identity;
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
    public class AccountController : ApiController
    {
        private IUsersService service;

        public AccountController(IUsersService userService)
        {
            this.service = userService;
        }

        #region RegisteringUsers

        [Authorize(Roles = "admin")]
        [Route("register-admin")]
        [HttpPost]
        public async Task<IHttpActionResult> RegisterAdmin(RegisterUserDTO userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await service.RegisterAdmin(userModel);

            if (result == null)
            {
                return BadRequest(ModelState);
            }

            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [Route("register-teacher")]
        [HttpPost]
        public async Task<IHttpActionResult> RegisterTeacher(RegisterUserDTO userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await service.RegisterTeacher(userModel);

            if (result == null)
            {
                return BadRequest(ModelState);
            }

            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [Route("register-parent")]
        [HttpPost]
        public async Task<IHttpActionResult> RegisterParent(RegisterUserDTO userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await service.RegisterParent(userModel);

            if (result == null)
            {
                return BadRequest(ModelState);
            }
            
            return Ok(result);
        }

        
        [Authorize(Roles = "admin")]
        [Route("register-student")]
        [HttpPost]
        public async Task<IHttpActionResult> RegisterStudent(RegisterStudentDTO userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await service.RegisterStudent(userModel);

            if (result == null)
            {
                return BadRequest(ModelState);
            }

            return Ok(result);
        }

        #endregion

        #region GettingUsers
        
        [Authorize(Roles = "admin")]
        [Route("me")]
        [HttpGet]
        public IHttpActionResult GetMySelfAdmin()
        {
            string userId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;

            UserSimpleViewDTO retVal = service.GetAdminById(userId);

            if (retVal == null)
            {
                return NotFound();
            }

            return Ok(retVal);
        }

        [Route("")]
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IEnumerable<UserViewWithRoleIds>> GetAllUsers()
        {
            var retVal = await service.GetAllUsers();            

            return retVal;
        }

        [Route("admins")]
        [AllowAnonymous]
        [HttpGet]
        //[Authorize(Roles = "admin")]
        public IEnumerable<UserSimpleViewDTO> GetAllAdmins()
        {
            var retVal = service.GetAllAdmins();

            return retVal;
        }

        [Route("teachers")]
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IEnumerable<UserSimpleViewDTO> GetAllTeachers()
        {
            var retVal = service.GetAllTeachers();

            return retVal;
        }

        [Route("parents")]
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<ParentSimpleViewDTO> GetAllParents()
        {
            var retVal = service.GetAllParents();

            return retVal;
        }

        [Route("students")]
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<StudentSimpleViewDTO> GetAllStudents()
        {
            var retVal = service.GetAllStudents();

            return retVal;
        }

        [Route("{id}")]
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<UserViewWithRoleIds> GetUserById(string id)
        {
            UserViewWithRoleIds retVal = await service.GetUserById(id);            

            return retVal;
        }

        [Route("admins/{id}")]
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult GetAdminById(string id)
        {
            UserSimpleViewDTO retVal = service.GetAdminById(id);

            if (retVal == null)
            {
                return NotFound();
            }

            return Ok(retVal);
        }

        [Route("teachers/{id}")]
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IHttpActionResult GetTeacherById(string id)
        {
            UserSimpleViewDTO retVal = service.GetTeacherById(id);

            if (retVal == null)
            {
                return NotFound();
            }

            return Ok(retVal);
        }

        [Route("parents/{id}")]
        [Authorize(Roles = "admin")]        
        [HttpGet]
        public IHttpActionResult GetParentById(string id)
        {
            ParentSimpleViewDTO retVal = service.GetParentById(id);

            if (retVal == null)
            {
                return NotFound();
            }

            return Ok(retVal);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        [Route("students/{id}")]
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IHttpActionResult GetStudentById(string id)
        {
            StudentSimpleViewDTO retVal = service.GetStudentById(id);

            if (retVal == null)
            {
                return NotFound();
            }

            return Ok(retVal);
        }

        #endregion

        #region DeletingUsers

        [Route("admins/delete/{id}")]
        [AllowAnonymous]
        [HttpDelete]
        public IHttpActionResult DeleteAdmin(string id)
        {
            UserSimpleViewDTO retVal = service.DeleteAdmin(id);

            if (retVal == null)
            {
                return BadRequest();
            }

            return Ok(retVal);
        }

        [Route("teachers/delete/{id}")]
        [AllowAnonymous]
        [HttpDelete]
        public IHttpActionResult DeleteTeacher(string id)
        {
            UserSimpleViewDTO retVal = service.DeleteTeacher(id);

            if (retVal == null)
            {
                return BadRequest();
            }

            return Ok(retVal);
        }

        [Route("parents/delete/{id}")]
        [AllowAnonymous]
        [HttpDelete]
        public IHttpActionResult DeleteParent(string id)
        {
            UserSimpleViewDTO retVal = service.DeleteParent(id);

            if (retVal == null)
            {
                return BadRequest();
            }

            return Ok(retVal);
        }

        [Route("students/delete/id")]
        [AllowAnonymous]
        [HttpDelete]
        public IHttpActionResult DeleteStudent(string id)
        {
            UserSimpleViewDTO retVal = service.DeleteStudent(id);

            if (retVal == null)
            {
                return BadRequest();
            }

            return Ok(retVal);
        }

        #endregion        
    }
}
