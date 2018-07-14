using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Models.Services
{
     public class AuthorsService
    {
        private BooksStoreDBContext db = new BooksStoreDBContext();

        public int AddAuthor(string name)
        {
            Author a = db.Authors.FirstOrDefault(au => au.Name == name);
            if (a == null)
            {
                a = new Author();
                a.Name = name;
                db.Authors.Add(a);
                db.SaveChanges();
            }
            return a.AuthorId;
        }
    }
}
