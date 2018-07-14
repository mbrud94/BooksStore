using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Models.Services
{
    public class CategoriesService
    {
        private BooksStoreDBContext db = new BooksStoreDBContext();
        
        public IEnumerable<Category> GetAllCategories()
        {
            return db.Categories.AsEnumerable();
        }

        public int AddCategory(string name)
        {
            Category c = db.Categories.FirstOrDefault(cat => cat.Name == name);
            if (c == null)
            {
                c = new Category();
                c.Name = name;
                db.Categories.Add(c);
                db.SaveChanges();
            }
            return c.CategoryId;
        }
    }
}
