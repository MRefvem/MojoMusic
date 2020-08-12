using Ecommerce_App.Models.Interfaces;
using Ecommerce_App.Models.Services;
using Ecommerce_App.Models;
using System;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTest1 
    {
       /* private IProduct BuildRepository()
        {
            return new InventoryManagement(_db);
            
        }

        [Fact]
        public async Task CanCheckGetterAndSetterOfProductModel()
        {
            var product = new InventoryManagement()
            {
                Name = "Guitar",
                SKU ="183db1d",
                Price = 100.20m,
                Description = " Basic guitar"
            };

            var service = BuildRepository();
            var saved = await service.Create(product);

            Assert.Equal("Guitar", saved.Name); */

        [Fact]
        public void test()
        {
            string message = "hello";
            string expected = "hello";

            Assert.Equal(expected,message);
        }
        
    }
}
