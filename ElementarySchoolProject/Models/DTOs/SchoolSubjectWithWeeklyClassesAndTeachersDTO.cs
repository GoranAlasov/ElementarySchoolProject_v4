using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ElementarySchoolProject.Models.DTOs.UserDTOs;

namespace ElementarySchoolProject.Models.DTOs
{
    public class SchoolSubjectWithWeeklyClassesAndTeachersDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int WeeklyClasses { get; set; }
        public IEnumerable<UserSimpleViewDTO> Teachers { get; set; }
    }
}