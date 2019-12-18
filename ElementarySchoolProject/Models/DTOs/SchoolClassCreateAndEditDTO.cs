using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElementarySchoolProject.Models.DTOs
{
    public class SchoolClassCreateAndEditDTO
    {
        [Range(1, 8, ErrorMessage = "Must be between 1 and 8.")]
        [Required]
        public int SchoolGrade { get; set; }

        [RegularExpression("[A-Z]", ErrorMessage = "Single capital letter only!")]
        [Required]
        public string ClassName { get; set; }
    }
}