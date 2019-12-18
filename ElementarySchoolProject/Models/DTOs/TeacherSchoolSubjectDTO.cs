using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElementarySchoolProject.Models.DTOs
{
    public class TeacherSchoolSubjectDTO
    {
        public int Id { get; set; }
        public string TeacherId { get; set; }
        public string TeacherFirstName { get; set; }
        public string TeacherLastName { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
    }
}