using ElementarySchoolProject.Models.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElementarySchoolProject.Models.DTOs
{
    public class GradeDTO
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTime DateOfGrading { get; set; }
        public SchoolSubjectBasicDTO Subject { get; set; }
        public UserSimpleViewDTO GradingTeacher { get; set; }
        public StudentBasicDTO Student { get; set; }
    }
}