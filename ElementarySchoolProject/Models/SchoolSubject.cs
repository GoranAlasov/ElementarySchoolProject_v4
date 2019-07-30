using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElementarySchoolProject.Models
{
    public class SchoolSubject
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int WeeklyClasses { get; set; }        

        public virtual IEnumerable<TeacherSchoolSubject> TeacherSchoolSubjects { get; set; }        
    }
}