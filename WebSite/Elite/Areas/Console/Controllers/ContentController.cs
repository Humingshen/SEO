using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hms.Web.Areas.Console.Controllers
{
    public class ContentController : Controller
    {
        // GET: Console/Content
        public ActionResult List()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Tags()
        {
            return View();
        }
        public ActionResult Comment()
        {
            return View();
        }
    }
}