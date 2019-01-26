using Project_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Management.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View();
            }
           
        }

        public ActionResult Profile()
        {
            Users Profile  = new Users();
           

            return View(Profile.Profile((int)(Session["UserId"])));
        }

        public ActionResult Edit()
        {
            Users Edit = new Users();
            return View(Edit.Profile((int)(Session["UserId"])));
        }

        public ActionResult Logout()
        {
            Session["UserId"] = null;
            Session["UserName"] = null;
            return View();
        }
        public ActionResult Message()
        {
            return View();
        }
    }
}