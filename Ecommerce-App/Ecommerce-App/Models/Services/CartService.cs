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
        private readonly StoreDbContext _context;

        //brings in the storedb
        public CartService(StoreDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Create - Method creates a new cart.
        /// </summary>
        /// <param name="UserEmail">The email of the user.</param>
        /// <returns>A new cart was created for the user.</returns>
        public async Task<Cart> Create (string userEmail)
        {
            Cart cart = new Cart()
            {
                UserEmail = userEmail
            };

            cart.IsActive = true;

            _context.Entry(cart).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return cart;
        }

        /// <summary>
        /// GetCartForUserByEmail - Method gets a cart by using the user's email
        /// </summary>
        /// <param name="userEmail">The email of the user.</param>
        /// <returns>The cart of the user that was searched.</returns>
        public async Task<Cart> GetCartForUserByEmail(string userEmail)
        {
           Cart cart = await _context.Carts.Where(x => x.UserEmail == userEmail && x.IsActive == true)
                                            .Include(x => x.CartItems)
                                            .ThenInclude(x => x.Product)
                                            .FirstOrDefaultAsync();

            return cart;
        }

        /// <summary>
        /// Update - Method updates the cart status of the user
        /// </summary>
        /// <param name="cart">A complete cart obejct.</param>
        /// <returns>An updated cart object.</returns>
        public async Task<Cart> Update(Cart cart)
        {
            var cartToUpdate = await _context.Carts.FindAsync(cart.Id); 

            _context.Entry(cartToUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return await _context.Carts.FindAsync(cart.Id);

        }

        /// <summary>
        /// Delete - Method that deletes a cart from the database.
        /// </summary>
        /// <param name="cartId">The id of the cart to be deleted.</param>
        /// <returns>The task complete, the user's cart now deleted from the database.</returns>
        public async Task Delete(int cartId)
        {
            CartItems cart = await _context.CartItems.FindAsync(cartId);

            if (cart == null)
            {
                return;
            }
            else
            {
                _context.Entry(cart).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
        }
    }
}
