using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Store_App.Controllers;
using Store_App.Models.DBClasses;

namespace StoreAppUnitTests
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
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult)); // Use Result property

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var sales = okResult.Value as IEnumerable<Sale>;
            Assert.IsNotNull(sales);
            // Add further assertions as needed for the expected result
        }

        [TestMethod]
        public async Task GetSale_ReturnsSale()
        {
            // Arrange
            var dbContext = MockDbContext(); // Ensure that this method returns a properly mocked DbContext
            var controller = new SaleController(dbContext);

            // Act
            var result = await controller.GetSale(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Value, typeof(Sale));
        }

        [TestMethod]
        public async Task GetSale_ReturnsNotFoundForNonexistentSale()
        {
            // Arrange
            var dbContext = MockDbContext(); // Ensure that this method returns a properly mocked DbContext
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
            var dbContext = MockDbContext(); // Ensure that this method returns a properly mocked DbContext
            var controller = new SaleController(dbContext);
            var newSale = new Sale { /* set necessary properties */ };

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
            var dbContext = MockDbContext(); // Ensure that this method returns a properly mocked DbContext
            var controller = new SaleController(dbContext);
            var updatedSale = new Sale { /* set necessary properties */ };

            // Act
            var result = await controller.UpdateSale(1, updatedSale);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.ExecuteResult, typeof(OkResult));
        }

        [TestMethod]
        public async Task UpdateSale_ReturnsBadRequestForMismatchedIds()
        {
            // Arrange
            var dbContext = MockDbContext(); // Ensure that this method returns a properly mocked DbContext
            var controller = new SaleController(dbContext);
            var updatedSale = new Sale { /* set necessary properties */ };

            // Act
            var result = await controller.UpdateSale(999, updatedSale);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.ExecuteResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task DeleteSale_ReturnsOkResponse()
        {
            // Arrange
            var dbContext = MockDbContext(); // Ensure that this method returns a properly mocked DbContext
            var controller = new SaleController(dbContext);

            // Act
            var result = await controller.DeleteSale(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.ExecuteResult, typeof(OkResult));
        }

        [TestMethod]
        public async Task DeleteSale_ReturnsNotFoundForNonexistentSale()
        {
            // Arrange
            var dbContext = MockDbContext(); // Ensure that this method returns a properly mocked DbContext
            var controller = new SaleController(dbContext);

            // Act
            var result = await controller.DeleteSale(999);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.ExecuteResult, typeof(NotFoundResult));
        }

        private StoreAppDbContext MockDbContext()
        {
            // Create mock data
            var mockData = new List<Sale>
        {
            new Sale { SaleId = 1, /* other properties */ },
            new Sale { SaleId = 2, /* other properties */ },
            // Add more mock data as needed
        }.AsQueryable();

            // Create mock DbSet
            var mockSet = new Mock<DbSet<Sale>>();
            mockSet.As<IQueryable<Sale>>().Setup(m => m.Provider).Returns(mockData.Provider);
            mockSet.As<IQueryable<Sale>>().Setup(m => m.Expression).Returns(mockData.Expression);
            mockSet.As<IQueryable<Sale>>().Setup(m => m.ElementType).Returns(mockData.ElementType);
            mockSet.As<IQueryable<Sale>>().Setup(m => m.GetEnumerator()).Returns(mockData.GetEnumerator());

            // Create mock DbContext
            var mockDbContext = new Mock<StoreAppDbContext>();
            mockDbContext.Setup(c => c.Sales).Returns(mockSet.Object);

            return mockDbContext.Object;
        }
    }
}
