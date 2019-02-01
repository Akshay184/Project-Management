﻿using Project_Management.Models;
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