using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElementarySchoolProject.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public int Value { get; set; }        

        public DateTime DateOfGrading { get; set; }
        public virtual TeacherSchoolSubject TeacherSchoolSubject { get; set; }
        public virtual Student Student { get; set; }
    }
}