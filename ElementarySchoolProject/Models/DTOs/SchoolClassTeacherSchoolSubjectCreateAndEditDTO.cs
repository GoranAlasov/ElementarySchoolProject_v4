using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElementarySchoolProject.Models.DTOs
{
    public class SchoolClassTeacherSchoolSubjectCreateAndEditDTO
    {
        [RegularExpression("[0-9]+", ErrorMessage = "Must be a number!")]
        [Required]
        public int SchoolClassId { get; set; }

        [RegularExpression("[0-9]+", ErrorMessage = "Must be a number!")]
        [Required]
        public int TeacherSchoolSubjectId { get; set; }        
    }
}