using SchoolEasy.Database;
using SchoolEasy.Web.Areas.School.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolEasy.Web.Areas.School.Controllers
{
    public class CourseMasterController : Controller
    {
        SchoolAppEntities db = new SchoolAppEntities();
        // GET: School/CourseMaster
        public ActionResult Index()
        {
            CourseMasterModel model = new CourseMasterModel();
            string SchoolCode = Convert.ToString(Session["SchoolCode"]);
            var courselist = (from p in db.CourseMasters
                              where p.SchoolCode == SchoolCode
                              select new Courses
                              { CourseId = p.CourseId,
                                CourseName = p.CourseName
                              }).ToList();
            model.CourseList = courselist;
            return View(model);
        }

        public ActionResult Create()
        {
            CourseMasterModel model = new CourseMasterModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CourseMasterModel model)
        {
            if(ModelState.IsValid)
            {
                string SchoolCode = Convert.ToString(Session["SchoolCode"]);
                CourseMaster courseMaster = new CourseMaster();
                courseMaster.CourseName = model.CourseName;
                courseMaster.SchoolCode = SchoolCode;
                db.CourseMasters.Add(courseMaster);
                db.SaveChanges();
                TempData["message"] = "Data Saved Successfully";
            }
            else
            {
                TempData["message"] = "Validation Error";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int CourseId)
        {
            CourseMasterModel model = new CourseMasterModel();
            var Course = db.CourseMasters.Where(x => x.CourseId == CourseId).FirstOrDefault();
            model.CourseId=Course.CourseId;
            model.CourseName = Course.CourseName;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CourseMasterModel model)
        {
            if (ModelState.IsValid)
            {
                string SchoolCode = Convert.ToString(Session["SchoolCode"]);
                CourseMaster courseMaster = db.CourseMasters.Find(model.CourseId);
                courseMaster.CourseName = model.CourseName;
                db.Entry(courseMaster).State=System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["message"] = "Data Updated Successfully";
            }
            else
            {
                TempData["message"] = "Validation Error";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int CourseId)
        {
            if (ModelState.IsValid)
            {
                CourseMaster courseMaster = db.CourseMasters.Find(CourseId);
                db.Entry(courseMaster).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                TempData["message"] = "Data Deleted Successfully";
            }
            else
            {
                TempData["message"] = "Validation Error";
            }
            return RedirectToAction("Index");
        }
    }
}