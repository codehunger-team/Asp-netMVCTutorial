using SchoolEasy.Database;
using SchoolEasy.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SchoolEasy.Web.Controllers
{
    public class AccountController : Controller
    {
        SchoolAppEntities db = new SchoolAppEntities();
        // GET: Account
        public ActionResult Login()
        {
            if(Request.IsAuthenticated)
            {
                string cookieName = FormsAuthentication.FormsCookieName; //Find cookie name
                HttpCookie authCookie = HttpContext.Request.Cookies[cookieName]; //Get the cookie by it's name
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value); //Decrypt it
                string UserName = ticket.Name; //You have the UserName!
                Session["SchoolCode"] = UserName;
                return RedirectToAction("Index", "Dashboard",new { area="School"});
            }
            LoginModel lm = new LoginModel();
            return View(lm);
        }
        [HttpPost]
        public ActionResult Login(LoginModel lm)
        {
            try
            {
                Request.Cookies.Clear();
                if (ModelState.IsValid)
                {
                    string Password = encryption(lm.UserPassword);
                    var check = (from p in db.tb_Login where p.UserName == lm.UserName && p.UserPassword == Password && p.Active==true select p).Count();
                    if(check>0)
                    {
                        DateTime expiryDate = DateTime.Now.AddDays(30);

                        //create a new forms auth ticket
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2, lm.UserName, DateTime.Now, expiryDate, lm.isRemember, String.Empty);


                        //encrypt the ticket
                        string encryptedTicket = FormsAuthentication.Encrypt(ticket);


                        //create a new authentication cookie - and set its expiration date
                        HttpCookie authenticationCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                        authenticationCookie.Expires = ticket.Expiration;


                        //add the cookie to the response.
                        Response.Cookies.Add(authenticationCookie);
                        Session["SchoolCode"] = lm.UserName;
                        TempData["message"] = "Login Successful";
                        return RedirectToAction("Index", "Dashboard", new { area = "School" });
                    }
                    else
                    {
                        TempData["message"] = "UnAuthorized School";
                        return View(lm);
                    }
                }
                else
                {
                    return View(lm);
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = "Some Error Occured";
                return View(lm);
            }
        }
        public ActionResult Register()
        {
            RegisterModel rm = new RegisterModel();
            return View(rm);
        }
        [HttpPost]
        public ActionResult Register(RegisterModel rm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    tb_SchoolRegister tr = new tb_SchoolRegister();
                    tr.SchoolName = rm.SchoolName;
                    tr.SchoolEmail = rm.SchoolEmail;
                    tr.SchoolCode = rm.SchoolCode;
                    tr.SchoolContactNo = rm.SchoolContactNo;
                    db.tb_SchoolRegister.Add(tr);
                    db.SaveChanges();

                    string password = encryption(rm.Password);
                    tb_Login tl = new tb_Login();
                    tl.UserName = rm.SchoolCode;
                    tl.UserPassword = password;
                    tl.Active = true;
                    db.tb_Login.Add(tl);
                    db.SaveChanges();
                    Session["SchoolCode"] = rm.SchoolCode;
                    TempData["message"] = "School Registered Successfully";
                    return RedirectToAction("Index", "Dashboard", new { area = "School" });
                }
                else
                {
                    return View(rm);
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
                return View(rm);
            }
        }
        public static string encryption(String password)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encrypt;
            UTF8Encoding encode = new UTF8Encoding();
            //encrypt the given password string into Encrypted data  
            encrypt = md5.ComputeHash(encode.GetBytes(password));
            StringBuilder encryptdata = new StringBuilder();
            //Create a new string by using the encrypted data  
            for (int i = 0; i < encrypt.Length; i++)
            {
                encryptdata.Append(encrypt[i].ToString());
            }
            return encryptdata.ToString();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Response.Cookies.Clear();
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account", new { area = "" });
        }
    }
}