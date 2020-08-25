using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Interfaces
{
    public interface IOrder
    {
        /// <summary>
        /// Create - Method that creates a new order.
        /// </summary>
        /// <param name="cart">The cart details that need to be processed with the order.</param>
        /// <param name="orderAddress">The address that the order will send the ordered products to.</param>
        /// <returns>The completed order, user's products are enroute!</returns>
        Task<Order> Create(Order order);

        /// <summary>
        /// GetMostRecent - Method that gets the user's most recent order for the purpose of rendering the order details to the post-purchase summary page.
        /// </summary>
        /// <param name="cartId">The Id of the newly-inactive cart.</param>
        /// <returns>The order detail of the user's most recent purchase.</returns>
        Task<Order> GetMostRecent(string userEmail);
    }
}
