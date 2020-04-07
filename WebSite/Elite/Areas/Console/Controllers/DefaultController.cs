using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hms.Web.Areas.Console.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Console/Default
        public ActionResult Index()
        {
            return View();
        }

        // GET: Console/Welcome
        public ActionResult Welcome()
        {
            return View();
        }

        public ActionResult Users()
        {
            return View();
        }

        // GET: Page
        public ActionResult Login()
        {
            return View();
        }

        // GET: Page
        public ActionResult Role()
        {
            return View();
        }
        // GET: Page
        public ActionResult Article()
        {
            return View();
        }
        // GET: Page
        public ActionResult Product()
        {
            return View();
        }
        public ActionResult ArtEdit(int id)
        {
            ViewBag.Art = "";
            return View();
        }
        // GET: Page
        public ActionResult Comment()
        {
            return View();
        }
        // GET: Page
        public ActionResult Visitors()
        {
            return View();
        }
        // GET: Page
        public ActionResult Authorize()
        {
            return View();
        }
        // GET: Page
        public ActionResult Video()
        {
            return View();
        }
        // GET: Page
        public ActionResult File()
        {
            return View();
        }
        // GET: Page
        public ActionResult DownLoad()
        {
            return View();
        }
        // GET: Page
        public ActionResult Password()
        {
            return View();
        }
    }
}