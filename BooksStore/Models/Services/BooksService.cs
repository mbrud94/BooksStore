using BooksStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BooksStore.Models.Services
{
    public class BooksService
    {
        private BooksStoreDBContext db = new BooksStoreDBContext();

        public IList<BookGeneralViewModel> GetAllBooks(string sortOrder)
        {
            IList<BookGeneralViewModel> booksList = new List<BookGeneralViewModel>();
            var books = this.SortBooks(db.Books, sortOrder);
            foreach (Book b in books)
            {
                booksList.Add(ConvertEntityToGeneralVM(b));
            }
            return booksList;
        }

        public IList<BookGeneralViewModel> GetBooksByAuthor(int id, string sortOrder)
        {
            IList<BookGeneralViewModel> booksList = new List<BookGeneralViewModel>();
            var books = this.SortBooks(db.Books.Where(b => b.AuthorId == id), sortOrder);
            foreach (Book b in books)
            {
                booksList.Add(ConvertEntityToGeneralVM(b));
            }
            return booksList;
        }

        public IList<BookGeneralViewModel> GetBooksByCategory(int id, string sortOrder)
        {
            IList<BookGeneralViewModel> booksList = new List<BookGeneralViewModel>();
            var books = this.SortBooks(db.Books.Where(b => b.CategoryId == id), sortOrder);
            foreach (Book b in books)
            {
                booksList.Add(ConvertEntityToGeneralVM(b));
            }
            return booksList;
        }

        public List<BookGeneralViewModel> SearchBooks(string searchString, string sortOrder)
        {
            List<BookGeneralViewModel> booksList = new List<BookGeneralViewModel>();
            var books = this.SortBooks(db.Books.Where(b => b.Title.ToUpper().Contains(searchString.ToUpper())), sortOrder);
            foreach (Book b in books)
            {
                booksList.Add(ConvertEntityToGeneralVM(b));
            }
            return booksList;
        }
        public Book FindById(int id)
        {
            Book book = db.Books.FirstOrDefault(b => b.BookId == id);
            book.VisitCounter++;
            db.SaveChanges();
            return book;
        }

        public void SaveBook(Book book, HttpPostedFileBase file, string path)
        {
            if (!String.IsNullOrEmpty(file.FileName))
            {
                file.SaveAs(path);
                book.ImageUrl = "images/" + book.Title.Replace(' ', '_') + "_img.png";
            }
            else
            {
                book.ImageUrl = "images/book.png";
            }
            if (book.Description == null)
                book.Description = String.Empty;
            db.Books.Add(book);
            db.SaveChanges();
        }

        public void SaveBook(Book book)
        {
            if (book.Description == null)
                book.Description = String.Empty;
            db.Entry(book).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void RemoveBook(int id)
        {
            Book book = db.Books.FirstOrDefault(b => b.BookId == id);
            if (book != null)
            {
                db.Books.Remove(book);
                db.SaveChanges();
            }
        }

        public List<BookGeneralViewModel> GetTopBooks()
        {
            List<BookGeneralViewModel> res = new List<BookGeneralViewModel>();
            res.AddRange(GetBestsellers());
            res.AddRange(GetNewestBooks());
            res.AddRange(GetTopVisitedBooks());
            return res;
        }
        private List<BookGeneralViewModel> GetBestsellers()
        {
            var items = db.OrdersDetails.GroupBy(od => od.BookId)
                .Select(odg => new { Count = odg.Sum(od => od.Count), Book = odg.FirstOrDefault().Book })
                .OrderByDescending(i => i.Count).Take(5);
            List<BookGeneralViewModel> res = new List<BookGeneralViewModel>();
            foreach(var item in items)
            {
                //Book b = FindById(item.Id);
                res.Add(this.ConvertEntityToGeneralVM(item.Book));             
            }
            return res;
        }

        private List<BookGeneralViewModel> GetNewestBooks()
        {
            List<BookGeneralViewModel> res = new List<BookGeneralViewModel>();
            foreach (Book b in db.Books.OrderByDescending(b => b.ReleaseDate).Take(5))
            {
                //Book b = FindById(item.Id);
                res.Add(this.ConvertEntityToGeneralVM(b));
            }
            return res;
        }

        private List<BookGeneralViewModel> GetTopVisitedBooks()
        {
            List<BookGeneralViewModel> res = new List<BookGeneralViewModel>();
            foreach (Book b in db.Books.OrderByDescending(b => b.VisitCounter).Take(5))
            {
                //Book b = FindById(item.Id);
                res.Add(this.ConvertEntityToGeneralVM(b));
            }
            return res;
        }

        private IEnumerable<Book> SortBooks(IEnumerable<Book> books, string sortOrder)
        {
            IEnumerable<Book> res = null;
            switch(sortOrder)
            {
                case "TITLE_DESC":
                    res = books.OrderByDescending(b => b.Title);
                    break;
                case "PRICE_DESC":
                    res = books.OrderByDescending(b => b.Price);
                    break;
                case "PRICE":
                    res = books.OrderBy(b => b.Price);
                    break;
                case "LENGTH_DESC":
                    res = books.OrderByDescending(b => b.PageCount);
                    break;
                case "LENGTH":
                    res = books.OrderBy(b => b.PageCount);
                    break;
                case "RELEASEDATE_DESC":
                    res = books.OrderByDescending(b => b.ReleaseDate);
                    break;
                case "RELEASEDATE":
                    res = books.OrderBy(b => b.ReleaseDate);
                    break;
                case "TITLE":
                default:
                    res = books.OrderBy(b => b.Title);
                    break;
            }
            return res;

        }
        private BookGeneralViewModel ConvertEntityToGeneralVM(Book book)
        {
            BookGeneralViewModel bookVM = new BookGeneralViewModel();
            bookVM.Author = book.Author;
            bookVM.BookId = book.BookId;
            bookVM.Category = book.Category;
            bookVM.Category.Name = book.Category.Name.First().ToString().ToUpper() + book.Category.Name.Substring(1);
            bookVM.Description = GetShortBookDescription(book);
            bookVM.ImageUrl = book.ImageUrl;
            bookVM.Price = book.Price;
            bookVM.Title = FormatTitle(book);
            bookVM.CategoryColor = GetCategoryColor(book);
            bookVM.Count = book.Count.HasValue ? book.Count.Value : 0;
            return bookVM;
        }

        private string GetShortBookDescription(Book b)
        {
            string res = string.Empty;
            string [] descWords = b.Description.Split(' ');
            int descSize = 50;
            int maxCount = descWords.Length < descSize ? descWords.Length : descSize;
            for (int i = 0; i<maxCount; i++)
            {
                res += descWords[i];
                if(i!= maxCount-1)
                    res += " ";
            }
            res += "...";
            return res;
        }

        private string FormatTitle(Book b)
        {
            char[] specialChars = { '(', '.' };
            foreach(char c in specialChars)
            {
                if (b.Title.IndexOf(c) != -1)
                    return b.Title.Substring(0, b.Title.IndexOf(c));
            }
            return b.Title;
        }

        private string GetCategoryColor(Book b)
        {
            string res = string.Empty;
            switch(b.Category.CategoryId)
            {
                case 2: res = "slateblue";
                    break;
                case 3:
                    res = "brown";
                    break;
                case 4:
                    res = "darkgreen";
                    break;
                case 5:
                    res = "red";
                    break;
                case 6:
                    res = "darkgoldenrod";
                    break;
                case 7:
                    res = "darkviolet";
                    break;
                case 8:
                    res = "peru";
                    break;
            }
            return res;
        }
    }
}
