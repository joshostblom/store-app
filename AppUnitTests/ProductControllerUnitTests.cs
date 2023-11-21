using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Store_App.Controllers;
using Store_App.Models.DBClasses;
using Xunit;

namespace AppUnitTests
{
    public class ProductControllerUnitTests
    {
        public static class DbSetMock
        {
            public static Mock<DbSet<T>> Create<T>(List<T> data) where T : class
            {
                var queryable = data.AsQueryable();
                var mockDbSet = new Mock<DbSet<T>>();

                mockDbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
                mockDbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
                mockDbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
                mockDbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

                // Setup other DbSet methods as needed (e.g., Add, Remove, Find, etc.)

                return mockDbSet;
            }
        }

        [Fact]
        public void GetTest_ReturnsProduct()
        {
            // Arrange
            var controller = new ProductController(null);

            // Act
            var result = controller.GetTest();

            // Assert
            Xunit.Assert.NotNull(result);
            Xunit.Assert.IsType<Product>(result);
        }

        [Fact]
        public void SearchProducts_ReturnsFilteredProducts()
        {
            // Arrange
            var dbContextMock = new Mock<StoreAppDbContext>();
            dbContextMock.Setup(m => m.Products).Returns(DbSetMock.Create(new List<Product>
            {
            new Product { ProductName = "TestProduct1" },
            new Product { ProductName = "TestProduct2" }
            }).Object);
            var controller = new ProductController(dbContextMock.Object);

            // Act
            var result = controller.SearchProducts("Test");

            // Assert
            Xunit.Assert.NotNull(result);
            Xunit.Assert.IsType<List<Product>>(result);
            Xunit.Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetProducts_ReturnsAllProducts()
        {
            // Arrange
            var dbContextMock = new Mock<StoreAppDbContext>();
            dbContextMock.Setup(m => m.Products).Returns(DbSetMock.Create(new List<Product>
        {
            new Product { ProductId = 1, ProductName = "TestProduct1" },
            new Product { ProductId = 2, ProductName = "TestProduct2" }
        }).Object);
            var controller = new ProductController(dbContextMock.Object);

            // Act
            var result = await controller.GetProducts();

            // Assert
            var actionResult = Xunit.Assert.IsType<ActionResult<IEnumerable<Product>>>(result);
            Xunit.Assert.NotNull(actionResult.Value);
            Xunit.Assert.Equal(2, actionResult.Value.Count());
        }

        [Fact]
        public async Task GetProduct_ReturnsProductById()
        {
            // Arrange
            var dbContextMock = new Mock<StoreAppDbContext>();
            dbContextMock.Setup(m => m.Products.FindAsync(It.IsAny<int>())).ReturnsAsync(new Product { ProductId = 1, ProductName = "TestProduct" });
            var controller = new ProductController(dbContextMock.Object);

            // Act
            var result = await controller.GetProduct(1);

            // Assert
            var actionResult = Xunit.Assert.IsType<ActionResult<Product>>(result);
            Xunit.Assert.NotNull(actionResult.Value);
            Xunit.Assert.Equal("TestProduct", actionResult.Value.ProductName);
        }

        [Fact]
        public async Task PostProduct_ReturnsCreatedAtAction()
        {
            // Arrange
            var dbContextMock = new Mock<StoreAppDbContext>();
            var controller = new ProductController(dbContextMock.Object);
            var newProduct = new Product { ProductName = "NewProduct" };

            // Act
            var result = await controller.PostProduct(newProduct);

            // Assert
            var createdAtActionResult = Xunit.Assert.IsType<CreatedAtActionResult>(result.Result);
            Xunit.Assert.Same(newProduct, createdAtActionResult.Value);
            Xunit.Assert.Equal(nameof(ProductController.GetProduct), createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task PutProduct_UpdatesProduct()
        {
            // Arrange
            var dbContextMock = new Mock<StoreAppDbContext>();
            var controller = new ProductController(dbContextMock.Object);
            var existingProduct = new Product { ProductId = 1, ProductName = "ExistingProduct" };

            // Act
            var result = await controller.PutProduct(1, existingProduct);

            // Assert
            Xunit.Assert.IsType<OkResult>(result);
            dbContextMock.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteProduct_ReturnsOkResult()
        {
            // Arrange
            var dbContextMock = new Mock<StoreAppDbContext>();
            dbContextMock.Setup(m => m.Products.FindAsync(It.IsAny<int>())).ReturnsAsync(new Product { ProductId = 1, ProductName = "TestProduct" });
            var controller = new ProductController(dbContextMock.Object);

            // Act
            var result = await controller.DeleteProduct(1);

            // Assert
            Xunit.Assert.IsType<OkResult>(result);
            dbContextMock.Verify(m => m.Products.Remove(It.IsAny<Product>()), Times.Once);
            dbContextMock.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteProduct_ReturnsNotFoundWhenProductNotFound()
        {
            // Arrange
            var dbContextMock = new Mock<StoreAppDbContext>();
            dbContextMock.Setup(m => m.Products.FindAsync(It.IsAny<int>())).ReturnsAsync((Product)null);
            var controller = new ProductController(dbContextMock.Object);

            // Act
            var result = await controller.DeleteProduct(1);

            // Assert
            Xunit.Assert.IsType<NotFoundResult>(result);
            dbContextMock.Verify(m => m.Products.Remove(It.IsAny<Product>()), Times.Never);
            dbContextMock.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }

}
