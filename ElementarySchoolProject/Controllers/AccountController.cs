using ElementarySchoolProject.Models.Users.UserDTOs;
using ElementarySchoolProject.Services.UsersService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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

        //[Authorize(Roles = "admins, teachers, parents, students")]
        //[Route("me")]
        //public RegisterUserDTO GetMySelfAdmin()
        //{
        //    RegisterUserDTO retVal = new RegisterUserDTO();

        //    string userId = User.Identity.Name.
        //     = RequestContext.Principal.Identity;

        //    return retVal;
        //}

        #region RegisterUsers

        [Authorize(Roles = "admins")]
        [Route("register-admin")]
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

            return Ok();
        }

        [Authorize(Roles = "admins")]
        [Route("register-teacher")]
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

            return Ok();
        }

        [Authorize(Roles = "admins")]
        [Route("register-parent")]
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

            return Ok();
        }

        
        [Route("register-student")]
        public async Task<IHttpActionResult> RegisterStudent(RegisterUserDTO userModel)
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

            return Ok();
        }

        #endregion

        [Route("getall")]
        [Authorize(Roles = "admins")]
        public async Task<IList<UserBasicInfoDTO>> GetAllUsers()
        {
            var result = await service.GetAllUsers();

            IList<UserBasicInfoDTO> result2 = new List<UserBasicInfoDTO>();

            foreach (var item in result)
            {
                result2.Add(Utilities.UserToUserDTOConverters.UserToBasicInfoDTO(item));
            }

            return result2;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

    }
}
