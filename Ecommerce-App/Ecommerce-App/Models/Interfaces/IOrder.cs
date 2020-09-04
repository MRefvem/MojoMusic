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


        /// <summary>
        /// GetAllOrders - Method that gets all the user's order 
        /// </summary>
        /// <param name="userEmail">The email (username) of the logged in user.</param>
        /// <returns> A list of all of the user's orders</returns>
        Task<List<Order>> GetAllOrders(string userEmail);

        /// <summary>
        /// GetCompleteCompanyOrderHistory - Method that gets all of the orders ever completed on the site
        /// </summary>
        /// <returns>A list of every order ever placed</returns>
        Task<List<Order>> GetCompleteCompanyOrderHistory();

        /// <summary>
        /// GetTotal - Tallies up the total cost of an order
        /// </summary>
        /// <param name="order">The order object to tally up total price for</param>
        /// <returns>The task complete</returns>
        Order GetTotal(Order order);
    }
}
