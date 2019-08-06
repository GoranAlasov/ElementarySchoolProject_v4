using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElementarySchoolProject.Models
{
    public class SchoolClass
    {
        public int Id { get; set; }        
        public int SchoolGrade { get; set; }        
        public string Name { get; set; }


        public virtual List<Student> Students { get; set; }
        public virtual List<SchoolClassTeacherSchoolSubject> SchoolClassTeacherSchoolSubjects { get; set; }
    }
}