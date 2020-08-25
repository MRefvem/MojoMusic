using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce_App.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<Customer> _signInMnager;

        public LogoutModel(SignInManager<Customer> signInManager)
        {
            _signInMnager = signInManager;
        }

        public void OnGet()
        {

        }

        /// <summary>
        /// Logout OnPost method queries the signInManager and performs the operations necessary for a user to logout of their account.
        /// </summary>
        /// <returns>The task complete, user is logged out of their account and then redirected back home.</returns>
        public async Task<IActionResult> OnPost()
        {
            //logout
            await _signInMnager.SignOutAsync();
            return RedirectToPagePermanent("Index", "Home");
        }
    }
}