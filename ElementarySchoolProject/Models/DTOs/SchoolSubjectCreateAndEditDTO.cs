using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElementarySchoolProject.Models.DTOs
{
    public class SchoolSubjectCreateAndEditDTO
    {
        [StringLength(25, MinimumLength = 5, ErrorMessage = "Must be between 5 and 25 characters.")]
        public string Name { get; set; }

        [Range(1, 10, ErrorMessage = "Must be between 1 and 10.")]
        public int WeeklyClasses { get; set; }
    }
}