using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_App.Models;
using Ecommerce_App.Models.Interfaces;
using Ecommerce_App.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce_App.Pages.Details
{
    public class ProductModel : PageModel
    {
        private readonly IProduct _product;
        private readonly ICartItems _cartItems;
        private readonly ICart _cart;
        private readonly SignInManager<Customer> _signInManager;

        [BindProperty]
        public Product Product { get; set; }
        [BindProperty]
        public int CurrentCartId { get; set; }

        public ProductModel(IProduct product, ICartItems cartItems, ICart cart, SignInManager<Customer> signInManager)
        {
            _product = product;
            _cartItems = cartItems;
            _cart = cart;
            _signInManager = signInManager;
            CurrentCartId = 0;
        }

        /// <summary>
        /// OnGet - This page displays information about the contents of a user's shopping cart upon page load.
        /// </summary>
        /// <param name="Id">The inputted Id of the cart associated with the specified user.</param>
        /// <returns>The completed task, the user's shopping cart contents displayed along with products info.</returns>
        public async Task<IActionResult> OnGet(int Id)
        {
            Product = await _product.GetProduct(Id);
            var user = await _signInManager.UserManager.GetUserAsync(User);

            // TODO: Handle exception for if there is no user logged in
            if (user != null)
            {
                Cart cart = await _cart.GetCartForUserByEmail(GetUserEmail());

                // logic here
                if (cart == null)
                {
                    cart = await _cart.Create(GetUserEmail());
                }

                var items = await _cartItems.GetAllCartItems(cart.Id);

                CurrentCartId = cart.Id;
            }

            return Page();
        }

        /// <summary>
        /// OnPost - This method performs the logic necessary for when the user posts to this route. Here the responsibility of this method is to be able to display s single queried product available in the products database, but also to query the database to det all of the relevant cart details for the associated user (a shopping cart is instantiated for the user if for some reason they get to this page and do not have a shopping cart).
        /// </summary>
        /// <param name="Id">The Id of the product selected by the user.</param>
        /// <returns>The task complete, the selected product displayed to the user.</returns>
        public async Task<IActionResult> OnPost(int Id)
        {
            var prod = await _product.GetProduct(Id);
            var user = await _signInManager.UserManager.GetUserAsync(User);

            // TODO: Handle exception for if there is no user logged in
            if (user == null)
            {
                return RedirectToPage("/Account/Login");
            }

            Cart cart = await _cart.GetCartForUserByEmail(GetUserEmail());
            
            if (cart == null)
            {
                // gives user a cart if they dont have one
                await _cart.Create(GetUserEmail());
            }

            var items = await _cartItems.GetAllCartItems(cart.Id);

            bool contained = false;

            foreach (var item in items)
            {
                if (Id == item.ProductId)
                {
                    item.Quantity++;
                    await _cartItems.Update(item);
                    contained = true;
                    break;
                }
            }

            if (!contained)
            {
                CartItems cartItems = new CartItems
                {
                    ProductId = Id,
                    CartId = cart.Id,
                    Quantity = 1
                };

                await _cartItems.Create(cartItems);
            }

            Product = prod;
            CurrentCartId = cart.Id;

            return Page();
        }

        /// <summary>
        /// GetUserEmail - This method performs the logic necessary to get the email of the user based on the claims assigned to them upon site registration.
        /// </summary>
        /// <returns>A string containing the email of the user.</returns>
        protected string GetUserEmail()
        {
            return User.Claims.First(x => x.Type == "Email").Value;
        }
    }

    
}
