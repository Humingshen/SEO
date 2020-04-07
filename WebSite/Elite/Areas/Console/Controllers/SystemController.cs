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
        public ActionResult Article()
        {
            return View();
        }
        public ActionResult Comment()
        {
            return View();
        }
        public ActionResult Product()
        {
            return View();
        }
        public ActionResult Element()
        {
            return View();
        }

    }
}