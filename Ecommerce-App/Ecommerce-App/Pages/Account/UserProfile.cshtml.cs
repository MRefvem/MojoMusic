using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Ecommerce_App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Ecommerce_App.Pages.Account
{
    public class UserProfileModel : PageModel
    {
        private readonly UserManager<Customer> _userManager;
        private readonly SignInManager<Customer> _signInManager;

        [BindProperty]
        public Customer CurrentUser { get; set; }
        //[BindProperty]
        //public string FirstName { get; set; }
        //[BindProperty]
        //public string LastName { get; set; }
        //[BindProperty]
        //[DataType(DataType.PhoneNumber)]
        //public string PhoneNumber { get; set; }
        //[BindProperty]
        //public string Address { get; set; }
        //[BindProperty]
        //public string City { get; set; }
        //[BindProperty]
        //public string State { get; set; }
        //[BindProperty]
        //public string Zip { get; set; }

        public UserProfileModel(UserManager<Customer> userManager, SignInManager<Customer> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGet()
        {
            Customer user = await _userManager.GetUserAsync(User);

            CurrentUser = user;

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            Customer userToUpdate = await _userManager.GetUserAsync(User);

            userToUpdate.FirstName = CurrentUser.FirstName;
            userToUpdate.LastName = CurrentUser.LastName;
            userToUpdate.PhoneNumber = CurrentUser.PhoneNumber;
            userToUpdate.Address = CurrentUser.Address;
            userToUpdate.City = CurrentUser.City;
            userToUpdate.State = CurrentUser.State;
            userToUpdate.Zip = CurrentUser.Zip;

            var result = await _userManager.UpdateAsync(userToUpdate);

            if (result.Succeeded)
            {
                var currentClaims = await _userManager.GetClaimsAsync(userToUpdate);

                Claim nameClaim = currentClaims.Where(x => x.Type == "FullName").FirstOrDefault();
                Claim emailClaim = currentClaims.Where(x => x.Type == "Email").FirstOrDefault();

                await _userManager.RemoveClaimAsync(userToUpdate, nameClaim);
                await _userManager.RemoveClaimAsync(userToUpdate, emailClaim);

                Claim updatedNameClaim = new Claim("FullName", $"{userToUpdate.FirstName} {userToUpdate.LastName}");
                Claim updatedEmailClaim = new Claim("Email", $"{userToUpdate.Email}");

                await _userManager.AddClaimAsync(userToUpdate, updatedNameClaim);
                await _userManager.AddClaimAsync(userToUpdate, updatedEmailClaim);

                var updatedClaims = await _userManager.GetClaimsAsync(userToUpdate);

                await _signInManager.SignOutAsync();
                await _signInManager.SignInWithClaimsAsync(userToUpdate, true, updatedClaims);
                
                CurrentUser = userToUpdate;
            }

            return Page();
        }
    }
}