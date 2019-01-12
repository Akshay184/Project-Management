using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Project_Management.Models;
using System.IO;

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
            Filename = Filename + extension;

             Filename = Path.Combine(Server.MapPath("~/UserProfileImage/"), Filename);
            

            NewUser1.Register(NewUser,Filename);
            return RedirectToAction("Index");

        }


    }
}