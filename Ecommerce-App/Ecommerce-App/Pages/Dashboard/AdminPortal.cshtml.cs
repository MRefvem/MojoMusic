using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_App.Models;
using Ecommerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce_App.Pages.Dashboard
{
    [Authorize(Policy = "AdminOnly")]
    public class AdminPortalModel : PageModel
    {
        private readonly IProduct _product;
        private readonly IOrder _order;
        private readonly ICart _cart;
        private readonly ICartItems _cartItems;

        public List<Product> Product { get; set; }
        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public string SKU { get; set; }
        [BindProperty]
        [Column(TypeName = "decimal(7,2)")]
        public decimal Price { get; set; }
        [BindProperty]
        public string Description { get; set; }
        public List<Order> Orders { get; set; }
        public Cart Cart { get; set; }
        public decimal Total { get; set; }

        public AdminPortalModel(IProduct product, IOrder order, ICart cart, ICartItems cartItems)
        {
            _product = product;
            _order = order;
            _cart = cart;
            _cartItems = cartItems;
        }

        /// <summary>
        /// OnGet - Method queries the products database and returns information about all products to display upon loading the page, also queries the products database and returns information about all of the orders ever placed on the website and prepares that information to be viewed by the administrator.
        /// </summary>
        /// <returns>The completed task, information about all products successfully rendered to the page.</returns>
        public async Task<IActionResult> OnGet()
        {
            var prods = await _product.GetProducts();

            List<Order> allOrders = await _order.GetCompleteCompanyOrderHistory();
            Orders = allOrders;

            decimal totalPrice = 0;

            if (allOrders != null)
            {
                foreach (var order in allOrders)
                {
                    foreach (var cartItem in order.Cart.CartItems)
                    {
                        totalPrice += cartItem.Product.Price * cartItem.Quantity;
                    }
                    order.Cart.Total = totalPrice;
                    Total = totalPrice;
                }
            }

            Product = prods;

            return Page();
        }

        /// <summary>
        /// OnPost - On posting to the page, a new product is constructed in this method and that information is packaged into a product object to be sent to the products database through the .Create method.
        /// </summary>
        /// <returns>The task complete, a new product has been added to the database.</returns>
        public async Task<IActionResult> OnPost()
        {
            Product newProduct = new Product()
            {
                Name = Name,
                SKU = SKU,
                Price = Price,
                Description = Description,
            };

            await _product.Create(newProduct);

            return await OnGet();
        }
    }
}