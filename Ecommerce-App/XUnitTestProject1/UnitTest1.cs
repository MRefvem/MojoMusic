using Ecommerce_App.Models.Interfaces;
using Ecommerce_App.Models.Services;
using Ecommerce_App.Models;
using System;
using Xunit;
using Ecommerce.Models;
using System.Collections.Generic;

namespace XUnitTestProject1
{
    public class UnitTest1 : DatabaseTest
    {
       private IProduct BuildRepository()
        {
            return new InventoryManagement(_db);
        }

        [Fact]
        public async void CanCreateNewProduct()
        {
            // Arrange
            Product product = new Product()
            {
                Name = "Drum",
                SKU = "313ndjn",
                Description = "Basic drum set",
                Price = 100.00m,

            };

            var repo = BuildRepository();

            // Act
            var saved = await repo.Create(product);
        
            //Assert

            Assert.NotNull(saved);
            Assert.Equal("Drum", saved.Name);
        }


        [Fact]
        public async void CanGetAProduct()
        {
            // Arrange
            var service = BuildRepository();


            // Act
            var result = await service.GetProduct(1);

            //Assert

            Assert.Equal("Piano", result.Name);
          
        }


        [Fact]
        public async void CanGetAllProduct()
        {
            // Arrange
            var service = BuildRepository();

            // Act
            List<Product> result = await service.GetProducts();

            //Assert
            Assert.Equal(10, result.Count);
        }


        [Fact]
        public async void CanUpdateProduct()
        {
            // Arrange
            var service = BuildRepository();
            var updatedProduct = new Product
            {
                Id = 1,
                Name = "Stienway Piano",
            };

            // Act
            Product result = await service.Update(updatedProduct);

            //Assert
            Assert.Equal("Stienway Piano", result.Name);
        }


        [Fact]
        public async void CanDeleteProduct()
        {
            // Arrange 
            var service = BuildRepository();

            // Act & Assert
            List<Product> result = await service.GetProducts();
            Assert.Equal(10, result.Count);
            await service.Delete(1);
            List<Product> result2 = await service.GetProducts();
            Assert.Equal(9, result2.Count);

        }

        [Fact]
        public async void CanCheckIfThereAreNoProducts()
        {
            // Arrange
            var service = BuildRepository();

            await service.Delete(1);
            await service.Delete(2);
            await service.Delete(3);
            await service.Delete(4);
            await service.Delete(5);
            await service.Delete(6);
            await service.Delete(7);
            await service.Delete(8);
            await service.Delete(9);
            await service.Delete(10);

            //Act

            List<Product> result = await service.GetProducts();
            //Assert
            Assert.Empty(result);
        }
           

        [Fact]
        public async void CannotDeleteFromEmptyTable()
        {
            // Arrange
            var service = BuildRepository();

            await service.Delete(1);
            await service.Delete(2);
            await service.Delete(3);
            await service.Delete(4);
            await service.Delete(5);
            await service.Delete(6);
            await service.Delete(7);
            await service.Delete(8);
            await service.Delete(9);
            await service.Delete(10);

            //Act

            List<Product> result = await service.GetProducts();
            await service.Delete(11);
            //Assert
            Assert.Empty(result);
        }

        [Fact]
        public void CheckingGetterSetterForProduct()
        {
            Product product = new Product
            {
                Id = 1,
                Name = "Drum",
                Description = "Basic drum",
                SKU = "131NJNN",
                Price = 100.00m,
            };

            Assert.Equal(1, product.Id);
            Assert.Equal("Drum", product.Name);
            Assert.Equal("Basic drum", product.Description);
            Assert.Equal("131NJNN", product.SKU);
            Assert.Equal(100.00m, product.Price);
        }

    }
}
