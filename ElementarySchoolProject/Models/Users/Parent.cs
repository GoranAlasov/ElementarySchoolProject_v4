﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElementarySchoolProject.Models
{
    public class Parent : ApplicationUser
    {
        public virtual List<Student> Students { get; set; }
    }
}