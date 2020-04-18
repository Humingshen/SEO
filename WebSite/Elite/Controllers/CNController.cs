using Hms.Web.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hms.Web.Controllers
{
    public class CNController : Controller
    {

        protected WebSiteEntities _db = new WebSiteEntities();

        string version = "zh-CN";

        // GET: CN
        public ActionResult Index()
        {
            ViewBag.Page = _db.T_Pages.Find(1);
            ViewBag.Elements = _db.T_Elements.Where(i => i.PageId == 1 && i.Enable).ToList();
            var news = _db.T_Article.Where(i => i.Tags == "industry" && i.State == 1 && i.Version == version).OrderByDescending(o => o.Created).ToList();
            ViewBag.News = news.Skip(0).Take(3).ToList();
            return View();
        }
        // GET: CN
        public ActionResult About()
        {
            ViewBag.Page = _db.T_Pages.Find(2);
            ViewBag.Elements = _db.T_Elements.Where(i => i.PageId == 2 && i.Enable).ToList();
            var news = _db.T_Article.Where(i => i.Tags == "certificate" && i.State == 1 && i.Version == version).OrderByDescending(o => o.Created).ToList();
            ViewBag.News = news;
            return View();
        }
        // GET: CN
        public ActionResult Product()
        {
            ViewBag.Page = _db.T_Pages.Find(3);
            ViewBag.Elements = _db.T_Elements.Where(i => i.PageId == 3 && i.Enable).ToList();
            var news = _db.T_Article.Where(i => i.Tags == "knowledge" && i.State == 1 && i.Version == version).OrderByDescending(o => o.Created).ToList();
            ViewBag.News = news;
            return View();

        }

        // GET: CN
        public ActionResult News(string id)
        {
            ViewBag.Page = _db.T_Pages.Find(4);
            ViewBag.Elements = _db.T_Elements.Where(i => i.PageId == 4 && i.Enable).ToList();
            var news = _db.T_Article.Where(i => i.Tags == id && i.State == 1 && i.Version == version).OrderByDescending(o => o.Created).ToList();
            ViewBag.News = news;
            return View();
        }

        // GET: CN
        public ActionResult Media()
        {
            ViewBag.Page = _db.T_Pages.Find(5);
            ViewBag.Elements = _db.T_Elements.Where(i => i.PageId == 5 && i.Enable).ToList();
            var news = _db.T_Article.Where(i => i.Tags == "media" && i.State == 1 && i.Version == version).OrderByDescending(o => o.Created).ToList();
            ViewBag.News = news;
            return View();
        }
        // GET: CN
        public ActionResult Contact()
        {
            ViewBag.Page = _db.T_Pages.Find(6);
            ViewBag.Elements = _db.T_Elements.Where(i => i.PageId == 6 && i.Enable).ToList();
            return View();
        }
        // GET: CN
        public ActionResult Service()
        {
            return View();

        }
        // GET: CN
        public ActionResult Brand()
        {
            return View();

        }
    }
}