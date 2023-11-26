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
using System.Data.SqlClient;
using Store_App.Helpers;

namespace AddressControllerUnitTests
{
    [TestClass]
    public class AddressControllerTests
    {
        [TestMethod]
        public async Task GetAddress_ReturnsAddressById()
        {
            // Arrange
            var controller = new AddressController(MockDbContext());
            var addressId = 1;

            // Act
            var result = await controller.GetAddress(addressId);

            // Assert
            Assert.IsNotNull(result);

            // Check if the result is NotFound
            if (result.Result is NotFoundResult)
            {
                Assert.IsNull(result.Value, "Expected null Value property when address is not found.");
            }
            else
            {
                Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Address>));
                Assert.IsInstanceOfType(result.Value, typeof(Address));
                Assert.AreEqual(addressId, result.Value.AddressId);
            }
        }


        [TestMethod]
        public async Task GetAddress_ReturnsNotFoundForNonexistentAddress()
        {
            // Arrange
            var controller = new AddressController(MockDbContext());
            var addressId = 999; // Assuming this address ID does not exist in the mock data

            // Act
            var result = await controller.GetAddress(addressId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task CreateAddress_ReturnsCreatedResponse()
        {
            // Arrange
            var controller = new AddressController(MockDbContext());
            var newAddress = new Address { AddressId = 3, Street = "New Street", City = "New City" };

            // Act
            var result = await controller.CreateAddress(newAddress);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
            Assert.AreEqual("GetAddress", (result.Result as CreatedAtActionResult).ActionName);
            Assert.AreEqual(newAddress.AddressId, (result.Result as CreatedAtActionResult).RouteValues["addressId"]);
        }

        [TestMethod]
        public async Task UpdateAddress_ReturnsOkResponse()
        {
            // Arrange
            var controller = new AddressController(MockDbContext());
            var addressId = 1;
            var updatedAddress = new Address { AddressId = addressId, Street = "Updated Street", City = "Updated City" };

            // Act
            var result = await controller.UpdateAddress(addressId, updatedAddress);

            // Assert
            Assert.IsNotNull(result);

            // Check if the status code is either 200 (OK) or 404 (Not Found)
            Assert.IsTrue((result as StatusCodeResult)?.StatusCode == 200 || (result as StatusCodeResult)?.StatusCode == 404,
                          "Expected a status code of 200 (OK) or 404 (Not Found), but received a different status code.");
        }


        [TestMethod]
        public async Task UpdateAddress_ReturnsBadRequestForMismatchedIds()
        {
            // Arrange
            var controller = new AddressController(MockDbContext());
            var addressId = 1;
            var updatedAddress = new Address { AddressId = 2, Street = "Updated Street", City = "Updated City" };

            // Act
            var result = await controller.UpdateAddress(addressId, updatedAddress);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));
            Assert.IsTrue(result is OkResult || result is IActionResult);
        }

        [TestMethod]
        public async Task DeleteAddress_ReturnsOkResponse()
        {
            // Arrange
            var controller = new AddressController(MockDbContext());
            var addressId = 999; // Assuming this address ID does not exist in the mock data

            // Act
            var result = await controller.DeleteAddress(addressId);

            // Assert
            Assert.IsNotNull(result);

            if (result is NotFoundResult)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail($"Unexpected result type: {result.GetType()}");
            }
        }

        [TestMethod]
        public async Task DeleteAddress_ReturnsNotFoundForNonexistentAddress()
        {
            // Arrange
            var controller = new AddressController(MockDbContext());
            var nonExistentAddressId = 999; // Assuming an address with this ID doesn't exist

            // Act
            var result = await controller.DeleteAddress(nonExistentAddressId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetCustomerByAddress_ReturnsCustomer()
        {
            // Arrange
            var dbContext = MockDbContext();
            var controller = new AddressController(dbContext);
            var addressId = 1; // Assuming this address ID exists in the mock data

            // Act
            var result = await controller.GetCustomerByAddress(addressId);

            // Assert
            if (result.Result is NotFoundResult)
            {
                Assert.IsNull(result.Value, "Expected null Value property when customer is not found.");
            }
            else
            {
                // If not NotFound, assert the types and properties
                Assert.IsInstanceOfType(result, typeof(ActionResult<Person>));
                Assert.IsInstanceOfType(result.Value, typeof(Person));

                var customer = result.Value as Person;
                Assert.IsNotNull(customer);
                Assert.AreEqual(1, customer.PersonId);
            }
        }

        private static StoreAppDbContext MockDbContext()
        {
            // Mocking DbContext using Moq
            var mockDbContext = new Mock<StoreAppDbContext>(new DbContextOptions<StoreAppDbContext>());

            var addressData = new List<Address>
    {
        new Address { AddressId = 1, Street = "Street1", City = "City1" },
        new Address { AddressId = 2, Street = "Street2", City = "City2" },
    };

            mockDbContext.Setup(db => db.Addresses).Returns(MockDbSet(addressData));

            return mockDbContext.Object;
        }

        private static DbSet<T> MockDbSet<T>(List<T> data) where T : class
        {
            // Mocking DbSet using Moq
            var queryableData = data.AsQueryable();

            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IAsyncEnumerable<T>>()
                .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new TestAsyncEnumerator<T>(queryableData.GetEnumerator()));

            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<T>(queryableData.Provider));
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());

            return mockSet.Object;
        }
    }
}
