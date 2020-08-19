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

        public string Run()
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

            var creditCard = new creditCardType
            {
                cardNumber = "5424000000000015",
                expirationDate = "1020",
                cardCode = "555"
            };

            customerAddressType billingAddress = GetBillingAddress(3);

            var paymentType = new paymentType { Item = creditCard };

            // make the transaction request

            var transRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureContinueTransaction.ToString(),
                amount = 150.00m,
                payment = paymentType,
                billTo = billingAddress,
            };

            var request = new createTransactionRequest { transactionRequest = transRequest };

            var controller = new createTransactionController(request);
            controller.Execute();

            var response = controller.GetApiResponse();

            return "";
        }

        private customerAddressType GetBillingAddress(int orderId)
        {
            customerAddressType address = new customerAddressType
            {
                firstName = "Josie",
                lastName = "Kitty",
                address = "123 Cat Nip Lane",
                city = "Seattle",
                zip = "98004"
            };

            return address;
        }
    }
}
