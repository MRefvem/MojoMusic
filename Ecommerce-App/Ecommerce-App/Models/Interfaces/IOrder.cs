using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Interfaces
{
    public interface IOrder
    {
        /// <summary>
        /// Create - Method that creates a new order
        /// </summary>
        /// <param name="cart">The cart details that need to be processed with the order</param>
        /// <param name="orderAddress">The address that the order will send the ordered products to</param>
        /// <returns>The completed order, user's products are enroute!</returns>
        Task<Order> Create(Cart cart, OrderAddress orderAddress);

        Task<Order> GetMostRecent(int cartId);
    }
}
