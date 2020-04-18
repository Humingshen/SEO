using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hms.Web.Areas.Console.Controllers
{
    public class ProductController : Controller
    {
        // GET: Console/Product
        public ActionResult List()
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
        public ActionResult Editor()
        {
            return View();
        }
    }
}