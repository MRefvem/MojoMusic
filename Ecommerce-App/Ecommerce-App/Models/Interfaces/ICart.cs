using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Interfaces
{
    public interface ICart
    {
        /// <summary>
        /// Create - Method creates a new cart.
        /// </summary>
        /// <param name="UserEmail">The email of the user.</param>
        /// <returns>A new cart was created for the user.</returns>
        Task<Cart> Create(string userEmail);

        /// <summary>
        /// Update - Method updates the cart status of the user
        /// </summary>
        /// <param name="cart">A complete cart obejct.</param>
        /// <returns>An updated cart object.</returns>
        Task<Cart> Update(Cart cart);

        /// <summary>
        /// GetCartForUserByEmail - Method gets a cart by using the user's email
        /// </summary>
        /// <param name="userEmail">The email of the user.</param>
        /// <returns>The cart of the user that was searched.</returns>
        Task<Cart> GetCartForUserByEmail(string userEmail);

        /// <summary>
        /// Delete - Method that deletes a cart from the database.
        /// </summary>
        /// <param name="cartId">The id of the cart to be deleted.</param>
        /// <returns>The task complete, the user's cart now deleted from the database.</returns>
        Task Delete(int cartId);
    }
}
