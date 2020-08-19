using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Interfaces
{
    public interface ICartItems
    {
        Task<CartItems> Create(CartItems cartItems);

        Task<CartItems> GetCartItem(int CartId, int ProductId);
        Task<List<CartItems>> GetAllCartItems(int CartId);
        Task<CartItems> Update(CartItems cartItems);
        Task Delete(int id);

    }
}
