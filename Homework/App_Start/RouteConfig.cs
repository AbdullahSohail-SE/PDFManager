using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Homework
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "DeleteRoute",
               url: "Home/DeletePDF/{id}",
               defaults: new { controller = "Home", action = "DeletePDF"}
           );
            routes.MapRoute(
               name: "PreviewRoute",
               url: "Home/PreviewPDF/{id}",
               defaults: new { controller = "Home", action = "PreviewPDF" }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
