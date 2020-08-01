using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolEasy.Web.Areas.School.Models
{
    public class RegistrationEntryModel
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Please Enter Student Name")]
        public string StudentName { get; set; }

        [Required(ErrorMessage = "Please Enter Father Name")]
        public string FatherName { get; set; }

        [Required(ErrorMessage = "Please Enter Registration No.")]
        public string RegistrationNo { get; set; }

        public string Address { get; set; }

        [Required(ErrorMessage = "Please Enter Contact No.")]
        public string MobileNo { get; set; }

        public decimal RegistrationAmount { get; set; }

        public int CourseId { get; set; }

        public int ClassId { get; set; }

        public List<RegistredStudentList> RegistredStudentList { get; set; }
    }

    public class RegistredStudentList
    {
        public int id { get; set; }
        public string StudentName { get; set; }
        public string FatherName { get; set; }
        public string RegistrationNo { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string CourseName { get; set; }
        public string ClassName { get; set; }
    }
}