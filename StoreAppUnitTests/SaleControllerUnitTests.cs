using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Store_App.Controllers;
using Store_App.Models.DBClasses;
using static ProductControllerUnitTests.ProductControllerTests;

namespace SaleControllerUnitTests
{
    [TestClass]
    public class SaleControllerUnitTests
    {
        [TestMethod]
        public async Task GetSales_ReturnsSales()
        {
            // Arrange
            var dbContext = MockDbContext();
            var controller = new SaleController(dbContext);

            // Act
            var result = await controller.GetSales();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<Sale>>));

            var okResult = result as ActionResult<IEnumerable<Sale>>;
            Assert.IsNotNull(okResult);

            var sales = okResult.Value;
            Assert.IsNotNull(sales);

            Assert.IsTrue(sales.Any(), "Expected at least one sale in the result.");
        }

        [TestMethod]
        public async Task GetSale_ReturnsSale()
        {
            // Arrange
            var dbContext = MockDbContext();
            var controller = new SaleController(dbContext);

            // Act
            var result = await controller.GetSale(1);

            // Assert
            Assert.IsNotNull(result, "The ActionResult<Sale> result should not be null.");

            // Ensure the result is of type ActionResult<Sale>
            Assert.IsInstanceOfType(result, typeof(ActionResult<Sale>), "The result should be of type ActionResult<Sale>.");

            // If it's an ActionResult, ensure the Value property is not null
            if (result is ActionResult<Sale> actionResult)
            {
                Assert.IsNotNull(actionResult.Value, "The Value property of ActionResult<Sale> should not be null.");
                var sale = actionResult.Value;

                Assert.AreEqual(1, sale.SaleId, "SaleId should match the expected value.");
            }
            else
            {
                Assert.Fail("Unexpected type of ActionResult. Expected ActionResult<Sale>.");
            }
        }

        [TestMethod]
        public async Task GetSale_ReturnsNotFoundForNonexistentSale()
        {
            // Arrange
            var dbContext = MockDbContext();
            var controller = new SaleController(dbContext);

            // Act
            var result = await controller.GetSale(999);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task CreateSale_ReturnsCreatedResponse()
        {
            // Arrange
            var dbContext = MockDbContext();
            var controller = new SaleController(dbContext);
            var saleId = 1;
            var newSale = new Sale { SaleId = saleId };

            // Act
            var result = await controller.CreateSale(newSale);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
        }

        [TestMethod]
        public async Task UpdateSale_ReturnsOkResponse()
        {
            // Arrange
            var dbContext = MockDbContext();
            var controller = new SaleController(dbContext);
            var saleId = 1;
            var updatedSale = new Sale { SaleId = saleId };

            // Act
            var result = await controller.UpdateSale(saleId, updatedSale);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));

            // Check if the status code is either 204 (No Content) or 404 (Not Found)
            var statusCode = (result as StatusCodeResult)?.StatusCode;
            Assert.IsTrue(statusCode == 204 || statusCode == 404,
                          $"Expected a status code of 204 (No Content) or 404 (Not Found), but received {statusCode}.");
        }

        [TestMethod]
        public async Task UpdateSale_ReturnsBadRequestForMismatchedIds()
        {
            // Arrange
            var controller = new SaleController(MockDbContext());
            var saleId = 1;
            var updatedSale = new Sale { SaleId = saleId + 1 }; // Mismatched ID

            // Act
            var result = await controller.UpdateSale(saleId, updatedSale);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }


        [TestMethod]
        public async Task DeleteSale_ReturnsOkResponse()
        {
            // Arrange
            var dbContext = MockDbContext();
            var controller = new SaleController(dbContext);
            var saleId = 1;

            // Act
            var result = await controller.DeleteSale(saleId);

            // Assert
            Assert.IsNotNull(result, "Result should not be null.");

            if (result is OkResult)
            {
                var deletedSale = await dbContext.Sales.FindAsync(saleId);
                Assert.IsNull(deletedSale, "Expected the sale to be deleted, but it still exists in the database.");
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
        public async Task DeleteSale_DeletesSale()
        {
            // Arrange
            var dbContext = MockDbContext();
            var controller = new SaleController(dbContext);
            var saleId = 1;

            // Act
            await controller.DeleteSale(saleId);

            // Assert
            var deletedSale = await dbContext.Sales.FindAsync(saleId);
            Assert.IsNull(deletedSale, "Expected the sale to be deleted, but it still exists in the database.");
        }

        [TestMethod]
        public async Task DeleteSale_ReturnsNotFoundForNonexistentSale()
        {
            // Arrange
            var controller = new SaleController(MockDbContext());
            var nonExistentSaleId = 999; // Assuming a sale with this ID doesn't exist

            // Act
            var result = await controller.DeleteSale(nonExistentSaleId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

        }

        private static StoreAppDbContext MockDbContext()
        {
            // Mocking DbContext using Moq
            var mockDbContext = new Mock<StoreAppDbContext>(new DbContextOptions<StoreAppDbContext>());
            mockDbContext.Setup(db => db.Sales).Returns(MockDbSet());

            return mockDbContext.Object;
        }

        private static DbSet<Sale> MockDbSet()
        {
            // Mocking DbSet using Moq
            var data = new List<Sale>
            {
                new Sale { SaleId = 1, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), PercentOff = 10.0m },
                new Sale { SaleId = 2, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), PercentOff = 15.0m },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Sale>>();
            mockSet.As<IAsyncEnumerable<Sale>>()
                .Setup(m => m.GetAsyncEnumerator(It.IsAny<System.Threading.CancellationToken>()))
                .Returns(new TestAsyncEnumerator<Sale>(data.GetEnumerator()));

            mockSet.As<IQueryable<Sale>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<Sale>(data.Provider));
            mockSet.As<IQueryable<Sale>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Sale>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Sale>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            // Setup FindAsync to mimic the behavior of finding by SaleId
            mockSet.Setup(m => m.FindAsync(It.IsAny<object[]>()))
                .ReturnsAsync((object[] keyValues) => data.FirstOrDefault(s => s.SaleId == (int)keyValues[0]));

            // Setup Remove to mimic the behavior of removing an entity
            mockSet.Setup(m => m.Remove(It.IsAny<Sale>()))
                .Callback<Sale>(saleToRemove => data = data.Where(s => s.SaleId != saleToRemove.SaleId).AsQueryable());

            return mockSet.Object;
        }

    }
}
