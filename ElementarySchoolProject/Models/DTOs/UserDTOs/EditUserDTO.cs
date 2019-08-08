using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElementarySchoolProject.Models.DTOs.UserDTOs
{
    public class EditUserDTO
    {
        [StringLength(35, MinimumLength = 7, ErrorMessage = "Email must be between 7 and 35 characters long.")]
        public string Email { get; set; }

        [StringLength(20, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 20 characters long.")]
        public string UserName { get; set; }

        [StringLength(20, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 20 characters long.")]
        public string FirstName { get; set; }

        [StringLength(20, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 20 characters long.")]
        public string LastName { get; set; }
    }
}