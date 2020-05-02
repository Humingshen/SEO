using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Elite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "ActionOnly",
                "{action}/{id}",
                new { controller = "CN", action = "Index", id = UrlParameter.Optional },
                new { action = "Index|About|Contact|Brand|Info|Product|News|Media" }
            );
            routes.MapRoute(
                name: "CN",
                url: "zh-cn/{action}/{id}",
                defaults: new { controller = "CN", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                 name: "EN",
                 url: "en-us/{action}/{id}",
                 defaults: new { controller = "US", action = "Index", id = UrlParameter.Optional }
             );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
