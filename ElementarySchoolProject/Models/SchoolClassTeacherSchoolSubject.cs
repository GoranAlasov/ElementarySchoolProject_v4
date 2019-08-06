using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ElementarySchoolProject.Models
{
    public class SchoolClassTeacherSchoolSubject
    {
        public int Id { get; set; }

        [NotMapped]
        public int? TeacherSchoolSubjectId { get; set; }
        public virtual TeacherSchoolSubject TeacherSchoolSubject { get; set; }

        [NotMapped]
        public int? SchoolClassId { get; set; }
        public virtual SchoolClass SchoolClass { get; set; }
    }
}