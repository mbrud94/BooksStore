using BooksStore.Models;
using BooksStore.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;

namespace BooksStore.Controllers
{
    public class OrdersController : Controller
    {
        // GET: Orders
        public ActionResult Index()
        {
            OrdersService service = new OrdersService();
            return PartialView("OrdersList", service.GetUserOrders(User.Identity.Name));
        }

        public ActionResult CreateOrder()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult CreateOrder(FormCollection values)
        {
            Order o = new Order();
            OrdersService service = new OrdersService();
            TryUpdateModel(o);
            service.CreateOrder(User.Identity.Name, o);
            return RedirectToAction("Complete", new { orderId = o.OrderId});
        }

        public ActionResult Complete(int orderId)
        {
            return View(orderId);
        }

        public ActionResult Details(int id)
        {
            BooksStoreDBContext db = new BooksStoreDBContext();

            return new ViewAsPdf(db.Orders.Include("OrderDetails").FirstOrDefault(o => o.OrderId == id));
        }
    }
}