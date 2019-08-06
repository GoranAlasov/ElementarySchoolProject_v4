using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElementarySchoolProject.Models.DTOs
{
    public class TeacherSchoolSubjectCreateAndEditDTO
    {        
        public string TeacherId { get; set; }
        public int SchoolSubjectId { get; set; }
    }
}