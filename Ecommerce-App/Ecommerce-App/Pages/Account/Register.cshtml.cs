using System;
using System.Collections.Generic;
using System.Linq;
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
        private UserDbContext _context;

        public RegisterModel(UserManager<Customer> userManager, UserDbContext context)
        {
            _userManager = userManager;
            _context = context;
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
        public IActionResult OnPost()
        {
            // when a user "posts back" what happens?
            // does it save to the DB? YES
            // does it make an API call? NO
            
            // capture all the values in our input and create a user and save them into our database

            if (Input.Password == Input.ConfirmPassword)
            {
                Customer customer = new Customer()
                {
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    Email = Input.Email,
                    Password = Input.Password
                };

                _context.Entry(customer).State = EntityState.Added;
                _context.SaveChanges();

                // does it redirect us?
                // redirects HOME
                return RedirectToAction("Index", "Home");
            }

            

            return RedirectToPage("/Account/Register");
            
        }

        public class RegisterViewModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
    }


}