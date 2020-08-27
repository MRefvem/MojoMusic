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
        private readonly UserManager<Customer> _userManager;
        private readonly SignInManager<Customer> _signInManager;
        private readonly IEmailSender _emailSenderService;
        private readonly ICart _cart;

        public RegisterModel(UserManager<Customer> userManager, SignInManager<Customer> signInManager, IEmailSender emailSenderService, ICart cart)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSenderService = emailSenderService;
            _cart = cart;
        }

        [BindProperty]
        public RegisterViewModel Input { get; set; }

        public void OnGet()
        {

        }

        /// <summary>
        /// OnPost - Method performs all of the internal operations necessary to prepare what information is necessary to have when posting to this route.
        /// </summary>
        /// <returns>The completed action, user is registered to an account (if successful) or redirected back to this page if the attempt is unsuccessful.</returns>
        public async Task<IActionResult> OnPost()
        {
         
            if (ModelState.IsValid)
            {
                Customer customer = new Customer()
                {
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    PhoneNumber = Input.PhoneNumber,
                    Address = Input.Address,
                    City = Input.City,
                    State = Input.State,
                    Zip = Input.Zip,
                    Email = Input.Email,
                    UserName = Input.Email
                };

               var result = await _userManager.CreateAsync(customer, Input.Password);

                if (result.Succeeded)
                {
                    // Assign claims to the user
                    Claim claim = new Claim("FullName", $"{Input.FirstName} {Input.LastName}");
                    Claim claimEmail = new Claim("Email", $"{Input.Email}");
                    await _userManager.AddClaimAsync(customer, claim);
                    await _userManager.AddClaimAsync(customer, claimEmail);
                    await _signInManager.SignInAsync(customer, isPersistent: false);

                    // Send an email to the user upon successful registration
                    string subject = "Welcome to Mojo Music";
                    string htmlMessage = $"<h1>We're excited to have you here {customer.FirstName}<h1>";
                    await _emailSenderService.SendEmailAsync(customer.Email, subject, htmlMessage);
                    _cart.Create(customer.Email).Wait();

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
            [EmailAddress]
            [Compare("Email")]
            [Display(Name = "Confirm Email")]
            public string ConfirmEmail { get; set; }

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

            [Required]
            [DataType(DataType.PhoneNumber)]
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }

            [Required]
            public string Address { get; set; }

            [Required]
            public string City { get; set; }

            [Required]
            public string State { get; set; }

            [Required]
            public int Zip { get; set; }
        }
    }


}