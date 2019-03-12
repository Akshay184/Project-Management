using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Project_Management.Models;

namespace Project_Management.Controllers
{
    public class ChatController : Controller
    {
        // GET: Chat
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PrivateChat(int ProjectId)
        {
            ProjectMembers group = new ProjectMembers();
            Login lg = new Login();
            ViewBag.id = (Convert.ToInt16(User.Identity.GetUserName()));
            ViewBag.UserName = lg.UserName(Convert.ToInt16(User.Identity.GetUserName()));
            ViewBag.Room = ProjectId ;
            Messages mssg = new Messages();
            ViewBag.ShowMessage = mssg.GetMessgae(ProjectId);
            return View();
        }
    }
}