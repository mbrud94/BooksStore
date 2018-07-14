using BooksStore.Models.Services;
using BooksStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BooksStore.Controllers
{
    public class ChartsController : Controller
    {
        // GET: Charts
        public ActionResult Index()
        {
            return View("Charts");
        }

        public ActionResult ChartData()
        {
            StatsService service = new StatsService();
            ChartsViewModel chartVM = service.GetChartsData();
            return Json(chartVM, JsonRequestBehavior.AllowGet);
        }
    }
}