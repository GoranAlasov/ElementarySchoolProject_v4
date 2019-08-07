using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ElementarySchoolProject.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public int Value { get; set; }        
        public DateTime DateOfGrading { get; set; }

        [NotMapped]
        public int? SchoolClassTeacherSchoolSubjectId { get; set; }
        public virtual SchoolClassTeacherSchoolSubject SchoolClassTeacherSchoolSubject { get; set; }

        [NotMapped]
        public int? StudentId { get; set; }
        public virtual Student Student { get; set; }
    }
}