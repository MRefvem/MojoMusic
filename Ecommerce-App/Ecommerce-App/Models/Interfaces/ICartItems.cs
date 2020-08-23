using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Interfaces
{
    public interface ICartItems
    {
        /// <summary>
        /// Creates a cart item
        /// </summary>
        /// <param name="cartItems"> the cart item object</param>
        /// <returns> the created cartitem object</returns>
        Task<CartItems> Create(CartItems cartItems);

        /// <summary>
        /// Gets a single cart item by the cartid and product id
        /// </summary>
        /// <param name="CartId"> the id of the userscart</param>
        /// <param name="ProductId"> the id of the product</param>
        /// <returns> the cart item object</returns>
        Task<CartItems> GetCartItem(int CartId, int ProductId);

        /// <summary>
        /// Gets all cart items by the cartid
        /// </summary>
        /// <param name="cartId">the users cart id</param>
        /// <returns>the cart item object</returns>
        Task<List<CartItems>> GetAllCartItems(int cartId);

        /// <summary>
        /// Updates a cart item
        /// </summary>
        /// <param name="cartItems"> the cart item object</param>
        /// <returns> the updated cart</returns>
        Task<CartItems> Update(CartItems cartItems);

        /// <summary>
        /// Deletes a cartitem
        /// </summary>
        /// <param name="id"> the id of the cart item</param>
        /// <returns> task completion</returns>
        Task Delete(int productId, int cartId);

    }
}
