using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElementarySchoolProject.Models
{
    public class TeacherSchoolSubject
    {
        public int Id { get; set; }

        public virtual Teacher Teacher { get; set; }
        public virtual SchoolSubject SchoolSubject { get; set; }
        public virtual IEnumerable<SchoolClass> SchoolClasses { get; set; }
    }
}