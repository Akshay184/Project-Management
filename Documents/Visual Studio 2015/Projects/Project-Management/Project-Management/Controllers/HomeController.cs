using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_Management.Models;
using System.Data.Entity;
using System.Data;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using Microsoft.AspNet.Identity;
using System.IO;

namespace Project_Management.Controllers
{ //[System.Runtime.InteropServices.Guid("0C2C6A46-45B7-4907-91DA-D86AC48944BB")]
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        

        
        

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(UserRegistration NewUser)
        {
            
            UserRegistration NewUser1 = new UserRegistration();
            string Filename = Path.GetFileNameWithoutExtension(NewUser.ImageUpload.FileName);
            string extension = Path.GetExtension(NewUser.ImageUpload.FileName);
            Filename = Filename + extension;
            Filename = Path.Combine(Server.MapPath("~/ProfileImage/"), Filename);
            Guid activationCode = Guid.NewGuid();
            NewUser.ActivationGuid = activationCode.ToString();

            NewUser1.NewUser(NewUser,Filename);
            string link = "<br/><a href = '" + string.Format("{0}://{1}/Home/Activation/{2}", Request.Url.Scheme, Request.Url.Authority, NewUser.ActivationGuid) + "'>Click here to activate your account.</a>";
            NewUser.ActivationEmail(NewUser, link);
                
            return RedirectToAction("Register");
                
            
        }

        public ActionResult Activation()
        {
            ViewBag.ActivationMessage = "Invalid Activation code";
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
                        ViewBag.ActivationMessage = "Activated Successfully";

                    }
                }
            }
            return Redirect("Login");

        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserRegistration ToLogin)
        {
            ViewBag.Message = "Invalid Credentials";
            string a = ToLogin.UserPassword;
            string b = ToLogin.UserEmail;

            dbProjectManagementEntities db = new dbProjectManagementEntities();
            var user = db.tblUsers.Where(m => m.UserEmail.Equals(ToLogin.UserEmail) && m.UserPassword.Equals(ToLogin.UserPassword)).FirstOrDefault();
            if(user != null)
            {
                Session["UserId"] = user.UserId;
                Session["UserName"] = user.UserName;
                ViewBag.Message = "Login Successful";
            }
            return View();      
        }
        public ActionResult Chat()
        {
            return View();
        }
    }


}
