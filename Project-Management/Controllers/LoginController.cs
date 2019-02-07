using Project_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Project_Management.Controllers
{
    public class LoginController : Controller
    {
        private Login _login = null;
        
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

       
       
        [HttpPost]
        public ActionResult Index(Login user)
        {
            ViewBag.ErrorMessage = null;
            Login logUser = new Login();
            if (logUser.isAuthenticated(user))
            {
                var ticket = new FormsAuthenticationTicket("user.Email", true, 200);
                var encrypted = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                cookie.Expires = DateTime.Now.AddMinutes(200);
                cookie.HttpOnly = true;
                Response.Cookies.Add(cookie);
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ViewBag.ErrorMessage = "Wrong Credentials";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Home", "Index");
        }

    }
}