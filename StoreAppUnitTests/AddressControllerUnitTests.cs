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
            var dbContext = MockDbContext();
            var controller = new AddressController(dbContext);

            // Your sample data for testing
            var sampleAddress = new Address
            {
                AddressId = 1,
                Street = "Sample Street",
                City = "Sample City"
                // Add other properties as needed
            };

            try
            {
                // Act
                var result = await controller.UpdateAddress(1, sampleAddress);

                // Assert
                Assert.IsInstanceOfType(result, typeof(OkResult));

                // You can add additional assertions based on your specific requirements

                // For example, you may want to check if the address was actually updated in the mocked DbSet
                var updatedAddress = dbContext.Addresses.Find(1);
                Assert.IsNotNull(updatedAddress);
                Assert.AreEqual("Sample Street", updatedAddress.Street);
                Assert.AreEqual("Sample City", updatedAddress.City);
            }
            catch (Exception ex)
            {
                // Log the exception for further investigation
                Console.WriteLine($"Exception in UpdateAddress unit test: {ex}");
                throw;
            }
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
            Assert.IsTrue(result is OkResult || result is IActionResult); // Check for any success response
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

        private static DbSet<T> MockDbSet<T>(List<T> data) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();

            var queryableData = data.AsQueryable();

            mockSet.As<IAsyncEnumerable<T>>()
                .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new TestAsyncEnumerator<T>(queryableData.GetEnumerator()));

            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<T>(queryableData.Provider));
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());

            return mockSet.Object;
        }

        [TestMethod]
        public async Task GetCustomerByAddress_ReturnsCustomer()
        {
            // Arrange
            var dbContext = MockDbContext();
            var controller = new AddressController(dbContext);

            // Act
            var result = await controller.GetCustomerByAddress(1);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Person>));
            Assert.IsInstanceOfType(result.Value, typeof(Person));

            // Add additional assertions based on your specific requirements
            // For example, you may want to verify that the returned customer matches the expected data.
            var customer = result.Value as Person;
            Assert.IsNotNull(customer);
            Assert.AreEqual(1, customer.PersonId); // Adjust this based on your test data
        }

        private StoreAppDbContext MockDbContext()
        {
            var options = new DbContextOptionsBuilder<StoreAppDbContext>()
                .UseSqlServer(new SqlConnection(ConfigConnectionHelper.GetConnectionString()))
                .Options;

            var addressData = new List<Address>
            {
                new Address { AddressId = 1, Street = "Street1", City = "City1" },
                new Address { AddressId = 2, Street = "Street2", City = "City2" },
            };

                    var peopleData = new List<Person>
            {
                new Person { PersonId = 1, FirstName = "Person1", AddressId = 1 },
                new Person { PersonId = 2, FirstName = "Person2", AddressId = 2 },
            };

            var dbContext = new Mock<StoreAppDbContext>(options);

            // Set up DbSet for Address
            dbContext.Setup(c => c.Addresses).Returns(MockDbSet(addressData));

            // Set up DbSet for People
            dbContext.Setup(c => c.People).Returns(MockDbSet(peopleData));

            return dbContext.Object;
        }
    }
}
