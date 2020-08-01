using SchoolEasy.Web.Areas.School.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolEasy.Web.Areas.School.Controllers
{
    [SchoolAuthorization]
    public class DashboardController : Controller
    {
        // GET: School/Dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}