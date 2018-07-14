using BooksStore.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BooksStore.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: Categories
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllCategories()
        {
            CategoriesService service = new CategoriesService();
            return PartialView(service.GetAllCategories());
        }

        public ActionResult CategoriesSelectList(int? selectedId)
        {
            ViewBag.CategoryId = SelectListHelper.GetcategoriesList(selectedId);
            return PartialView();
        }

        public ActionResult AddCategory(string name)
        {
            CategoriesService service = new CategoriesService();
            int id = service.AddCategory(name);
            return RedirectToAction("CategoriesSelectList", new { selectedId = id });
        }
    }
}