using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Interfaces
{
    public interface ICart
    {
        /// <summary>
        /// Creates a new cart
        /// </summary>
        /// <param name="UserEmail">the email of the user </param>
        /// <returns> a cart object</returns>
        Task<Cart> Create(string UserEmail);

        /// <summary>
        /// Gets a cart by using the users email
        /// </summary>
        /// <param name="userEmail">the email of the user</param>
        /// <returns>  cart of the user  </returns>
        Task<Cart> GetCartForUserByEmail(string userEmail);
    }
}
