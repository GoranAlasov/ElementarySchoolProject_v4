using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ElementarySchoolProject.Models
{
    public class Student : ApplicationUser
    {        
        public string ParentId { get; set; }                
        public virtual Parent Parent { get; set; }
        
        public virtual SchoolClass SchoolClass { get; set; }
        public virtual List<Grade> Grades { get; set; }
    }
}