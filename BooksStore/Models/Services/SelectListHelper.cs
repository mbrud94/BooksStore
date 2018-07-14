using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BooksStore.Models.Services
{
     public class SelectListHelper
    {
        BooksStoreDBContext db = new BooksStoreDBContext();
        public static SelectList GetAuthorList(int? selectedId)
        {
            BooksStoreDBContext db = new BooksStoreDBContext();
            if (selectedId.HasValue)
            {
                return new SelectList(db.Authors.OrderBy(a => a.Name), "AuthorId", "Name", selectedId.Value);
            }
            else
            {
                 return new SelectList(db.Authors.OrderBy(a=>a.Name), "AuthorId", "Name");
            }

        }

        public static SelectList GetcategoriesList(int? selectedId)
        {
            BooksStoreDBContext db = new BooksStoreDBContext();
            if (selectedId.HasValue)
            {
                return new SelectList(db.Categories.OrderBy(a => a.Name), "CategoryId", "Name", selectedId.Value);
            }
            else
            {
                return new SelectList(db.Categories.OrderBy(a => a.Name), "CategoryId", "Name");
            }

        }
    }
}
