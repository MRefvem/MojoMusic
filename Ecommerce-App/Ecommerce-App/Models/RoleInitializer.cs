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
        // create a list of identity roles
        private static readonly List<IdentityRole> Roles = new List<IdentityRole>()
        {
            new IdentityRole{Name = ApplicationRoles.Admin, NormalizedName = ApplicationRoles.Admin.ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString() }
        };

        // method that creates the admin
        public async static Task SeedData(IServiceProvider serviceProvider, UserManager<Customer> users, IConfiguration _config)
        {
            using (var dbContext = new UserDbContext(serviceProvider.GetRequiredService<DbContextOptions<UserDbContext>>()))
            {
                dbContext.Database.EnsureCreated();
                AddRoles(dbContext);
                await SeedUsers(users, _config);
            }
        }

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
