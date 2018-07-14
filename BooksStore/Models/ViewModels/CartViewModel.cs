using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Models.ViewModels
{
    public class CartViewModel
    {
        public int ItemsCount { get; set; }
        public int BooksCount { get; set; }
        public string ErrorMessage { get; set; }
    }
}
