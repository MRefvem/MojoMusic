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

        public UserProfileModel(UserManager<Customer> userManager, SignInManager<Customer> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// OnGet - Method that asks the userManager for information about the current user and makes it available to be read on the page
        /// </summary>
        /// <returns>The task complete, user information is available</returns>
        public async Task<IActionResult> OnGet()
        {
            Customer user = await _userManager.GetUserAsync(User);

            CurrentUser = user;

            return Page();
        }

        /// <summary>
        /// OnPost - Method that takes in information inputted by the user, asks the Db for information about the current user and then uses the input to update that info before sending it back to be saved. Also updates the user's claims in accordance.
        /// </summary>
        /// <returns>The task complete, user info has been updated.</returns>
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

            return RedirectToPage("UserProfile");
        }
    }
}