using BooksStore.Models;
using BooksStore.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BooksStore.Controllers
{
    public class BooksController : Controller
    {
        // GET: Books
        public ActionResult Index(int authorId = -1, int categoryId = -1, string searchString = "", string sortOrder = "TITLE")
        {
            BooksService service = new BooksService();
            ViewBag.SortOrder = sortOrder;
            if (categoryId != -1)
            {
                ViewBag.ListType = "Książki gatunku";
                if (!Request.IsAjaxRequest())
                    return View("BooksList", service.GetBooksByCategory(categoryId, sortOrder));
                else
                    return PartialView("BooksListPartial", service.GetBooksByCategory(categoryId, sortOrder));
            }
            else if (authorId != -1)
            {
                ViewBag.ListType = "Książki autora";
                if (!Request.IsAjaxRequest())
                    return View("BooksList", service.GetBooksByAuthor(authorId, sortOrder));
                else
                    return PartialView("BooksListPartial", service.GetBooksByAuthor(authorId, sortOrder));
            }
            else if(!String.IsNullOrEmpty(searchString))
            {
                ViewBag.SearchString = searchString;
                ViewBag.ListType = "Wyniki wyszukiwania";
                if (!Request.IsAjaxRequest())
                    return View("BooksList", service.SearchBooks(searchString, sortOrder));
                else
                   return PartialView("BooksListPartial", service.SearchBooks(searchString, sortOrder));
            }
            else
            {
                ViewBag.ListType = "Lista wszystkich książek";
                if (!Request.IsAjaxRequest())
                    return View("BooksList", service.GetAllBooks(sortOrder));
                else
                    return PartialView("BooksListPartial", service.GetAllBooks(sortOrder));
            }
        }

        public ActionResult Details(int id)
        {
            BooksService service = new BooksService();
            return View(service.FindById(id));
        }
        [Authorize(Roles ="Admins")]
        public ActionResult Create()
        {
            BooksStoreDBContext db = new BooksStoreDBContext();
            Book b = new Book();
            b.ReleaseDate = DateTime.Now;
            ViewBag.EditMode = false;
            return View(b);
        }

        [HttpPost]
        public ActionResult Create(Book book)
        {
            BooksStoreDBContext db = new BooksStoreDBContext();
            ViewBag.EditMode = false;
            if (ModelState.IsValid)
            {
                BooksService service = new BooksService();
                HttpPostedFileBase file = Request.Files.Count > 0 ? Request.Files.Get(0) : null;
                string path = Server.MapPath("~/images/" + book.Title.Replace(' ', '_') + "_img.png");
                service.SaveBook(book, file, path);
                ViewBag.Message = "Książka zapisana pomyślnie";
                return View(new Book { ReleaseDate = DateTime.Now});
            }
            else
            {
                return View(book);
            }
        }

        public void Remove(int id)
        {
            BooksService service = new BooksService();
            service.RemoveBook(id);
        }

        public ActionResult Edit(int id)
        {
            BooksService service = new BooksService();
            Book b = service.FindById(id);
            ViewBag.EditMode = true;
            return View("Create",b);

        }

        [HttpPost]
        public ActionResult Edit(Book book)
        {
            BooksStoreDBContext db = new BooksStoreDBContext();
            ViewBag.EditMode = true;
            if (ModelState.IsValid)
            {
                BooksService service = new BooksService();
                //HttpPostedFileBase file = Request.Files.Count > 0 ? Request.Files.Get(0) : null;
                //string path = Server.MapPath("~/images/" + book.Title.Replace(' ', '_') + "_img.png");
                //service.SaveBook(book, file, path);
                service.SaveBook(book);
                ViewBag.Message = "Książka zapisana pomyślnie";
            }
            return View("Create",book);

        }

    }
}