using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElementarySchoolProject.Models.DTOs
{
    public class TeacherSchoolSubjectCreateAndEditDTO
    {
        [RegularExpression("[0-9]+", ErrorMessage = "Must be a number.")]
        public string TeacherId { get; set; }

        [RegularExpression("[0-9]+", ErrorMessage = "Must be a number.")]
        public int SchoolSubjectId { get; set; }
    }
}