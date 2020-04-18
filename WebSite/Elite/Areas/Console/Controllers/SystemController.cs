using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hms.Web.Areas.Console.Controllers
{
    public class SystemController : Controller
    {
        // GET: Console/System
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 网站栏目管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Pages()
        {
            return View();
        }

        /// <summary>
        /// 内容管理
        /// </summary>
        /// <returns></returns>
        public ActionResult WebSite()
        {
            return View();
        }
        public ActionResult Email()
        {
            return View();
        }
        public ActionResult Sms()
        {
            return View();
        }
        public ActionResult Element()
        {
            return View();
        }

        public ActionResult Dictionary()
        {
            return View();
        }

        public ActionResult Logs()
        {
            return View();
        }

    }
}