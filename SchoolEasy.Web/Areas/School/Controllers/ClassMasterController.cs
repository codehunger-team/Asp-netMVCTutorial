using SchoolEasy.Database;
using SchoolEasy.Web.Areas.School.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolEasy.Web.Areas.School.Controllers
{
    public class ClassMasterController : Controller
    {
        SchoolAppEntities db = new SchoolAppEntities();
        // GET: School/ClassMaster
        public ActionResult Index()
        {
            ClassMasterModel model = new ClassMasterModel();
            string SchoolCode = Convert.ToString(Session["SchoolCode"]);
            ViewBag.CourseId = new SelectList((from p in db.CourseMasters where p.SchoolCode == SchoolCode select p).ToList(), "CourseId", "CourseName");
            return View(model);
        }

        [HttpPost]
        public JsonResult SaveClass(ClassMasterModel model)
        {
            var IsSuccess = false;
            var msg = "";
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            try
            {
                if(ModelState.IsValid)
                {
                    var checkCLassName = db.ClassMasters.Where(x => x.ClassName.ToUpper() == model.ClassName.ToUpper().Trim()).Count();
                    if (checkCLassName == 0)
                    {
                        ClassMaster classM = new ClassMaster();
                        classM.ClassName = model.ClassName;
                        classM.CourseId = model.CourseId;
                        db.ClassMasters.Add(classM);
                        db.SaveChanges();
                        IsSuccess = true;
                        msg = "Data Saved Successfully";
                    }
                    else
                    {
                        IsSuccess = false;
                        msg = "Class Name Already Exists";
                    }
                }
                else
                {
                    IsSuccess = false;
                    msg = "Validation Error";
                }            
            }
            catch (Exception ex)
            {
                IsSuccess = false;
                msg = ex.Message;
            }
            var jsonData = new { IsSuccess, msg };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult UpdateClass(ClassMasterModel cm)
        {
            var isSuccess = false;
            var message = "";
            try
            {
                if (cm.Id == 0)
                {
                    isSuccess = false;
                    message = " Class With this Name Not Found.Try Again.";
                }
                ClassMaster model = db.ClassMasters.Find(cm.Id);
                model.CourseId = cm.CourseId;
                model.ClassName = cm.ClassName.Trim();
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                isSuccess = true;
                message = "Data Modified Successfully";
                return Json("Data Modified Successfully", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                isSuccess = false;
                message = ex.Message;
            }
            var jsonData = new { isSuccess, message };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult ViewCLassList()
        {
            ClassMasterModel model = new ClassMasterModel();
            var data = (from p in db.ClassMasters
                        select new ClassList
                        {
                            Id=p.ClassId,
                            ClassName=p.ClassName,
                            CourseName=p.CourseMaster.CourseName
                        }).ToList();

            model.ClassList = data;
            return PartialView("_CLassList", model);
        }


        public ActionResult EditClass(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpNotFoundResult("Class With this Name Not Found.Try Again.");
            }
            try
            {
                string SchoolCode = Convert.ToString(Session["SchoolCode"]);
                int ClassId = 0;
                int.TryParse(id, out ClassId);
                ClassMaster model = db.ClassMasters.Find(ClassId);
                ClassMasterModel cm = new ClassMasterModel();
                cm.Id = model.ClassId;
                cm.ClassName = model.ClassName;
                cm.CourseId = model.CourseId;
                ViewBag.CourseId = new SelectList((from p in db.CourseMasters where p.SchoolCode == SchoolCode select p), "CourseId", "CourseName", model.CourseId).ToList();
                return Json(cm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Some Error Occurred.Unable to Fetch Data.Try Again. " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult DeleteClass(string id)
        {
            var isSuccess = false;
            var message = "";
            if (string.IsNullOrEmpty(id))
            {
                return new HttpNotFoundResult("Class With this Name Not Found.Try Again.");
            }
            try
            {
                int ClassId = 0;
                int.TryParse(id, out ClassId);
                ClassMaster model = db.ClassMasters.Find(ClassId);
                db.Entry(model).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                isSuccess = true;
                message = "Class Deleted Successfully";
            }
            catch (Exception ex)
            {
                var ErrMsg = "";
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException == null)
                    {
                        ErrMsg = ex.InnerException.Message;
                    }
                    else
                    {
                        ErrMsg = ex.InnerException.InnerException.Message;
                    }
                }
                else
                {
                    ErrMsg = ex.Message;
                }
                if (ErrMsg.Contains("The DELETE statement conflicted with the REFERENCE constraint "))
                {
                    ErrMsg = "This information is already being used in system somewhere, can't be deleted!";
                }
                isSuccess = false;
                message = ErrMsg;
            }
            var jsonData = new { isSuccess, message };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
    }
}