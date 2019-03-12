using Project_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Project_Management.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var a = Request.Url.Segments.Last();
            if (Request.QueryString["Message"] == "qmsoish")
            {
                ViewBag.Activate = "Activated Successfully";
            }
            return View();
        }


        [HttpPost]
        public ActionResult Index(Login user)
        {
            ViewBag.ErrorMessage = null;
            Login login = new Login();
            var auth = login.Authenticate(user);
            if (auth == -1)
            {
                ViewBag.ErrorMessage = "Please Verify Your Email";
                return View();
            }
            else if (auth == 0)
            {
                ViewBag.ErrorMessage = "Wrong Credentials";
                return View();
            }
            else

            {
               
                var ticket = new FormsAuthenticationTicket(auth.ToString(), true, 200);
                var encrypted = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                cookie.Expires = DateTime.Now.AddMinutes(200);
                cookie.HttpOnly = true;
                Response.Cookies.Add(cookie);
                return RedirectToAction("Index", "Dashboard");
                
            }

        }



       

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Activation()
        {
            ViewBag.ActivationMessage = "Invalid Activation Code";
            if (RouteData.Values["id"] != null)
            {
                string Activationcode = RouteData.Values["id"].ToString();
                using (dbProjectManagementEntities db = new dbProjectManagementEntities())
                {

                    tblUser ToActivate = db.tblUsers.Where(m => m.GUID == Activationcode).SingleOrDefault();
                    if (ToActivate != null)
                    {
                        ToActivate.UserStatus = true;
                        db.SaveChanges();

                    }


                }


            }

            return RedirectToAction("Index", new { Message = "qmsoish" });
        }
    }
}