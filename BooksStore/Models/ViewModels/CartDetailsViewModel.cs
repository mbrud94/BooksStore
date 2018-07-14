using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Models.ViewModels
{
    public class CartDetailsViewModel
    {
        public int TotalItemsCount { get; set; }
        public int TotalBooksCount { get; set; }
        public decimal TotalPrice { get; set; }
        public List<CartItemViewModel> CartItems { get; set; }
    }
}
