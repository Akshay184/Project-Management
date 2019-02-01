using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Project_Management.Models;
using System.IO;
using System.Web.Security;

namespace Project_Management.Controllers
{
   // [System.Runtime.InteropServices.Guid("0C2C6A46-45B7-4907-91DA-D86AC48944BB")]
    public class HomeController : Controller
    {
        // GET: Home
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
        public ActionResult Register(Users NewUser)
        {
            Users NewUser1 = new Users();

            string Filename = Path.GetFileNameWithoutExtension(NewUser.ImageUpload.FileName);
            string extension = Path.GetExtension(NewUser.ImageUpload.FileName); 
            Filename = Filename + extension;

             Filename = Path.Combine(Server.MapPath("~/UserProfileImage/"), Filename);
            Guid activationCode = Guid.NewGuid();
            NewUser.ActivationGuid = activationCode.ToString();
            

            NewUser1.Register(NewUser,Filename);
            string link = "<br /><a href = '" + string.Format("{0}://{1}/Home/Activation/{2}", Request.Url.Scheme, Request.Url.Authority, NewUser.ActivationGuid) + "'>Click here to activate your account.</a>";
            NewUser1.ActivationEmail(NewUser,link);

            return RedirectToAction("Index");

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
                        ViewBag.ActivationMessage = "Activated Successfully";
                    }


                }


            }

            return View();
        }
        [HttpGet]
        public ActionResult Login()
            {
                return View();
            }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users ToLogin)
        {
            ViewBag.Message = "Invalid Credentials";
            string a = ToLogin.Password;
            string b = ToLogin.Email;
           
            
                dbProjectManagementEntities2 db = new dbProjectManagementEntities2();

                var user = db.tblUsers.Where(m => m.UserEmail.Equals(ToLogin.Email) && m.UserPassword.Equals(ToLogin.Password)).FirstOrDefault();
           
                if (user != null)
                {

                   
                    Session["UserId"] = user.UserId;
                    Session["UserName"] = user.UserName;
                    ViewBag.Message = "Login Successgful";
                }
           
            return RedirectToAction("Index","Dashboard");
        }

      


    }
}