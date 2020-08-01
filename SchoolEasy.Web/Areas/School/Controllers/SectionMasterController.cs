using SchoolEasy.Database;
using SchoolEasy.Web.Areas.School.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolEasy.Web.Areas.School.Controllers
{
    public class SectionMasterController : Controller
    {
        SchoolAppEntities db = new SchoolAppEntities();
        // GET: School/SectionMaster
        public ActionResult Index()
        {
            SectionMasterModel model = new SectionMasterModel();
            string SchoolCode = Convert.ToString(Session["SchoolCode"]);
            var sectionList = (from p in db.SectionMasters
                               where p.SchoolCode == SchoolCode
                               select new SectionList
                               {
                                   SectionId = p.SectionId,
                                   SectionName = p.SectionName,
                                   CourseName = p.ClassMaster.CourseMaster.CourseName,
                                   ClassName = p.ClassMaster.ClassName
                               }).ToList();
            model.SectionList = sectionList;
            return View(model);
        }

        public ActionResult Create()
        {
            SectionMasterModel model = new SectionMasterModel();
            string SchoolCode = Convert.ToString(Session["SchoolCode"]);
            ViewBag.CourseId = new SelectList((from p in db.CourseMasters where p.SchoolCode == SchoolCode select p).ToList(), "CourseId", "CourseName");
            ViewBag.ClassId = new SelectList(Enumerable.Empty<SelectListItem>());
            return View(model);
        }
        public ActionResult FillClass(int CourseId)
        {
            var classes = new SelectList(db.ClassMasters.Where(c => c.CourseId == CourseId).ToList(), "ClassId", "ClassName");
            return Json(classes, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(SectionMasterModel model)
        {
            if (ModelState.IsValid)
            {
                string SchoolCode = Convert.ToString(Session["SchoolCode"]);
                SectionMaster sectionMaster = new SectionMaster();
                sectionMaster.SectionName = model.SectionName;
                sectionMaster.ClassId = model.ClassId;
                sectionMaster.SchoolCode = SchoolCode;
                db.SectionMasters.Add(sectionMaster);
                db.SaveChanges();
                TempData["message"] = "Data Saved Successfully";
            }
            else
            {
                TempData["message"] = "Validation Error";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int SectionId)
        {
            SectionMasterModel model = new SectionMasterModel();
            string SchoolCode = Convert.ToString(Session["SchoolCode"]);
            var Section = db.SectionMasters.Find(SectionId);
            model.SectionId = Section.SectionId;
            model.ClassId = Section.ClassId;
            model.CourseId = Section.ClassMaster.CourseId;
            model.SectionName = Section.SectionName;
            ViewBag.CourseId = new SelectList((from p in db.CourseMasters where p.SchoolCode == SchoolCode select p).ToList(), "CourseId", "CourseName", model.CourseId);
            ViewBag.classid = new SelectList(db.ClassMasters.Where(c => c.CourseId == model.CourseId).ToList(), "ClassId", "ClassName", Section.ClassId);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(SectionMasterModel model)
        {
            if (ModelState.IsValid)
            {
                string SchoolCode = Convert.ToString(Session["SchoolCode"]);
                SectionMaster sectionMaster = db.SectionMasters.Find(model.SectionId);
                sectionMaster.SectionName = model.SectionName;
                sectionMaster.ClassId = model.ClassId;
                sectionMaster.SchoolCode = SchoolCode;
                db.Entry(sectionMaster).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["message"] = "Data Updated Successfully";
            }
            else
            {
                TempData["message"] = "Validation Error";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int SectionId)
        {
            if (ModelState.IsValid)
            {
                SectionMaster sectionMaster = db.SectionMasters.Find(SectionId);
                db.Entry(sectionMaster).State = System.Data.Entity.EntityState.Deleted;
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