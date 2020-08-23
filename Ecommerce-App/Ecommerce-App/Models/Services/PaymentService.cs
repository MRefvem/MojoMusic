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
        private IConfiguration _config;

        public PaymentService(IConfiguration configuration)
        {
            _config = configuration;
        }

        public TransactionResponse Run(creditCardType creditCard, customerAddressType billingAddress, Cart cart)
        {
            // Important, bring in a full object
            // Can return something other than a string

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = _config["AuthorizeLoginId"],
                ItemElementName = ItemChoiceType.transactionKey,
                Item = _config["AuthorizeTransactionKey"]
            };

            // create the cart we want on file
            // store these in the secrets file, make a dropdown for the user in the checkout process

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

            // make the transaction request

            var transRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureContinueTransaction.ToString(),
                amount = total,
                payment = paymentType,
                billTo = billingAddress,
                lineItems = lineItems
            };

            var request = new createTransactionRequest { transactionRequest = transRequest };

            var controller = new createTransactionController(request);
            controller.Execute();

            var response = controller.GetApiResponse();

            if (response.transactionResponse != null)
            {
                if (response.transactionResponse != null)
                {
                    return new TransactionResponse
                    {
                        Successful = true,
                        Response = $"Transaction Success: {response.transactionResponse.authCode}"
                    };
                }
            }
            else if (response.transactionResponse != null)
            {
                return new TransactionResponse
                {
                    Successful = false,
                    Response = $"Transaction Error: {response.transactionResponse.errors[0].errorCode} {response.transactionResponse.errors[0].errorText}"
                };
            }
            return new TransactionResponse
            {
                Successful = false,
                Response = $"Error: {response.messages.message[0].code} {response.messages.message[0].text}"
            };
        }

        public class TransactionResponse
        {
            public bool Successful;
            public string Response;
        }
    }
}
