using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hms.Web.Areas.Console.Controllers
{
    public class WorkorderController : Controller
    {
        // GET: Console/Workorder
        public ActionResult List()
        {
            return View();
        }
    }
}