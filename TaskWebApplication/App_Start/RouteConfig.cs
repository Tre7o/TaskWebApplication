using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TaskWebApplication
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Task", // name of the controller
            //    url: "task/{action}", // defines the url to trigger the controller
            //    defaults: new { controller = "Task", action = "ViewTask" }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "Task",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Task", action = "AddTask" }
           );

            //routes.MapRoute(
            //    name: "Test", // name of the controller
            //    url: "test/{action}", // defines the url to trigger the controller
            //    defaults: new { controller = "Test", action = "ShowMessage" }
            //);
        }
    }
}