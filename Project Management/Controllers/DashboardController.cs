using Project_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Project_Management.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {

            return View();

        }

        public ActionResult Profile(int auth)
        {
            Users Profile = new Users();
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
            return View(Edit.Profile(Convert.ToInt16(User.Identity.GetUserName())));
        }

        [HttpPost]
        public ActionResult Edit(Users ToEdit)
        {
            Users Editing = new Users();
            Editing.Edit(ToEdit, (Convert.ToInt16(User.Identity.GetUserName())));
            return RedirectToAction("Profile");
        }

        public ActionResult Requests()
        {

            return View();
        }

        /*public ActionResult Logout()
        {
            Session["UserId"] = null;
            Session["UserName"] = null;
            return View();
        }*/
        public ActionResult Message(int ProjectId)
        {
            ProjectMembers group = new ProjectMembers();
            Login lg = new Login();
            ViewBag.id = (Convert.ToInt16(User.Identity.GetUserName()));
            ViewBag.UserName = lg.UserName(Convert.ToInt16(User.Identity.GetUserName()));
            ViewBag.Room = ProjectId;
            Messages mssg = new Messages();
            ViewBag.ShowMessage = mssg.GetMessgae(ProjectId);
            return View();
        }

    }
}