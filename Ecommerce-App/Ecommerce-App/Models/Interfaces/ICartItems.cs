using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Interfaces
{
    public interface ICartItems
    {
        /// <summary>
        /// Create - Method creates a cart item.
        /// </summary>
        /// <param name="cartItems">The cart item object to be created.</param>
        /// <returns>The created cart item object.</returns>
        Task<CartItems> Create(CartItems cartItems);

        /// <summary>
        /// GetCartItem - Method gets a single cart item by the cartid and product id.
        /// </summary>
        /// <param name="CartId">The id of the user's cart.</param>
        /// <param name="ProductId">The id of the product.</param>
        /// <returns>The cart item object.</returns>
        Task<CartItems> GetCartItem(int CartId, int ProductId);

        /// <summary>
        /// GetAllCartItems - Method gets all cart items associated with a specific user's cart.
        /// </summary>
        /// <param name="cartId">The user's cart id.</param>
        /// <returns>A list of all items in a user's cart.</returns>
        Task<List<CartItems>> GetAllCartItems(int cartId);

        /// <summary>
        /// Update - Method updates the contents of a user's cart.
        /// </summary>
        /// <param name="cartItems">The cart item to be updated.</param>
        /// <returns>The updated item.</returns>
        Task<CartItems> Update(CartItems cartItems);

        /// <summary>
        /// Delete - Method deletes a cart item. It finds the exact product by first searching for the Id of that product and the associated Id of the user's cart.
        /// </summary>
        /// <param name="productId">The id of the product to be deleted.</param>
        /// <param name="cartId">The id of the user's cart.</param>
        /// <returns>The completed task: that item has now been deleted from the user's cart.</returns>
        Task Delete(int productId, int cartId);

    }
}
