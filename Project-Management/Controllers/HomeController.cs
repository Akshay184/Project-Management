using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Project_Management.Models;

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
            NewUser1.Register(NewUser);
            return RedirectToAction("Index");

        }


    }
}