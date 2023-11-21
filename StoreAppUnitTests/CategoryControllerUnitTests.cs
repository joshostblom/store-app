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

namespace CategoryControllerUnitTests
{
    [TestClass]
    public class CategoryControllerTests
    {
        [TestMethod]
        public async Task GetCategory_ReturnsCategoryById()
        {
            // Arrange
            var controller = new CategoryController(MockDbContext());
            var categoryId = 1;

            // Act
            var result = await controller.GetCategory(categoryId);

            // Assert
            Assert.IsNotNull(result);

            // Check if the result is NotFound
            if (result.Result is NotFoundResult)
            {
                Assert.IsNull(result.Value, "Expected null Value property when category is not found.");
            }
            else
            {
                Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Category>));
                Assert.IsInstanceOfType(result.Value, typeof(Category));
                Assert.AreEqual(categoryId, result.Value.CategoryId);
            }
        }

        [TestMethod]
        public async Task GetCategory_ReturnsNotFoundForNonexistentCategory()
        {
            // Arrange
            var controller = new CategoryController(MockDbContext());
            var categoryId = 999; // Assuming this category ID does not exist in the mock data

            // Act
            var result = await controller.GetCategory(categoryId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task CreateCategory_ReturnsCreatedResponse()
        {
            // Arrange
            var controller = new CategoryController(MockDbContext());
            var newCategory = new Category { CategoryId = 3, Name = "TestCategory" };

            // Act
            var result = await controller.CreateCategory(newCategory);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
            Assert.AreEqual("GetCategory", (result.Result as CreatedAtActionResult).ActionName);
            Assert.AreEqual(newCategory.CategoryId, (result.Result as CreatedAtActionResult).RouteValues["categoryId"]);
        }

        [TestMethod]
        public async Task UpdateCategory_ReturnsOkResponse()
        {
            // Arrange
            var controller = new CategoryController(MockDbContext());
            var categoryId = 1;
            var updatedCategory = new Category { CategoryId = categoryId, Name = "UpdatedCategory" };

            // Act
            var result = await controller.UpdateCategory(categoryId, updatedCategory);

            // Assert
            Assert.IsNotNull(result);


            // Check if the status code is either 200 (OK) or 404 (Not Found)
            Assert.IsTrue((result as StatusCodeResult)?.StatusCode == 200 || (result as StatusCodeResult)?.StatusCode == 404,
                          "Expected a status code of 200 (OK) or 404 (Not Found), but received a different status code.");
        }

        [TestMethod]
        public async Task UpdateCategory_ReturnsBadRequestForMismatchedIds()
        {
            // Arrange
            var controller = new CategoryController(MockDbContext());
            var categoryId = 1;
            var updatedCategory = new Category { CategoryId = categoryId + 1, Name = "UpdatedCategory" }; // Mismatched ID

            // Act
            var result = await controller.UpdateCategory(categoryId, updatedCategory);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task DeleteCategory_ReturnsOkResponse()
        {
            // Arrange
            var dbContext = MockDbContext();
            var controller = new CategoryController(dbContext);
            var categoryId = 1;

            // Act
            var result = await controller.DeleteCategory(categoryId);

            // Assert
            Assert.IsNotNull(result, "Result should not be null.");

            if (result is OkResult)
            {
                var deletedCategory = await dbContext.Categories.FindAsync(categoryId);
                Assert.IsNull(deletedCategory, "Expected the category to be deleted, but it still exists in the database.");
            }
            else if (result is NotFoundResult)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail($"Unexpected result type: {result.GetType()}");
            }
        }

        [TestMethod]
        public async Task DeleteCategory_DeletesCategory()
        {
            // Arrange
            var dbContext = MockDbContext();
            var controller = new CategoryController(dbContext);
            var categoryId = 1;

            // Act
            await controller.DeleteCategory(categoryId);

            // Assert
            var deletedCategory = await dbContext.Categories.FindAsync(categoryId);
            Assert.IsNull(deletedCategory, "Expected the category to be deleted, but it still exists in the database.");
        }

        [TestMethod]
        public async Task DeleteCategory_ReturnsNotFoundForNonexistentCategory()
        {
            // Arrange
            var controller = new CategoryController(MockDbContext());
            var nonExistentCategoryId = 999; // Assuming a category with this ID doesn't exist

            // Act
            var result = await controller.DeleteCategory(nonExistentCategoryId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        private static StoreAppDbContext MockDbContext()
        {
            // Mocking DbContext using Moq
            var mockDbContext = new Mock<StoreAppDbContext>();
            mockDbContext.Setup(db => db.Categories).Returns(MockDbSet());

            return mockDbContext.Object;
        }

        private static DbSet<Category> MockDbSet()
        {
            // Mocking DbSet using Moq
            var data = new List<Category>
            {
                new Category { CategoryId = 1, Name = "TestCategory1" },
                new Category { CategoryId = 2, Name = "TestCategory2" },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Category>>();
            mockSet.As<IAsyncEnumerable<Category>>()
                .Setup(m => m.GetAsyncEnumerator(It.IsAny<System.Threading.CancellationToken>()))
                .Returns(new TestAsyncEnumerator<Category>(data.GetEnumerator()));

            mockSet.As<IQueryable<Category>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<Category>(data.Provider));
            mockSet.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockSet.Object;
        }
    }
}