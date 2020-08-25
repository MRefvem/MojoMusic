using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Controllers.Bases;
using Ecommerce_App.Models.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Ecommerce_App.Models.Services
{
    public class PaymentService : IPayment
    {
        private readonly IConfiguration _config;

        public PaymentService(IConfiguration configuration)
        {
            _config = configuration;
        }

        /// <summary>
        /// Run - Method that is used to process a user's payment information.
        /// </summary>
        /// <param name="creditCard">The user's credit card used for the purchase.</param>
        /// <param name="billingAddress">The user's billing address.</param>
        /// <param name="cart">The cart the user is attempting to process.</param>
        /// <returns>Transaction response, detailing whether the purchase was successful or denied.</returns>
        public TransactionResponse Run(creditCardType creditCard, customerAddressType billingAddress, Cart cart)
        {
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = _config["AuthorizeLoginId"],
                ItemElementName = ItemChoiceType.transactionKey,
                Item = _config["AuthorizeTransactionKey"]
            };

            var paymentType = new paymentType { Item = creditCard };

            decimal total = 0;
            lineItemType[] lineItems = new lineItemType[cart.CartItems.Count];

            for (int i = 0; i < lineItems.Length; i++)
            {
                var cartItem = cart.CartItems[i];
                total += cartItem.Product.Price * cartItem.Quantity;
                lineItems[i] = new lineItemType
                {
                    itemId = cartItem.ProductId.ToString(),
                    name = cartItem.Product.Name,
                    quantity = cartItem.Quantity,
                    unitPrice = cartItem.Product.Price
                };
            }

            var transRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),
                amount = total,
                payment = paymentType,
                billTo = billingAddress,
                lineItems = lineItems
            };

            var request = new createTransactionRequest { transactionRequest = transRequest };

            var controller = new createTransactionController(request);
            controller.Execute();

            var response = controller.GetApiResponse();

            if (response != null)
            {
                if (response.transactionResponse != null)
                {
                    return new TransactionResponse
                    {
                        Successful = true,
                        Response = $"Transaction Success: {response.transactionResponse.authCode}"
                    };
                }
                else if (response.transactionResponse == null)
                {
                    return new TransactionResponse
                    {
                        Successful = false,
                        Response = $"Transaction Error: {response.transactionResponse.errors[0].errorCode} {response.transactionResponse.errors[0].errorText}"
                    };
                }
            }

            return new TransactionResponse
            {
                Successful = false,
                Response = ""
            };
        }

        public class TransactionResponse
        {
            public bool Successful;
            public string Response;
        }
    }
}
