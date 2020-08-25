using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Interfaces
{
    public interface IOrderAddress
    {
        /// <summary>
        /// Create - Method used to create the user's order address. User inputs their billing/shipping address into the form and that information is passed through the Checkout PageModel and through to the Order Service and then on to here.
        /// </summary>
        /// <param name="orderAddress">The input fields passed through from the user</param>
        /// <returns>The User's Order Address stored in the database</returns>
        Task<OrderAddress> Create(OrderAddress orderAddress);
    }
}
