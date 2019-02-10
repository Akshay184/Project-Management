using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_Management.Models;

namespace Project_Management.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Project

        //    [Authorize]
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
            ToAdd.AddProject(AddProject,(int)Session["UserId"]);
            ProjectMembers add = new ProjectMembers();
            add.AddAdmin((int)Session["UserId"],AddProject.Name);
            return RedirectToAction("AddMembers",new {Id = AddProject.Name });
        }

        public ActionResult AddMembers()
        {
            Users organization  = new Users();
            ViewBag.List = organization.Organistaion((int) Session["UserId"]);
            return View();
        }

        public PartialViewResult AddToGroup(string ProjectName,int UserId)
        {
            ProjectMembers addMember = new ProjectMembers(); ;
            ViewBag.Member =   addMember.ProjectMember(UserId,ProjectName);
            return PartialView("AddToGroup");

        }

        public ActionResult Projects()
        {
            Projects List = new Projects();
            ViewBag.Projects = List.ListProject((int)Session["UserId"]);
            return View();
        }
    }
}