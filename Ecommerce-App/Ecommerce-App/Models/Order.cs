using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models
{
    public class Order
    {
        public int OrderAddressId { get; set; }
        public int CartId { get; set; }

        // Nav properties
        public OrderAddress OrderAddress { get; set; }
        public Cart Cart { get; set; }
    }
}
