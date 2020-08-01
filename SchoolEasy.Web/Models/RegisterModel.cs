using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolEasy.Web.Models
{
    public class RegisterModel
    {
        public int Id { get; set; }

        [Required]
        public string SchoolName { get; set; }
        [Required]
        public string SchoolCode { get; set; }
        [Required]
        public string SchoolEmail { get; set; }
        [Required]
        public string SchoolContactNo { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password",ErrorMessage ="Password must be same")]
        public string ConfirmPassword { get; set; }
    }
}