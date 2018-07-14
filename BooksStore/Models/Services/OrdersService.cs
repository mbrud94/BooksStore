using BooksStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Models.Services
{
    public class OrdersService
    {
        Order order = new Order();

        public void CreateOrder(string userName, Order order)
        {
            BooksStoreDBContext db = new BooksStoreDBContext();
            order.Username = userName;
            order.OrderDate = DateTime.Now;
            db.Orders.Add(order);
            db.SaveChanges();
            CreateOrderFormCart(userName, order, db);
            
        }

        public List<Order> GetUserOrders(string userName)
        {
            BooksStoreDBContext db = new BooksStoreDBContext();
            return db.Orders.Where(o => o.Username == userName).ToList();
        }

        private void CreateOrderFormCart(string userName, Order order, BooksStoreDBContext db)
        {
            CartService service = new CartService();
            CartDetailsViewModel cartVM = service.GetCartItems(userName);
            order.Total = cartVM.TotalPrice;
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            foreach(CartItemViewModel item in cartVM.CartItems)
            {
                OrderDetail detail = ConvertCartItemToOrderDetails(item);
                detail.OrderId = order.OrderId;
                db.OrdersDetails.Add(detail);              
            }
            db.SaveChanges();
            service.ClearCart(userName, true);
        }

        private OrderDetail ConvertCartItemToOrderDetails(CartItemViewModel cartItem)
        {
            return new OrderDetail
            {
                BookId = cartItem.BookId,
                Count = cartItem.ItemsCount,
                UnitPrice = cartItem.ItemPrice            
            };
        }
    }
}
