using SchoolEasy.Database;
using SchoolEasy.Web.Areas.School.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolEasy.Web.Areas.School.Controllers
{
    public class RegistrationAmountController : Controller
    {
        SchoolAppEntities db = new SchoolAppEntities();
        // GET: School/RegistrationAmount
        public ActionResult Index()
        {
            string SchoolCode = Convert.ToString(Session["SchoolCode"]);
            RegistrationMasterModel model = new RegistrationMasterModel();
            List<RegistrationFeeList> data = (from a in db.RegistrationAmountMasters
                                               where a.SchoolCode == SchoolCode
                                               select new RegistrationFeeList
                                               {
                                                   id = a.Id,
                                                   CourseName = a.CourseMaster.CourseName,
                                                   RegistrationAmount = a.RegistrationAmount
                                               }).ToList();
            model.RegistrationFeeList = data;
            ViewBag.CourseId = new SelectList((from p in db.CourseMasters where p.SchoolCode == SchoolCode select new { CourseId = p.CourseId, CourseName = p.CourseName }).ToList(), "CourseId", "CourseName");
            return View(model);
        }

        [HttpPost]
        public JsonResult AddRegistrationAmount(RegistrationMasterModel model)
        {
            var isSuccess = false;
            var message = "";
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    string SchoolCode = Convert.ToString(Session["SchoolCode"]);
                    RegistrationAmountMaster ras = new RegistrationAmountMaster();
                    var checkData = (from a in db.RegistrationAmountMasters where a.CourseId == model.CourseId && a.SchoolCode == SchoolCode select a).Count();
                    if (checkData > 0)
                    {
                        var getId = (from a in db.RegistrationAmountMasters where a.CourseId == model.CourseId && a.SchoolCode == SchoolCode select a.Id).SingleOrDefault();
                        var register = db.RegistrationAmountMasters.Find(getId);
                        register.CourseId = model.CourseId;
                        register.RegistrationAmount = model.RegistrationAmount;
                        register.SchoolCode = SchoolCode;
                        db.Entry(register).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        transaction.Commit();
                        isSuccess = true;
                        TempData["success"] = "This course is already present, it is being updated !!";
                    }
                    else
                    {
                        ras.CourseId = model.CourseId;
                        ras.RegistrationAmount = model.RegistrationAmount;
                        ras.SchoolCode = SchoolCode;
                        db.RegistrationAmountMasters.Add(ras);
                        db.SaveChanges();
                        transaction.Commit();
                        isSuccess = true;
                        TempData["success"] = "Data Successfully Saved !!";
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
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
                    isSuccess = false;
                    message = ErrMsg;
                }
                var jsonData = new { isSuccess, message };
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(int? id)
        {

            RegistrationAmountMaster model = db.RegistrationAmountMasters.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            try

            {
                db.RegistrationAmountMasters.Remove(model);
                db.SaveChanges();
                TempData["success"] = " Deleted successfully";
                return RedirectToAction("Index");
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
                TempData["error"] = ErrMsg;
                return RedirectToAction("Index");
            }


        }
    }
}