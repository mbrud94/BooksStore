using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Models.ViewModels
{
    public class CartItemViewModel
    {
        public string ImgUrl { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public string BookAuthor { get; set; }
        public string BookCategory { get; set; }
        public int ItemsCount { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
