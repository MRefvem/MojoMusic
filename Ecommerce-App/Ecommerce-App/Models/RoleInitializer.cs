using Ecommerce_App.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ecommerce_App.Models
{
    public class RoleInitializer
    {
        // Create a list of identity roles
        private static readonly List<IdentityRole> Roles = new List<IdentityRole>()
        {
            new IdentityRole{Name = ApplicationRoles.Admin, NormalizedName = ApplicationRoles.Admin.ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString() }
        };

        /// <summary>
        /// SeedData - Sets the groundwork for being able to assign roles to future users.
        /// </summary>
        /// <param name="serviceProvider">The service provider interface.</param>
        /// <param name="users">The user manager, manages all of the user's in the program.</param>
        /// <param name="_config">IConfiguration, controls all of the configuration settings established in the Startup.cs file.</param>
        /// <returns>The completed task, a set of rules now in place that will specify how users are granted certain roles and permissions.</returns>
        public async static Task SeedData(IServiceProvider serviceProvider, UserManager<Customer> users, IConfiguration _config)
        {
            using (var dbContext = new UserDbContext(serviceProvider.GetRequiredService<DbContextOptions<UserDbContext>>()))
            {
                dbContext.Database.EnsureCreated();
                AddRoles(dbContext);
                await SeedUsers(users, _config);
            }
        }

        /// <summary>
        /// SeedUsers - Method that creates the seeded administrator in the database upon application startup.
        /// </summary>
        /// <param name="userManager">The user manager, manages all of the user's in the program.</param>
        /// <param name="_config">IConfiguration, controls all of the configuration settings established in the Startup.cs file.</param>
        /// <returns>The completed task, a seeded administrator created in the root of the program, can access special areas of the site and perform high-level CRUD operations.</returns>
        private async static Task SeedUsers(UserManager<Customer> userManager, IConfiguration _config)
        {
            if (userManager.FindByEmailAsync(_config["AdminEmail"]).Result == null)
            {
                Customer user = new Customer();
                user.UserName = _config["AdminEmail"];
                user.Email = _config["AdminEmail"];
                user.FirstName = "User";
                user.LastName = "Admin";

                IdentityResult result = userManager.CreateAsync(user, _config["AdminPassword"]).Result;

                if (result.Succeeded)
                {
                    Claim claim = new Claim("FullName", $"{user.FirstName} {user.LastName}");
                    Claim emailClaim = new Claim("Email", user.Email);
                    var result1 = userManager.AddClaimAsync(user, claim).Result;
                    var result2 = userManager.AddClaimAsync(user, emailClaim).Result;
                    await userManager.AddToRoleAsync(user, ApplicationRoles.Admin);
                }
            }
        }

        /// <summary>
        /// AddRoles - Method that allows the program to give roles to certain approved users (notably the administrator).
        /// </summary>
        /// <param name="context">Establishes the UserDbContext as the database the program is trying to access/modify.</param>
        private static void AddRoles(UserDbContext context)
        {
            if (context.Roles.Any()) return;

            foreach (var role in Roles)
            {
                context.Roles.Add(role);
                context.SaveChanges();
            }
        }
    }
}
