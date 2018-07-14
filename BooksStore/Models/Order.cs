using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.Models
{
    public class Order
    {
        [ScaffoldColumn(false)]
        public int OrderId { get; set; }

        [ScaffoldColumn(false)]
        public System.DateTime OrderDate { get; set; }

        [ScaffoldColumn(false)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Imię jest wymagane")]
        [DisplayName("Imię")]
        [StringLength(160)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        [DisplayName("Nazwisko")]
        [StringLength(160)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Adres jest wymagany")]
        [DisplayName("Adres")]
        [StringLength(70)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Miasto jest wymagane")]
        [StringLength(40)]
        [DisplayName("Miasto")]
        public string City { get; set; }


        [Required(ErrorMessage = "Kod podcztowy jest wymagany")]
        [DisplayName("Kod pocztowy")]
        [StringLength(10)]
        public string PostalCode { get; set; }


        [ScaffoldColumn(false)]
        public decimal Total { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
