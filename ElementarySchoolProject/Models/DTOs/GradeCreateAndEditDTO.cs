using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElementarySchoolProject.Models.DTOs
{
    public class GradeCreateAndEditDTO
    {
        public int Id { get; set; }

        [Range(1, 2, ErrorMessage = "Must be between 1 and 5.")]        
        public int Value { get; set; }

        //public DateTime DateOfGrading { get; set; }

        public string StudentId { get; set; }

        [RegularExpression("[0-9]+", ErrorMessage = "Must be a number!")]
        public int SchoolSubjectId { get; set; }
    }
}