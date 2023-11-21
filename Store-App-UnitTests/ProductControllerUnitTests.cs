using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Store_App.Controllers;
using Store_App.Models.DBClasses;
using Xunit;

namespace Store_App_UnitTests
{
    internal class ProductControllerUnitTests
    {

        public void GetTest_ReturnsProduct()
        {
            // Arrange
            var controller = new ProductController(null);

            // Act
            var result = controller.GetTest();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
        }

        public void SearchProducts_ReturnsFilteredProducts()
        {
            // Arrange
            var dbContextMock = new Mock<StoreAppDbContext>();
            dbContextMock.Setup(m => m.Products).Returns(new DbSetMock<Product>(new List<Product>
            {
            new Product { ProductName = "TestProduct1" },
            new Product { ProductName = "TestProduct2" }
            }));
            var controller = new ProductController(dbContextMock.Object);

            // Act
            var result = controller.SearchProducts("Test");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Product>>(result);
            Assert.Equal(2, result.Count());
        }

        public async Task GetProducts_ReturnsAllProducts()
        {
            // Arrange
            var dbContextMock = new Mock<StoreAppDbContext>();
            dbContextMock.Setup(m => m.Products).Returns(new DbSetMock<Product>(new List<Product>
            {
            new Product { ProductId = 1, ProductName = "TestProduct1" },
            new Product { ProductId = 2, ProductName = "TestProduct2" }
            }));
            var controller = new ProductController(dbContextMock.Object);

            // Act
            var result = await controller.GetProducts();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Product>>>(result);
            Assert.NotNull(actionResult.Value);
            Assert.Equal(2, actionResult.Value.Count());
        }

        public async Task GetProduct_ReturnsProductById()
        {
            // Arrange
            var dbContextMock = new Mock<StoreAppDbContext>();
            dbContextMock.Setup(m => m.Products.FindAsync(It.IsAny<int>())).ReturnsAsync(new Product { ProductId = 1, ProductName = "TestProduct" });
            var controller = new ProductController(dbContextMock.Object);

            // Act
            var result = await controller.GetProduct(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Product>>(result);
            Assert.NotNull(actionResult.Value);
            Assert.Equal("TestProduct", actionResult.Value.ProductName);
        }

        public async Task PostProduct_ReturnsCreatedAtAction()
        {
            // Arrange
            var dbContextMock = new Mock<StoreAppDbContext>();
            var controller = new ProductController(dbContextMock.Object);
            var newProduct = new Product { ProductName = "NewProduct" };

            // Act
            var result = await controller.PostProduct(newProduct);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Same(newProduct, createdAtActionResult.Value);
            Assert.Equal(nameof(ProductController.GetProduct), createdAtActionResult.ActionName);
        }

        public async Task PutProduct_UpdatesProduct()
        {
            // Arrange
            var dbContextMock = new Mock<StoreAppDbContext>();
            var controller = new ProductController(dbContextMock.Object);
            var existingProduct = new Product { ProductId = 1, ProductName = "ExistingProduct" };

            // Act
            var result = await controller.PutProduct(1, existingProduct);

            // Assert
            Assert.IsType<OkResult>(result);
            dbContextMock.Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        public async Task DeleteProduct_ReturnsOkResult()
        {
            // Arrange
            var dbContextMock = new Mock<StoreAppDbContext>();
            dbContextMock.Setup(m => m.Products.FindAsync(It.IsAny<int>())).ReturnsAsync(new Product { ProductId = 1, ProductName = "TestProduct" });
            var controller = new ProductController(dbContextMock.Object);

            // Act
            var result = await controller.DeleteProduct(1);

            // Assert
            Assert.IsType<OkResult>(result);
            dbContextMock.Verify(m => m.Products.Remove(It.IsAny<Product>()), Times.Once);
            dbContextMock.Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        public async Task DeleteProduct_ReturnsNotFoundWhenProductNotFound()
        {
            // Arrange
            var dbContextMock = new Mock<StoreAppDbContext>();
            dbContextMock.Setup(m => m.Products.FindAsync(It.IsAny<int>())).ReturnsAsync((Product)null);
            var controller = new ProductController(dbContextMock.Object);

            // Act
            var result = await controller.DeleteProduct(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
            dbContextMock.Verify(m => m.Products.Remove(It.IsAny<Product>()), Times.Never);
            dbContextMock.Verify(m => m.SaveChangesAsync(), Times.Never);
        }
    }

}
