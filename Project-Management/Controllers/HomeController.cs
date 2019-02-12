using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_Management.Models;
using System.IO;
using System.Web.Security;

namespace Project_Management.Controllers
{

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
            
            Filename = Filename + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + extension;
            NewUser1.Register(NewUser, Filename);
            Filename = Path.Combine(Server.MapPath("~/UserProfileImage/"), Filename);
            NewUser.ImageUpload.SaveAs(Filename);
            Guid activationCode = Guid.NewGuid();
            NewUser.ActivationGuid = activationCode.ToString();


           
            string link = "<br /><a href = '" + string.Format("{0}://{1}/Login/Activation/{2}", Request.Url.Scheme, Request.Url.Authority, NewUser.ActivationGuid) + "'>Click here to activate your account.</a>";
            NewUser1.ActivationEmail(NewUser, link);

            return RedirectToAction("Index");

        }

       





    }


}