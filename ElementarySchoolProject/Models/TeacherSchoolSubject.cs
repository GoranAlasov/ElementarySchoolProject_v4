using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ElementarySchoolProject.Models
{
    public class TeacherSchoolSubject
    {
        public int Id { get; set; }

        [NotMapped]
        public string TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }        

        [NotMapped]
        public int? SchoolSubjectId { get; set; }
        public virtual SchoolSubject SchoolSubject { get; set; }

        public virtual List<SchoolClassTeacherSchoolSubject> SchoolClassTeacherSchoolSubjects { get; set; }
    }
}