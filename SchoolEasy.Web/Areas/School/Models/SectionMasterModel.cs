using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolEasy.Web.Areas.School.Models
{
    public class SectionMasterModel
    {
        [Required(ErrorMessage = "Select Course")]
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Select Course")]
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        [Required(ErrorMessage = "Select Course")]
        public string SectionName { get; set; }
        public List<SectionList> SectionList { get; set; }
    }

    public class SectionList
    {
        public int SectionId { get; set; }
        public string SectionName { get; set; }
        public string ClassName { get; set; }
        public string CourseName { get; set; }
    }
}