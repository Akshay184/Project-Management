using Project_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Ajax.Utilities;

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
            Login logUser = new Login();
            var auth = logUser.Authenticate(user);
            
            if (auth == -1)
            {
                ViewBag.ErrorMessage = "Please Verify Your Email";
                return View();
            }
            else if(auth == 0)
            {
                ViewBag.ErrorMessage = "Wrong Credentials";
                return View();
            }
            else
            {
                Session["UserId"] = auth;
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
                using (dbProjectManagementEntities2 db = new dbProjectManagementEntities2())
                {

                    tblUser ToActivate = db.tblUsers.Where(m => m.GUID == Activationcode).SingleOrDefault();
                    if (ToActivate != null)
                    {
                        ToActivate.UserStatus = true;
                        db.SaveChanges();
                       
                    }


                }


            }

            return RedirectToAction("Index",new {Message= "qmsoish" });
        }

    }
}