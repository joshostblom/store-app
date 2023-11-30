using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Store_App.Controllers;
using Store_App.Models.DBClasses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ProductControllerUnitTests.ProductControllerTests;

namespace ProductToCartControllerUnitTests
{
    public static class MockExtensions
    {
        public static Mock<DbSet<T>> BuildMockDbSet<T>(this IQueryable<T> data) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IAsyncEnumerable<T>>()
                .Setup(m => m.GetAsyncEnumerator(It.IsAny<System.Threading.CancellationToken>()))
                .Returns(new TestAsyncEnumerator<T>(data.GetEnumerator()));

            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<T>(data.Provider));
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockSet;
        }
    }

    [TestClass]
    public class ProductToCartControllerTests
    {
        [TestMethod]
        public async Task GetProductsInCart_ReturnsProducts()
        {
            // Arrange
            var controller = new ProductToCartController(MockDbContext());
            var cartId = 1;

            var productToCarts = new List<ProductToCart>
            {
                new ProductToCart { CartId = cartId, ProductId = 1, Product = new Product { ProductId = 1, ProductName = "Product1" } },
                new ProductToCart { CartId = cartId, ProductId = 2, Product = new Product { ProductId = 2, ProductName = "Product2" } }
            };

            var mockDbSet = productToCarts.AsQueryable().BuildMockDbSet();
            var dbContextMock = new Mock<StoreAppDbContext>();
            dbContextMock.Setup(x => x.ProductToCarts).Returns(mockDbSet.Object);

            // Act
            var result = await controller.GetProductsInCart(cartId);

            // Assert
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(2, result.Value.Count());
        }

        [TestMethod]
        public async Task AddProductToCart_ReturnsCreatedResponse()
        {
            // Arrange
            var dbContextMock = new Mock<StoreAppDbContext>();
            dbContextMock.Setup(m => m.ProductToCarts).Returns(MockDbSet());

            // Mock setup for Products
            var productId = 1;
            dbContextMock.Setup(m => m.Products.FindAsync(productId)).ReturnsAsync(new Product
            {
                // Initialize properties of the Product entity
                ProductId = productId,
                ProductName = "Sample Product",
                Price = 10.0,
                ImageUrl = "sample-image.jpg",
                SaleId = null, // Provide appropriate values or leave null
                Sku = "SAMPLE123",
                Rating = 4.5,
                Descript = "Sample description",
                ManufacturerInformation = "Sample manufacturer info",
                ProdHeight = 5.0,
                ProdWidth = 3.0,
                ProdLength = 10.0,
                ProdWeight = 2.0,
            });

            // Mock setup for Carts
            var cartId = 1;
            dbContextMock.Setup(m => m.Carts.FindAsync(cartId)).ReturnsAsync(new Cart
            {
                // Initialize properties of the Cart entity
                CartId = cartId,
                Total = 0, // Provide an initial value for the Total property
                People = new List<Person>(), // Initialize People property
                ProductToCarts = new List<ProductToCart>(), // Initialize ProductToCarts property
            });

            var controller = new ProductToCartController(dbContextMock.Object);

            // Act
            var result = await controller.AddProductToCart(cartId, productId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
        }

        [TestMethod]
        public async Task RemoveProductFromCart_ReturnsOkResponse()
        {
            // Arrange
            var controller = new ProductToCartController(MockDbContext());
            var cartId = 1;
            var productId = 1;

            // Act
            var result = await controller.RemoveProductFromCart(cartId, productId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task RemoveProductFromCart_ReturnsNotFoundForNonexistentProductToCart()
        {
            // Arrange
            var controller = new ProductToCartController(MockDbContext());
            var cartId = 1;
            var productId = 999; // Assuming this product ID does not exist in the mock data

            // Act
            var result = await controller.RemoveProductFromCart(cartId, productId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        private static StoreAppDbContext MockDbContext()
        {
            // Mocking DbContext using Moq
            var mockDbContext = new Mock<StoreAppDbContext>(new DbContextOptions<StoreAppDbContext>());
            mockDbContext.Setup(db => db.ProductToCarts).Returns(MockDbSet());

            return mockDbContext.Object;
        }

        private static DbSet<ProductToCart> MockDbSet()
        {
            // Mocking DbSet using Moq
            var data = new List<ProductToCart>
            {
                new ProductToCart { CartId = 1, ProductId = 1, Product = new Product { ProductId = 1, ProductName = "Product1" } },
                new ProductToCart { CartId = 1, ProductId = 2, Product = new Product { ProductId = 2, ProductName = "Product2" } },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ProductToCart>>();
            mockSet.As<IAsyncEnumerable<ProductToCart>>()
                .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new TestAsyncEnumerator<ProductToCart>(data.GetEnumerator()));

            mockSet.As<IQueryable<ProductToCart>>().Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<ProductToCart>(data.Provider));
            mockSet.As<IQueryable<ProductToCart>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ProductToCart>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ProductToCart>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockSet.Object;
        }
    }
}
