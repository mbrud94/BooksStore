using BooksStore.Models;
using BooksStore.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BooksStore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            BooksService service = new BooksService();
            return View(service.GetTopBooks());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            BooksStoreDBContext db = new BooksStoreDBContext();
            var books = db.Books.Where(b => b.Description == null);
            foreach (var b in books)
                db.Books.Remove(b);
            db.SaveChanges();
            return View();
        }
    }
}