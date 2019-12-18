using ElementarySchoolProject.Models.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElementarySchoolProject.Models.DTOs
{
    public class SchoolClassDetailsDTO
    {
        public int Id { get; set; }
        public int SchoolGrade { get; set; }
        public string ClassName { get; set; }

        public IEnumerable<StudentBasicDTO> Students {get;set;}
    }
}