using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElementarySchoolProject.Models
{
    public class SchoolClass
    {
        public int Id { get; set; }

        //1-8
        public int SchoolGrade { get; set; }
        //1-a ili 1-1
        public string Name { get; set; }

        public virtual IEnumerable<Student> Students { get; set; }
        public virtual IEnumerable<SchoolClassTeacherSchoolSubject> SchoolClassTeacherSchoolSubjects { get; set; }
    }
}