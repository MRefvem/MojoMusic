using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        [Column(TypeName = "decimal(7,2)")]
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        // Navigation props
        public List<CartItems> CartItems { get; set; }
    }
}
