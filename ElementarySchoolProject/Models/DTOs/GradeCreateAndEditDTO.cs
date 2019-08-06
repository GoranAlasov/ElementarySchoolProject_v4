using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElementarySchoolProject.Models.DTOs
{
    public class GradeCreateAndEditDTO
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTime DateOfGrading { get; set; }
        public string StudentId { get; set; }
        public int TeacherSubjectId { get; set; }
    }
}