using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hms.Web.Areas.Console.Controllers
{
    public class ForumController : Controller
    {

        public ActionResult List()
        {
            return View();
        }

        public ActionResult Replys()
        {
            return View();
        }
    }
}