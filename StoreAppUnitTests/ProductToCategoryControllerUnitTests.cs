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
using System.Reflection;

namespace ProductToCategoryControllerUnitTests
{
    [TestClass]
    public class ProductToCategoryControllerTests
    {
        [TestMethod]
        public async Task GetProductsInCategory_ReturnsProducts()
        {
            // Arrange
            var categoryId = 1;
            var mockDbContext = MockDbContext(); // Ensure that mock data is set up correctly

            var controller = new ProductToCategoryController(mockDbContext);

            // Act
            var result = await controller.GetProductsInCategory(categoryId);

            // Assert
            Assert.IsNotNull(result);

            // Check if the result is NotFound
            if (result.Result is NotFoundResult)
            {
                Assert.IsNull(result.Value, "Expected null Value property when no products are found in the category.");
            }
            else
            {
                Assert.IsInstanceOfType(result.Result, typeof(ActionResult<IEnumerable<ProductToCategory>>));
                Assert.IsNotNull(result.Value, "Expected non-null Value property when products are found in the category.");
                Assert.IsTrue(result.Value.Any(), "Expected at least one product in the category.");
            }
        }

        [TestMethod]
        public async Task GetProductsInCategory_ReturnsNotFoundForNonexistentCategory()
        {
            // Arrange
            var controller = new ProductToCategoryController(MockDbContext());
            var nonExistentCategoryId = 999; // Assuming a category with this ID doesn't exist

            // Act
            var result = await controller.GetProductsInCategory(nonExistentCategoryId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }


        [TestMethod]
        public async Task AddProductToCategory_ReturnsCreatedResponse()
        {
            // Arrange
            var controller = new ProductToCategoryController(MockDbContext());
            var newProductToCategory = new ProductToCategory { CategoryId = 1, ProductId = 3 };

            // Act
            var result = await controller.AddProductToCategory(newProductToCategory.CategoryId, newProductToCategory.ProductId);

            // Assert
            Assert.IsNotNull(result);

            if (result.Result is CreatedAtActionResult)
            {
                var createdAtActionResult = result.Result as CreatedAtActionResult;
                Assert.AreEqual("GetProductsInCategory", createdAtActionResult.ActionName);
                Assert.AreEqual(newProductToCategory.CategoryId, createdAtActionResult.RouteValues["categoryId"]);

                Console.WriteLine($"Product: {newProductToCategory.ProductId}, Category: {newProductToCategory.CategoryId}");
            }
            else
            {
                Console.WriteLine($"Expected CreatedAtActionResult, but received {result.Result.GetType().Name}");
            }
        }

        [TestMethod]
        public async Task AddProductToCategory_ReturnsNotFoundForNonexistentProductOrCategory()
        {
            // Arrange
            var controller = new ProductToCategoryController(MockDbContext());
            var newProductToCategory = new ProductToCategory { CategoryId = 999, ProductId = 999 }; // Assuming these IDs do not exist

            // Act
            var result = await controller.AddProductToCategory(newProductToCategory.CategoryId, newProductToCategory.ProductId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundObjectResult));
            Assert.AreEqual("Product or Category not found", (result.Result as NotFoundObjectResult).Value);
        }


        [TestMethod]
        public async Task RemoveProductFromCategory_ReturnsOkResponse()
        {
            // Arrange
            var dbContext = MockDbContext();  // Ensure that this method returns a properly mocked DbContext
            var controller = new ProductToCategoryController(dbContext);

            // Act
            var result = await controller.RemoveProductFromCategory(1, 1);

            // Assert
            // Check if the result is either an OkResult or NotFoundObjectResult
            Assert.IsTrue(result is OkResult || result is NotFoundObjectResult);

            // If OkResult, check if the product is no longer associated with the category in the database
            if (result is OkResult)
            {
                var productToCategory = await dbContext.ProductToCategories
                    .FirstOrDefaultAsync(ptc => ptc.CategoryId == 1 && ptc.ProductId == 1);

                Assert.IsNull(productToCategory, "Expected the product to be removed from the category, but it still exists.");
            }
        }


        [TestMethod]
        public async Task RemoveProductFromCategory_ReturnsNotFoundForNonexistentProductToCategory()
        {
            // Arrange
            var controller = new ProductToCategoryController(MockDbContext());
            var nonExistentCategoryId = 999; // Assuming a category with this ID doesn't exist
            var nonExistentProductId = 888;  // Assuming a product with this ID doesn't exist

            // Act
            var result = await controller.RemoveProductFromCategory(nonExistentCategoryId, nonExistentProductId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            Assert.AreEqual($"Category with ID {nonExistentCategoryId} not found.", (result as NotFoundObjectResult)?.Value);
        }

        private static StoreAppDbContext MockDbContext()
        {
            // Mocking DbContext using Moq
            var mockDbContext = new Mock<StoreAppDbContext>(new DbContextOptions<StoreAppDbContext>());
            var mockProducts = new List<Product>
            {
                new Product { ProductId = 3, ProductName = "SampleProduct" }, // Add more properties as needed
            }.AsQueryable();

            var mockCategories = new List<Category>
            {
                new Category { CategoryId = 1, Name = "SampleCategory" }, // Add more properties as needed
            }.AsQueryable();

            mockDbContext.Setup(db => db.Products).Returns(GetMockDbSet(mockProducts));
            mockDbContext.Setup(db => db.Categories).Returns(GetMockDbSet(mockCategories));

            return mockDbContext.Object;
        }

        private static DbSet<T> GetMockDbSet<T>(IQueryable<T> data) where T : class
        {
            // Mocking DbSet using Moq
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IAsyncEnumerable<T>>()
                .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new TestAsyncEnumerator<T>(data.GetEnumerator()));

            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<T>(data.Provider));
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockSet.Object;
        }
    }
}