using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hms.Web.Areas.Console.Controllers
{
    public class UserController : Controller
    {
        // GET: Console/User
        public ActionResult Info()
        {
            return View();
        }

        public ActionResult PassWord()
        {
            return View();
        }
        
        public ActionResult List()
        {
            return View();
        }
        public ActionResult UserForm()
        {
            return View();
        }
    }
}