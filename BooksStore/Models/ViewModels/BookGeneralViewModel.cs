using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Models.ViewModels
{
    public class BookGeneralViewModel
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public Category Category { get; set; }
        public Author Author { get; set; }
        public string CategoryColor { get; set; }
        public int Count { get; set; }
    }
}
