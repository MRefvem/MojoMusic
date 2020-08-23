using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorizeNet.Api.Contracts.V1;
using Ecommerce_App.Models;
using Ecommerce_App.Models.Interfaces;
using Ecommerce_App.Models.Services;
using Microsoft.AspNetCore.Identity;
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

        public CheckoutModel(ICart cart, IPayment payment, IOrder order, IConfiguration configuration, SignInManager<Customer> signInManager, ICartItems cartItems)
        {
            _cart = cart;
            _payment = payment;
            _order = order;
            _config = configuration;
            _signInManager = signInManager;
            _cartItems = cartItems;
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

            if (paymentResult.Successful == true)
            {
                await _order.Create(cart, orderAddress);

                // TODO: Need a way to clear a user's shopping cart (create a new one?)
                // Deleting a user's cart or it's contents from the database means we won't be able to reference them later
                //await _cart.Create(GetUserEmail());

                return Page();
            }

            return Page();
        }

        protected string GetUserEmail()
        {
            return User.Claims.First(x => x.Type == "Email").Value;
        }
    }
}