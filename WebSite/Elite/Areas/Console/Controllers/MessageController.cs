using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hms.Web.Areas.Console.Controllers
{
    public class MessageController : Controller
    {
        // GET: Console/Message
        public ActionResult Index()
        {
            return View();
        }
    }
}