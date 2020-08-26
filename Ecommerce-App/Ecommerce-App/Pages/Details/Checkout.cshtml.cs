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
        private readonly IEmailSender _emailSender;
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

        public CheckoutModel(ICart cart, IPayment payment, IOrder order, IConfiguration configuration, SignInManager<Customer> signInManager, IEmailSender emailSender, ICartItems cartItems)
        {
            _cart = cart;
            _payment = payment;
            _order = order;
            _config = configuration;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _cartItems = cartItems;
            CurrentCartId = 0;
        }

        /// <summary>
        /// OnGet - This method performs all of the logic necessary to query the databases and then have information available to display to the page upon loading. In this case that includes the items present in the user's shopping cart near the time of purchase as well as seeded credit card details to be used for Sandbox most test purchases (no real credit cards can be used to make purchases on this site).
        /// </summary>
        /// <returns>The completed task, all necessary information is presented to the page upon page load.</returns>
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

        /// <summary>
        /// OnPost - Upon posting to this page (user checkout) a number of actions take place. This method constructs all of the pieces necessary for the most important operation: the purchase. This method assembles all of the final details needed to make the purchase including the user's shopping cart, to total price of all of the items in the cart, the billing/order address, and the credit card being used. Then finally a call is made to the payment service, the card gets ran, and the order is placed. The user inputs their billing information into a form, selects from one of the three seeded credit cards and then makes their purchase.
        /// </summary>
        /// <param name="firstName">Billing First Name</param>
        /// <param name="lastName">Billing Last Name</param>
        /// <param name="address">Billing Address</param>
        /// <param name="city">Billing City</param>
        /// <param name="state">Billing State</param>
        /// <param name="zip">Billing ZIP</param>
        /// <param name="cardNumber">Credit Card</param>
        /// <returns>The task complete, a successful (or unsuccessful) transaction has taken place.</returns>
        public async Task<IActionResult> OnPost(string firstName, string lastName, string address, string city, string state, int zip)
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
                // GET CART ITEMS HERE
                List<CartItems> cartItems = await _cartItems.GetAllCartItems(CurrentCartId);

                Order newOrder = new Order()
                {
                    CartId = cart.Id,
                    UserEmail = user.Email,
                    FirstName = firstName,
                    LastName = lastName,
                    Address = address,
                    City = city,
                    State = state,
                    Zip = zip
                };

                Order order =  await _order.Create(newOrder);

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

        /// <summary>
        /// GetUserEmail - This method performs the logic neceesary for retrieving a user's email addressed based on the claims assigned to that user upon their registration to the site.
        /// </summary>
        /// <returns>A string containing the user's email address.</returns>
        protected string GetUserEmail()
        {
            return User.Claims.First(x => x.Type == "Email").Value;
        }
    }
}