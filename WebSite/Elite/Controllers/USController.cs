using Hms.Web.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hms.Web.Controllers
{
    public class USController : Controller
    {
        protected WebSiteEntities _db = new WebSiteEntities();

        string version = "en-US";

        // GET: CN
        public ActionResult Index()
        {
            ViewBag.Page = _db.T_Pages.Find(11);
            ViewBag.Elements = _db.T_Elements.Where(i => i.PageId == 11 && i.Enable).ToList();
            var news = _db.T_Article.Where(i => i.Tags == "industry" && i.State == 1 && i.Version == version).OrderByDescending(o => o.Created).ToList();
            ViewBag.News = news.Skip(0).Take(3).ToList();
            return View();
        }
        // GET: CN
        public ActionResult About()
        {
            ViewBag.Page = _db.T_Pages.Find(12);
            ViewBag.Elements = _db.T_Elements.Where(i => i.PageId == 12 && i.Enable).ToList();
            var news = _db.T_Article.Where(i => i.Tags == "certificate" && i.State == 1 && i.Version == version).OrderByDescending(o => o.Created).ToList();
            ViewBag.News = news;
            return View();
        }
        // GET: CN
        public ActionResult Product()
        {
            ViewBag.Page = _db.T_Pages.Find(13);
            ViewBag.Elements = _db.T_Elements.Where(i => i.PageId == 13 && i.Enable).ToList();
            var news = _db.T_Article.Where(i => i.Tags == "knowledge" && i.State == 1 && i.Version == version).OrderByDescending(o => o.Created).ToList();
            ViewBag.News = news;
            return View();

        }

        // GET: CN
        public ActionResult News(string id)
        {
            ViewBag.Page = _db.T_Pages.Find(14);
            ViewBag.Elements = _db.T_Elements.Where(i => i.PageId == 14 && i.Enable).ToList();
            var news = _db.T_Article.Where(i => i.Tags == id && i.State == 1 && i.Version == version).OrderByDescending(o => o.Created).ToList();
            ViewBag.News = news;
            return View();
        }

        // GET: CN
        public ActionResult Media()
        {
            ViewBag.Page = _db.T_Pages.Find(15);
            ViewBag.Elements = _db.T_Elements.Where(i => i.PageId == 15 && i.Enable).ToList();
            var news = _db.T_Article.Where(i => i.Tags == "media" && i.State == 1 && i.Version == version).OrderByDescending(o => o.Created).ToList();
            ViewBag.News = news;
            return View();
        }
        // GET: CN
        public ActionResult Contact()
        {
            ViewBag.Page = _db.T_Pages.Find(16);
            ViewBag.Elements = _db.T_Elements.Where(i => i.PageId == 16 && i.Enable).ToList();
            return View();
        }
    }
}