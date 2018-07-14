using BooksStore.Models;
using BooksStore.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BooksStore.Controllers
{
    public class AuthorsController : Controller
    {
        // GET: Authors
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AuthorsSelectList(int? selectedId)
        {
            ViewBag.AuthorId = SelectListHelper.GetAuthorList(selectedId);
            return PartialView();
        }

        public ActionResult AddAuthor(string name)
        {
            AuthorsService service = new AuthorsService();
            int id = service.AddAuthor(name);
            return RedirectToAction("AuthorsSelectList", new { selectedId = id });
        }
        
    }
}