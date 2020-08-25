using System;
using System.Collections.Generic;
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

        // Nav properties
        public Cart Cart { get; set; }

        // combine order address with this table

        // order Table -> id (pk), first name, lastname, address infor, city, state......
        // orderitems table -> orderId, productId

        // on checkout, look at your cartitems and "transfer' them to a new order and make new orderitems for each product in your cart

        // receipt can just look at your orderitems table and pull all the products from there 
    }
}
