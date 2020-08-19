using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Ecommerce_App.Data;
using Ecommerce_App.Models;
using Ecommerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Ecommerce_App.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private UserManager<Customer> _userManager;
        private SignInManager<Customer> _signInManager;
        private IEmailSender _emailSenderService;
        private ICart _cart;

        public RegisterModel(UserManager<Customer> userManager, SignInManager<Customer> signInManager, IEmailSender emailSenderService, ICart cart)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSenderService = emailSenderService;
            _cart = cart;
        }

        // This lets the data from the frontend get sent to the server
        [BindProperty]
        public RegisterViewModel Input { get; set; }

        // Reserved method name for loading the page
        public void OnGet()
        {
            // what is going to populate on the page load?
        }

        // Another reserved method name for the post of the page
        public async Task<IActionResult> OnPost()
        {
         
            if (ModelState.IsValid)
            {
                Customer customer = new Customer()
                {
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    Email = Input.Email,
                    UserName = Input.Email
                };
               var result = await _userManager.CreateAsync(customer, Input.Password);

                if (result.Succeeded)
                {
                    Claim claim = new Claim("FullName", $"{Input.FirstName} {Input.LastName}");
                    await _userManager.AddClaimAsync(customer, claim);
                    await _signInManager.SignInAsync(customer, isPersistent: false);

                    // send them a email
                    string subject = "Welcome to Mojo Music";
                    string htmlMessage = $"<h1> We're excited to have you here {customer.FirstName}<h1>";
                    await _emailSenderService.SendEmailAsync(customer.Email, subject, htmlMessage);
                    _cart.Create(customer.Email).Wait();
                    // redirects HOME
                    return RedirectToAction("Index", "Home");
                }
                
            }

            return Page();
            
        }

        public class RegisterViewModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Compare("Password")]
            [Display(Name = "Confirm Password")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }
        }
    }


}