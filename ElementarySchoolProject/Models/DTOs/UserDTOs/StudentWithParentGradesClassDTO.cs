﻿using ElementarySchoolProject.Models.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElementarySchoolProject.Models.DTOs.UserDTOs
{
    public class StudentWithParentGradesClassDTO
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public UserSimpleViewDTO Parent { get; set; }

        public SchoolClassDTO Class { get; set; }

        public IEnumerable<GradeDTO> Grades { get; set; }
    }
}