using Microsoft.AspNet.Identity;
using Project_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Management.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        public ActionResult Index()
        {


            return View();
        }

        [HttpGet]
        public ActionResult CreateNewProject()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateNewProject(Projects AddProject)
        {
            Projects ToAdd = new Projects();
            ToAdd.AddProject(AddProject, (Convert.ToInt16(User.Identity.GetUserName())));
            ProjectMembers add = new ProjectMembers();
            add.AddAdmin((Convert.ToInt16(User.Identity.GetUserName())), AddProject.Name);
            return RedirectToAction("AddMembers", new { Id = AddProject.Name });
        }

        public ActionResult AddMembers()
        {
            Users organization = new Users();
            ViewBag.List = organization.Organistaion((Convert.ToInt16(User.Identity.GetUserName())));
            return View();
        }

        public PartialViewResult AddToGroup(string ProjectName, int UserId)
        {
            ProjectMembers addMember = new ProjectMembers(); ;
            ViewBag.Member = addMember.ProjectMember(UserId, ProjectName);
            return PartialView("AddToGroup");

        }

        public ActionResult Projects()
        {
            ProjectMembers List = new ProjectMembers();
            ViewBag.Projects = List.Groups((Convert.ToInt16(User.Identity.GetUserName())));

            return View();
        }

        public ActionResult PrivateChat()
        {
            Users organization = new Users();
            ViewBag.PrivateChat = organization.Organistaion((Convert.ToInt16(User.Identity.GetUserName())));
            return View();
        }
    }
}