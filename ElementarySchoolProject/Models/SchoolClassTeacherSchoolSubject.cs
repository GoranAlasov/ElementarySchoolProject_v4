using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElementarySchoolProject.Models
{
    public class SchoolClassTeacherSchoolSubject
    {
        public int Id { get; set; }

        public virtual TeacherSchoolSubject TeacherSchoolSubject { get; set; }
        public virtual SchoolClass SchoolClass { get; set; }
    }
}