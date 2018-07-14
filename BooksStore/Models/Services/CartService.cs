using BooksStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Models.Services
{
    public class CartService
    {
        public int GetItemsCount(string userName)
        {
            using (BooksStoreDBContext db = new BooksStoreDBContext())
            {
                if (db.CartItems.Any(c => c.UserName == userName))
                    return db.CartItems.Where(c => c.UserName == userName).Sum(c => c.Count);
                else return 0;
            }
        }

        public CartViewModel AddBookToCart(int bookId, string userName, int count = 1)
        {
            using (BooksStoreDBContext db = new BooksStoreDBContext())
            {
                CartItem item = db.CartItems.FirstOrDefault(c => c.BookId == bookId && c.UserName == userName);
                if (item == null)
                {
                    item = new CartItem
                    {
                        BookId = bookId,
                        UserName = userName,
                        DateCreated = DateTime.Now,
                        Count = count
                    };
                    db.CartItems.Add(item);
                }
                else
                {
                    item.Count += count;
                }
                db.SaveChanges();
            }
            CartViewModel cartVM = new CartViewModel();
            cartVM.BooksCount = DecrementBookCount(bookId, count);
            cartVM.ItemsCount = GetItemsCount(userName);
            return cartVM;
        }

        public CartDetailsViewModel GetCartItems(string userName)
        {
            BooksStoreDBContext db = new BooksStoreDBContext();
            List<CartItemViewModel> itemsList = new List<CartItemViewModel>();
            CartDetailsViewModel cartVM = new CartDetailsViewModel();
            foreach(CartItem item in db.CartItems.Where(c => c.UserName == userName))
            {
                itemsList.Add(ConvertEntityToVM(item));
            }
            cartVM.CartItems = itemsList;
            cartVM.TotalBooksCount = itemsList.Count;
            cartVM.TotalItemsCount = this.GetItemsCount(userName);
            cartVM.TotalPrice = CalculateTotalPrice(userName);
            return cartVM;
        }

        public void ClearCart(string userName, bool isOrder = false)
        {
            BooksStoreDBContext db = new BooksStoreDBContext();
            IEnumerable<CartItem> items = db.CartItems.Where(c => c.UserName == userName);
            if (!isOrder)
            {
                foreach (CartItem c in items)
                {
                    c.Book.Count += c.Count;
                    Hubs.HubAccessor.Instance.UpdateBookCounter(c.BookId, c.Book.Count.Value);
                }
            }
            db.CartItems.RemoveRange(items);
            db.SaveChanges();
        }

        public CartDetailsViewModel RemoveBookFromCart(string userName, int bookId)
        {
            BooksStoreDBContext db = new BooksStoreDBContext();
            CartDetailsViewModel cartVM = new CartDetailsViewModel();
            cartVM.CartItems = new List<CartItemViewModel>();
            CartItem item = db.CartItems.FirstOrDefault(c => c.UserName == userName && c.BookId == bookId);
            if(item != null)
            {
                item.Count--;
                item.Book.Count++;
                cartVM.CartItems.Add(ConvertEntityToVM(item));
                Hubs.HubAccessor.Instance.UpdateBookCounter(bookId, item.Book.Count.Value);
                if (item.Count == 0)
                {
                    db.CartItems.Remove(item);
                }
                db.SaveChanges();
                
                
            }
            cartVM.TotalBooksCount = db.CartItems.Count(c => c.UserName == userName);
            cartVM.TotalItemsCount = this.GetItemsCount(userName);
            cartVM.TotalPrice = CalculateTotalPrice(userName);
            return cartVM;
        }
        private int DecrementBookCount(int bookId, int value = 1)
        {
            int res = 0;
            using (BooksStoreDBContext db = new BooksStoreDBContext())
            {
                Book book = db.Books.FirstOrDefault(b => b.BookId == bookId);
                book.Count -= value;
                res = book.Count.Value;
                db.SaveChanges();
            }
            return res;
        }

        private CartItemViewModel ConvertEntityToVM(CartItem item)
        {
            CartItemViewModel itemVM = new CartItemViewModel
            {
                BookId = item.BookId,
                BookAuthor = item.Book.Author.Name,
                BookCategory = item.Book.Category.Name,
                BookTitle = item.Book.Title,
                ImgUrl = item.Book.ImageUrl,
                ItemPrice = item.Book.Price,
                ItemsCount = item.Count,
                TotalPrice = item.Count * item.Book.Price
            };
            return itemVM;
        }

        private decimal CalculateTotalPrice(string userName)
        {
            using (BooksStoreDBContext db = new BooksStoreDBContext())
            {
                if (db.CartItems.Any(c => c.UserName == userName))
                    return db.CartItems.Where(c => c.UserName == userName).Sum(c => c.Book.Price * c.Count);
                else return 0;
            }
        }
    }
}
