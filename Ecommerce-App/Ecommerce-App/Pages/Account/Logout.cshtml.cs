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
        private SignInManager<Customer> _signInMnager;

        public LogoutModel(SignInManager<Customer> signInManager)
        {
            _signInMnager = signInManager;
        }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            await _signInMnager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}