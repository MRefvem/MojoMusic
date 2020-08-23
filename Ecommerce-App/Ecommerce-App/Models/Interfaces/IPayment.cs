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
        TransactionResponse Run(creditCardType creditCard, customerAddressType billingAddress, Cart cart);
    }
}
