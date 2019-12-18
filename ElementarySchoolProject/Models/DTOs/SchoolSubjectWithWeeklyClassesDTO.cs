﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElementarySchoolProject.Models.DTOs
{
    public class SchoolSubjectWithWeeklyClassesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int WeeklyClasses { get; set; }
    }
}