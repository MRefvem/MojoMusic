using Ecommerce_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Interfaces
{
    public interface IProduct
    {

        /// <summary>
        /// Create - Method that allow us to create a new product.
        /// </summary>
        /// <param name="product">The product we want to create.</param>
        /// <returns>Information about the newly created product.</returns>
        Task<Product> Create(Product product);

        /// <summary>
        /// GetProducts - This method allows us to get a list of all of our products.
        /// </summary>
        /// <returns>A list of all products in the database.</returns>
        Task<List<Product>> GetProducts();

        /// <summary>
        /// GetProduct - This method allows us to get a list of all products that match the searched name.
        /// </summary>
        /// <param name="name">Takes in a string that represents the name searched on the front end.</param>
        /// <returns>Returns a list containing all the objects that match the searched name.</returns>
        Task<Product> GetProduct(int Id);

        /// <summary>
        /// GetProductByName - Gets product by name.
        /// </summary>
        /// <param name="name">The name of the product the program wants to find more information for.</param>
        /// <returns>The task complete, product information found.</returns>
        Task<Product> GetProductByName(string name);

        /// <summary>
        /// GetProduct - This method allows us to get a list of all products that match the searched name.
        /// </summary>
        /// <param name="name">Takes in a string that represents the name searched on the front end.</param>
        /// <returns>Returns a list containing all the objects that match the searched name.</returns>
        Task<List<Product>> SearchByProductName(string searchString);

        /// <summary>
        /// Update - Method allows the program to update information about a product.
        /// </summary>
        /// <param name="product">The product the administrator seeks to update information about.</param>
        /// <returns>The task complete: the product is now updated with the inputted information.</returns>
        Task<Product> Update(Product product);

        /// <summary>
        /// Delete - Method that allows the program to delete a product.
        /// </summary>
        /// <param name="id">The id of the item the administrator has chosen to delete.</param>
        /// <returns>The task completion, the stored product now deleted from the database.</returns>
        Task Delete(int id);
    }
}
