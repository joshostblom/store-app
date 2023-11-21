using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Store_App.Controllers;
using Store_App.Helpers;
using Store_App.Models.DBClasses;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CategoryControllerUnitTests
{
    [TestClass]
    public class CategoryControllerTests
    {
        [TestMethod]
        public async Task GetCategory_ReturnsCategory()
        {
            // Arrange
            var dbContextMock = new Mock<StoreAppDbContext>();
            var controller = new CategoryController(dbContextMock.Object);
            var categoryId = 1;
            var category = new Category { CategoryId = categoryId };

            dbContextMock.Setup(x => x.Categories.FindAsync(categoryId))
                .ReturnsAsync(category);

            // Act
            var result = await controller.GetCategory(categoryId);

            // Assert
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(categoryId, result.Value.CategoryId);
        }

        [TestMethod]
        public async Task GetCategory_ReturnsNotFound()
        {
            // Arrange
            var dbContextMock = new Mock<StoreAppDbContext>();
            var controller = new CategoryController(dbContextMock.Object);
            var categoryId = 1;

            dbContextMock.Setup(x => x.Categories.FindAsync(categoryId))
                .ReturnsAsync((Category)null);

            // Act
            var result = await controller.GetCategory(categoryId);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task CreateCategory_ReturnsCreatedAtAction()
        {
            // Arrange
            var dbContextMock = new Mock<StoreAppDbContext>();
            var controller = new CategoryController(dbContextMock.Object);
            var category = new Category { CategoryId = 1 };

            dbContextMock.Setup(x => x.Categories.Add(It.IsAny<Category>()))
                .Callback((Category addedCategory) =>
                {
                    Assert.IsNotNull(addedCategory);
                });

            // Act
            var result = await controller.CreateCategory(category);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
            var createdAtActionResult = (CreatedAtActionResult)result.Result;
            Assert.AreEqual("GetCategory", createdAtActionResult.ActionName);
            Assert.AreEqual(category.CategoryId, createdAtActionResult.RouteValues["categoryId"]);
        }

        [TestMethod]
        public async Task UpdateCategory_ReturnsOk()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<StoreAppDbContext>()
                .UseSqlServer(new SqlConnection(ConfigConnectionHelper.GetConnectionString()))
                .Options;

            using (var dbContext = new StoreAppDbContext(options))
            {
                var controller = new CategoryController(dbContext);

                var category = new Category { Name = "TestCategory" };
                dbContext.Categories.Add(category);
                await dbContext.SaveChangesAsync();

                // Act
                var result = await controller.UpdateCategory(category.CategoryId, category);

                // Assert
                Assert.IsInstanceOfType(result, typeof(OkResult));
            }
        }


        [TestMethod]
        public async Task UpdateCategory_ReturnsBadRequest()
        {
            // Arrange
            var dbContextMock = new Mock<StoreAppDbContext>();
            var controller = new CategoryController(dbContextMock.Object);
            var categoryId = 1;
            var category = new Category { CategoryId = 2 }; // Different ID

            // Act
            var result = await controller.UpdateCategory(categoryId, category);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task DeleteCategory_ReturnsOk()
        {
            // Arrange
            var dbContextMock = new Mock<StoreAppDbContext>();
            var controller = new CategoryController(dbContextMock.Object);
            var categoryId = 1;
            var category = new Category { CategoryId = categoryId };

            dbContextMock.Setup(x => x.Categories.FindAsync(categoryId))
                .ReturnsAsync(category);

            // Act
            var result = await controller.DeleteCategory(categoryId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task DeleteCategory_ReturnsNotFound()
        {
            // Arrange
            var dbContextMock = new Mock<StoreAppDbContext>();
            var controller = new CategoryController(dbContextMock.Object);
            var categoryId = 1;

            dbContextMock.Setup(x => x.Categories.FindAsync(categoryId))
                .ReturnsAsync((Category)null);

            // Act
            var result = await controller.DeleteCategory(categoryId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
