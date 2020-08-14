using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Interfaces
{
    public interface IProduct
    {

        /// <summary>
        /// Create - allow us to create a product
        /// </summary>
        /// <param name="product"> product we want to create</param>
        /// <returns> created product object </returns>
        Task<Product> Create(Product product);


        /// <summary>
        /// GetProducts - This method allows us to get a list of all of our products
        /// </summary>
        /// <returns>A list of all products</returns>
        Task<List<Product>> GetProducts();

        /// <summary>
        /// GetProduct - This method allows us to get a list of all products that match the searched name
        /// </summary>
        /// <param name="name">Takes in a string that represents the name searched on the front end</param>
        /// <returns>Returns a list containing all the objects that match the searched name</returns>
        Task<Product> GetProduct(int Id);

        /// <summary>
        /// Gets product by name
        /// </summary>
        /// <param name="name">name of the product</param>
        /// <returns> product object</returns>
        Task<Product> GetProductByName(string name);

        /// <summary>
        /// Update- allows us to update a product
        /// </summary>
        /// <param name="product"> product object we want to update</param>
        /// <returns> task completion </returns>
        Task<Product> Update(Product product);

        /// <summary>
        /// Delete - allows us to delete a product
        /// </summary>
        /// <param name="id"> id of the item we want to delete</param>
        /// <returns> task completion</returns>
        Task Delete(int id);

        /// <summary>
        /// Sort - This method allows us to sort the list of products alphabetically and reverse-alphabetically
        /// </summary>
        /// <param name="alphabetical">Takes in a string that correlates to button (alphabetical or reverse-alphabetical) selection on the front end</param>
        /// <returns>Returns a list of products according to selected button</returns>
       // List<Product> Sort(string alphabetical);
    }
}
