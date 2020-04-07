using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hms.Web.Areas.Console
{
    public class ConsoleAreaRegistration : AreaRegistration
    {
        public override string AreaName => "console";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            /*以下配置每一个区域必须配置*/

            context.MapRoute(
                this.AreaName + "_actionOnly",
                this.AreaName + "/{action}/{id}",
                new { area = this.AreaName, controller = "Default", action = "Index", id = UrlParameter.Optional },
                new { action = "Login|Welcome|Index" });

            context.MapRoute(
                this.AreaName + "_default",
                this.AreaName + "/{controller}/{action}/{id}",
                new { area = this.AreaName, controller = "Default", action = "Index", id = UrlParameter.Optional },
                new string[] { "Hms.Web.Areas." + this.AreaName + ".Controllers" });
        }
    }
}