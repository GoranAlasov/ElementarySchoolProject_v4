using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElementarySchoolProject.Models.DTOs
{
    public class SchoolClassDTO
    {
        public int Id { get; set; }
        public int Grade { get; set; }
        public string ClassName { get; set; }
    }
}