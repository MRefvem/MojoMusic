using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthorizeNet.Api.Contracts.V1;
using Ecommerce_App.Models;
using Ecommerce_App.Models.Interfaces;
using Ecommerce_App.Models.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Ecommerce_App.Pages.Details
{
    public class CheckoutModel : PageModel
    {

        private readonly ICart _cart;
        private readonly IPayment _payment;
        private readonly IOrder _order;
        private readonly IConfiguration _config;
        private readonly SignInManager<Customer> _signInManager;
        private readonly ICartItems _cartItems;
        private readonly IEmailSender _emailSender;

        [BindProperty]
        public int CurrentCartId { get; set; }
        [BindProperty]
        public string FirstName { get; set; }
        [BindProperty]
        public string LastName { get; set; }
        [BindProperty]
        public string Address { get; set; }
        [BindProperty]
        public string City { get; set; }
        [BindProperty]
        public string State { get; set; }
        [BindProperty]
        public string Zip { get; set; }
        [BindProperty]
        public string CardType { get; set; }
        public decimal Total { get; set; }
        public Cart CurrentUserCart { get; set; }

        public CheckoutModel(ICart cart, IPayment payment, IOrder order, IConfiguration configuration, SignInManager<Customer> signInManager, ICartItems cartItems, IEmailSender emailSender)
        {
            _cart = cart;
            _payment = payment;
            _order = order;
            _config = configuration;
            _signInManager = signInManager;
            _cartItems = cartItems;
            _emailSender = emailSender;
            CurrentCartId = 0;
        }

        public async Task<IActionResult> OnGet()
        {
            Cart cart = await _cart.GetCartForUserByEmail(GetUserEmail());

            // logic here
            if (cart == null)
            {
                await _cart.Create(GetUserEmail());
                return RedirectToPagePermanent("Index", "Products");
            }

            decimal totalPrice = 0;

            foreach (var item in cart.CartItems)
            {
                totalPrice += item.Product.Price * item.Quantity;
            }

            Total = totalPrice;

            CurrentCartId = cart.Id;
            CurrentUserCart = cart;

            var VISA = _config["VISATestNumber"];
            var Mastercard = _config["MastercardTestNumber"];
            var Discover = _config["DiscoverTestNumber"];

            return Page();
        }

        public async Task<IActionResult> OnPost(string firstName, string lastName, string address, string city, string state, int zip, string cardNumber)
        {
            Cart cart = await _cart.GetCartForUserByEmail(GetUserEmail());

            if (cart == null)
            {
                await _cart.Create(GetUserEmail());
            }

            decimal totalPrice = 0;

            foreach (var item in cart.CartItems)
            {
                totalPrice += item.Product.Price * item.Quantity;
            }

            Total = totalPrice;

            OrderAddress orderAddress = new OrderAddress()
            {
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                City = city,
                State = state,
                Zip = zip
            };

            var user = await _signInManager.UserManager.GetUserAsync(User);

            customerAddressType billingAddress = new customerAddressType()
            {
                firstName = firstName,
                lastName = lastName,
                email = user.UserName,
                address = address,
                city = city,
                state = state,
                zip = zip.ToString()
            };

            creditCardType creditCard = new creditCardType()
            {
                cardNumber = _config[$"{CardType}TestNumber"],
                expirationDate = _config["ExpDate"],
                cardCode = _config["CVV"]
            };

            CurrentCartId = cart.Id;
            CurrentUserCart = cart;

            var paymentResult = _payment.Run(creditCard, billingAddress, cart);

            if (paymentResult.Successful)
            {
               Order order =  await _order.Create(cart, orderAddress);

                StringBuilder sb = new StringBuilder();

                foreach (var item in cart.CartItems)
                {
                    sb.Append($"<li>{item.Product.Name} (Quantity: {item.Quantity})</li>");
                };

                string subject = "Your Order Summary";
                string htmlMessage = $"<h1>Thank you for your purchase {user.FirstName}.</h1><p>Your summary: {sb.ToString()}</p><p>Total purchase cost: ${totalPrice}</p><p>Your order is now being processed and should be heading your way soon. We hope you're happy with your purchase!</p>";
                await _emailSender.SendEmailAsync(user.UserName, subject, htmlMessage);

                cart.IsActive = false;

                await _cart.Update(cart);

                await _cart.Create(GetUserEmail());

                return RedirectToPage("Receipt");
            }

            return Page();
        }

        protected string GetUserEmail()
        {
            return User.Claims.First(x => x.Type == "Email").Value;
        }
    }
}