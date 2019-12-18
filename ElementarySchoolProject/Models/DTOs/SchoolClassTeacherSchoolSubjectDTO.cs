using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElementarySchoolProject.Models.DTOs
{
    public class SchoolClassTeacherSchoolSubjectDTO
    {
        public int Id { get; set; }

        public int SchoolClassId { get; set; }
        public string SchoolClassName { get; set; }

        public int TeacherSchoolSubjectId { get; set; }
        public string TeacherName { get; set; }
        public string SchoolSubjectName { get; set; }
    }
}