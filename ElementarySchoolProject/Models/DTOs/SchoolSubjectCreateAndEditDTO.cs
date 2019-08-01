using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElementarySchoolProject.Models.DTOs
{
    public class SchoolSubjectCreateAndEditDTO
    {
        public string Name { get; set; }
        public int WeeklyClasses { get; set; }
    }
}