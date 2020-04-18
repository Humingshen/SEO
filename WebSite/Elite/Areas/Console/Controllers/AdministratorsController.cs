using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hms.Web.Areas.Console.Controllers
{
    public class AdministratorsController : Controller
    {
        // GET: Console/Administrators
        public ActionResult List()
        {
            return View();
        }
        public ActionResult Role()
        {
            return View();
        }
        public ActionResult RoleForm()
        {
            return View();
        }
        public ActionResult Authorities()
        {
            return View();
        }
    }
}