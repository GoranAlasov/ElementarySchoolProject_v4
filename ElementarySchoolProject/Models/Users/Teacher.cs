using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElementarySchoolProject.Models
{
    public class Teacher : ApplicationUser
    {
        public virtual List<TeacherSchoolSubject> TeacherSchoolSubjects { get; set; }        
    }
}