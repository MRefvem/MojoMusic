using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public string UserEmail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        public decimal Total { get; set; }
        public DateTime Date { get; set; }
       // dateTime.now
        // Nav properties
        public Cart Cart { get; set; }
    }
}
