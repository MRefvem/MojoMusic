using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Interfaces
{
   public  interface ICartItems
    {
        Task<CartItems> Create(CartItems cartItems, int CartId);
    }
}
