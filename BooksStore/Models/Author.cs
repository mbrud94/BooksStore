using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }

        public virtual List<Book> Books { get; set; }
    }
}
