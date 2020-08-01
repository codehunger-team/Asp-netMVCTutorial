using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolEasy.Web.Areas.School.Models
{
    public class CourseMasterModel
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public List<Courses> CourseList { get; set; }
    }

    public class Courses
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
    }
}