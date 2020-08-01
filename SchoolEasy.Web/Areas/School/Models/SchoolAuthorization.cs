using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolEasy.Web.Areas.School.Models
{
    public class SchoolAuthorization : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;

            string SchoolCode = Convert.ToString(HttpContext.Current.Session["SchoolCode"]);
            if (SchoolCode == "")
            {
                filterContext.Result = new RedirectResult("~/Account/Login");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}