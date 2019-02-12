using Project_Management.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View();
            }
           
        }

        public ActionResult Profile(int auth)
        {
            Users Profile  = new Users();
            if (Profile.Profile(auth) != null)
            {
                return View(Profile.Profile(auth));
            }
            else
            {
                return (HttpNotFound());
            }

            
        }
        [HttpGet]
        public ActionResult Edit()
        {
            Users Edit = new Users();
            return View(Edit.Profile((int)(Session["UserId"])));
        }

        [HttpPost]
        public ActionResult Edit(Users ToEdit)
        {
            Users Editing  = new Users();
            Editing.Edit(ToEdit,(int)Session["UserID"]);
            return RedirectToAction("Profile");
        }

        public ActionResult Requests()
        {

            return View();
        }

        public ActionResult Logout()
        {
            Session["UserId"] = null;
            Session["UserName"] = null;
            return View();
        }
        public ActionResult Message(string  ProjectId)
        {
            ProjectMembers group = new ProjectMembers();
            ViewBag.id = Session["UserId"];
            ViewBag.Room = ProjectId;
            return View();
        }
    }
}