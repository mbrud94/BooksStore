using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Models
{
    public class Book
    {
        [ScaffoldColumn(false)]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Kategoria jest wymagana")]
        [DisplayName("Kategoria")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Autor jest wymagany")]
        [DisplayName("Autor")]
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "Tytuł jest wymagany")]
        [DisplayName("Tytuł")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Cena jest wymagana")]
        [DisplayName("Cena")]
        [DefaultValue(10.0)]
        [DisplayFormat(DataFormatString = "{0:#.####}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        [DisplayName("Opis")]
        public string Description { get; set; }

        [DisplayName("Okładka")]
        public string ImageUrl { get; set; }

        [DisplayName("Liczba stron")]
        [DefaultValue(0)]
        public int PageCount { get; set; }

        [DisplayFormat(DataFormatString ="{DD/MM/YYYY}", ApplyFormatInEditMode = true)]
        [DisplayName("Data wydania")]
        public DateTime ReleaseDate { get; set; }

        [DefaultValue(10)]
        [DisplayName("Liczba książek")]
        public Nullable<int> Count { get; set; } = 10;

        [ScaffoldColumn(false)]
        public int VisitCounter { get; set; }

        public virtual Category Category { get; set; }

        public virtual Author Author { get; set; }

        
    }
}
