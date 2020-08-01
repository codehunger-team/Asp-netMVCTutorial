using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolEasy.Web.Areas.School.Models
{
    public class RegistrationMasterModel
    {
	    public int id { get; set; }
        [Required]
        public int CourseId { get; set; }
        public decimal RegistrationAmount { get; set; }
        public List<RegistrationFeeList> RegistrationFeeList { get; set; }
    }

    public class RegistrationFeeList
    {
        public string CourseName { get; set; }
        public int id { get; set; }
        public decimal RegistrationAmount { get; set; }
    }
}