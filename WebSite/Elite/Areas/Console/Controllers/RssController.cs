using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hms.Web.Areas.Console.Controllers
{
    public class RssController : Controller
    {
        // GET: Console/Rss
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Link()
        {
            return View();
        }
        public ActionResult Content()
        {
            return View();
        }
    }
}