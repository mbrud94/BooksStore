using BooksStore.Models.Services;
using BooksStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BooksStore.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            CartService service = new CartService();
            return View(service.GetCartItems(User.Identity.Name));
        }

        public ActionResult CartCount(string userName)
        {
            CartService service = new CartService();
            CartViewModel cartVM = new CartViewModel();
            cartVM.ItemsCount = service.GetItemsCount(userName);
            return PartialView(cartVM);
            //return View();
        }

        public int AddBookToCart(int id)
        {
            CartService service = new CartService();
            service.AddBookToCart(id, User.Identity.Name);
            return service.GetItemsCount(User.Identity.Name);
        }

        [HttpGet]
        public ActionResult AddBooksToCart(int id, int count)
        {
            CartService service = new CartService();
            CartViewModel cartVM = new CartViewModel();
            if (User.Identity.IsAuthenticated)
            {
                cartVM = service.AddBookToCart(id, User.Identity.Name, count);
                Hubs.HubAccessor.Instance.UpdateBookCounter(id, cartVM.BooksCount);
            }
            else
            {
                cartVM.ErrorMessage = "Aby kontynuować musisz się zalogować.";
            }
            
            return Json(cartVM, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public void ClearCart()
        {
            CartService service = new CartService();
            service.ClearCart(User.Identity.Name);         
        }

        [HttpGet]
        public ActionResult RemoveBookFromCart(int bookId)
        {
            CartService service = new CartService();
            CartDetailsViewModel cartVM = service.RemoveBookFromCart(User.Identity.Name, bookId);
            return Json(cartVM, JsonRequestBehavior.AllowGet);
        }
    }
}