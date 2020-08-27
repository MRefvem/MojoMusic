using Ecommerce_App.Data;
using Ecommerce_App.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Services
{
    public class CartItemsService : ICartItems
    {
        private readonly StoreDbContext _context;

        // Brings in the storedb, product interface and cart interface
        public CartItemsService(StoreDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Create - Method creates a cart item.
        /// </summary>
        /// <param name="cartItems">The cart item object to be created.</param>
        /// <returns>The created cart item object.</returns>
        public async Task<CartItems> Create(CartItems cartItems)
        {
            _context.Entry(cartItems).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return cartItems;
        }

        /// <summary>
        /// GetCartItem - Method gets a single cart item by the cartid and product id.
        /// </summary>
        /// <param name="CartId">The id of the user's cart.</param>
        /// <param name="ProductId">The id of the product.</param>
        /// <returns>The cart item object.</returns>
        public async Task<CartItems> GetCartItem (int cartId,int productId)
        {
            CartItems cartItem = await _context.CartItems.Where(x => x.CartId == cartId && x.ProductId == productId)
                                                           .Include(x => x.Product)
                                                           .FirstOrDefaultAsync();

            return cartItem;
        }

        /// <summary>
        /// GetAllCartItems - Method gets all cart items associated with a specific user's cart.
        /// </summary>
        /// <param name="cartId">The user's cart id.</param>
        /// <returns>A list of all items in a user's cart.</returns>
        public async Task<List<CartItems>> GetAllCartItems(int cartId)
        {
            List<CartItems> cartItems = await _context.CartItems.Where(x => x.CartId == cartId)
                                                                .Include(x => x.Product)
                                                                .ToListAsync();

            return cartItems;
        }

        /// <summary>
        /// Update - Method updates the contents of a user's cart.
        /// </summary>
        /// <param name="cartItems">The cart item to be updated.</param>
        /// <returns>The updated item.</returns>
        public async Task<CartItems> Update(CartItems cartItems)
        {
            
            _context.Entry(cartItems).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return cartItems;
        }

        /// <summary>
        /// Delete - Method deletes a cart item. It finds the exact product by first searching for the Id of that product and the associated Id of the user's cart.
        /// </summary>
        /// <param name="productId">The id of the product to be deleted.</param>
        /// <param name="cartId">The id of the user's cart.</param>
        /// <returns>The completed task: that item has now been deleted from the user's cart.</returns>
        public async Task Delete(int productId, int cartId)
        {
            CartItems cartItem = await _context.CartItems.FindAsync(cartId, productId);

            if (cartItem == null)
            {
                return;
            }
            else
            {
                _context.Entry(cartItem).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
        }
    }
}
