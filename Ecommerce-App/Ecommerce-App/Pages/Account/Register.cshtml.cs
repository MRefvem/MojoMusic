using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Ecommerce_App.Data;
using Ecommerce_App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_App.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private UserManager<Customer> _userManager;
        private SignInManager<Customer> _signInManager;

        public RegisterModel(UserManager<Customer> userManager, SignInManager<Customer> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // This lets the data from the frontend get sent to the server
        [BindProperty]
        public RegisterViewModel Input { get; set; }

        // Reserved method name for loading the page
        public void OnGet()
        {

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
                    await _signInManager.SignInAsync(customer, isPersistent:false);

                    // does it redirect us?
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
            //[MaxLength(100)]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }
            
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Compare("Password")]
            [Display(Name = "Confirm Password")]
            public string ConfirmPassword { get; set; }
        }
    }


}