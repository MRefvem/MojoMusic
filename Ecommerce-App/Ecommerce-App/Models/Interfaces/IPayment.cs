using AuthorizeNet.Api.Contracts.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Ecommerce_App.Models.Services.PaymentService;

namespace Ecommerce_App.Models.Interfaces
{
    public interface IPayment
    {
        /// <summary>
        /// Run - Method that is used to process a user's payment information.
        /// </summary>
        /// <param name="creditCard">The user's credit card used for the purchase.</param>
        /// <param name="billingAddress">The user's billing address.</param>
        /// <param name="cart">The cart the user is attempting to process.</param>
        /// <returns>Transaction response, detailing whether the purchase was successful or denied.</returns>
        TransactionResponse Run(creditCardType creditCard, customerAddressType billingAddress, Cart cart);
    }
}
