using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int BookId { get; set; }
        public string UserName { get; set; }
        public int Count { get; set; }
        public System.DateTime DateCreated { get; set; }

        public virtual Book Book { get; set; }

    }
}
