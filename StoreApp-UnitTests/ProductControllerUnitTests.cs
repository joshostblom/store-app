using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Store_App.Controllers;
using Store_App.Models.DBClasses;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProductControllerUnitTests
{
    [TestClass]
    public class ProductControllerTests
    {
        [TestMethod]
        public void GetTest_ReturnsProduct()
        {
            // Arrange
            var controller = new ProductController(null);

            // Act
            var result = controller.GetTest();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Product));
        }

        [TestMethod]
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
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Product>));
            Assert.AreEqual(2, (result as List<Product>).Count);
        }

        [TestMethod]
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
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<Product>>));
            var actionResult = (ActionResult<IEnumerable<Product>>)result;

            // Extract the value from ActionResult
            var products = actionResult.Value;

            Assert.IsNotNull(products);
            Assert.AreEqual(2, products.Count());
        }

        [TestMethod]
        public async Task GetProduct_ReturnsProductById()
        {
            // Arrange
            var dbContextMock = new Mock<StoreAppDbContext>();
            dbContextMock.Setup(m => m.Products.FindAsync(It.IsAny<int>())).ReturnsAsync(new Product { ProductId = 1, ProductName = "TestProduct" });
            var controller = new ProductController(dbContextMock.Object);

            // Act
            var result = await controller.GetProduct(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Product>));
            var actionResult = (ActionResult<Product>)result;
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual("TestProduct", actionResult.Value.ProductName);
        }

        [TestMethod]
        public async Task PutProduct_UpdatesProduct()
        {
            // Arrange
            var dbContextMock = new Mock<StoreAppDbContext>();
            var controller = new ProductController(dbContextMock.Object);
            var existingProduct = new Product { ProductId = 1, ProductName = "ExistingProduct" };

            // Act
            var result = await controller.PutProduct(1, existingProduct);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
            dbContextMock.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task DeleteProduct_ReturnsOkResult()
        {
            // Arrange
            var dbContextMock = new Mock<StoreAppDbContext>();
            dbContextMock.Setup(m => m.Products.FindAsync(It.IsAny<int>())).ReturnsAsync(new Product { ProductId = 1, ProductName = "TestProduct" });
            var controller = new ProductController(dbContextMock.Object);

            // Act
            var result = await controller.DeleteProduct(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
            dbContextMock.Verify(m => m.Products.Remove(It.IsAny<Product>()), Times.Once);
            dbContextMock.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task DeleteProduct_ReturnsNotFoundWhenProductNotFound()
        {
            // Arrange
            var dbContextMock = new Mock<StoreAppDbContext>();
            dbContextMock.Setup(m => m.Products.FindAsync(It.IsAny<int>())).ReturnsAsync((Product)null);
            var controller = new ProductController(dbContextMock.Object);

            // Act
            var result = await controller.DeleteProduct(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

            // Verify that SaveChangesAsync was called once
            dbContextMock.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

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

                // Setup other DbSet methods as needed
                mockDbSet.Setup(m => m.Add(It.IsAny<T>())).Callback<T>(data.Add);
                mockDbSet.Setup(m => m.Remove(It.IsAny<T>())).Callback<T>(entity => data.Remove(entity));
                mockDbSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => data.FirstOrDefault(item => MatchesIds(item, ids)));

                return mockDbSet;
            }

            private static bool MatchesIds<T>(T item, object[] ids)
            {
                var properties = typeof(T).GetProperties().Where(p => p.GetCustomAttributes(typeof(KeyAttribute), true).Length > 0);
                var itemValues = properties.Select(p => p.GetValue(item)).ToArray();
                return itemValues.SequenceEqual(ids);
            }
        }
    }
}
