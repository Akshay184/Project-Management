﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Project_Management
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "ProjectMembers",
                "Projects/AddMembers/{ProjectName}/{UserId}",
                new {Controller="Projects", action = "AddToGroup"});

            routes.MapRoute(
                "Profile",
                "Dashboard/Profile/{auth}",
                new { Controller = "Dashboard", action = "Profile" });

            routes.MapRoute(
                "Projects",
                "Dashboard/Message/{ProjectId}",
                new { Controller = "Dashboard", action = "Message" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
