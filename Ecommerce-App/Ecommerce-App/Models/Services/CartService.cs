using Ecommerce_App.Data;
using Ecommerce_App.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Services
{
    public class CartService : ICart
    {
        private StoreDbContext _context;

        public CartService(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<Cart> Create (string UserEmail)
        {
            Cart cart = new Cart()
            {
                UserEmail = UserEmail
            };
            _context.Entry(cart).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task<Cart> GetCartForUserByEmail (string userEmail)
        {
           Cart cart = await _context.Carts.Where(x => x.UserEmail == userEmail).FirstOrDefaultAsync();
            return cart;
        }

        
    }
}
