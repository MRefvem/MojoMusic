using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Ecommerce_App.Models;
using Ecommerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Ecommerce_App.Pages.Account.LoginModel;

namespace Ecommerce_App.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<Customer> _signInManager;
        private readonly ICart _cart;

        public LoginModel(SignInManager<Customer> signInManager, ICart cart)
        {
            _signInManager = signInManager;
            _cart = cart;
        }

        [BindProperty]
        public LoginViewModel Input { get; set; }

        public void OnGet()
        {

        }

        /// <summary>
        /// OnPost - Method used to gather up all of the information from the database necessary for when the user logs in to their account.
        /// </summary>
        /// <returns>The completed task, user is logged in and then redirected to the home page if successful (method also instantiates a cart for the user if they for some reason do not have one). If unsuccessful, the user is redirected back to the login page for another attempt.</returns>
        public async Task<IActionResult> OnPost()
        {
            // authenticate
            if (ModelState.IsValid)
            {
                // sign in
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, false, false);

                if (result.Succeeded)
                {
                    // checks to see if user has a cart
                    var currentUserCart = _cart.GetCartForUserByEmail(Input.Email);

                    if (currentUserCart == null)
                    {
                        // gives user a cart when the login 
                        await _cart.Create(Input.Email);
                    }

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Invalid Username or Password");
            return Page();
        }

        public class LoginViewModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
    }
}