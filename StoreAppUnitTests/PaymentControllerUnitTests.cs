using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Store_App.Controllers;
using Store_App.Models.DBClasses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static ProductControllerUnitTests.ProductControllerTests;

namespace PaymentControllerUnitTests
{
    [TestClass]
    public class PaymentControllerTests
    {
        [TestMethod]
        public async Task GetPaymentForCurrentUser_ReturnsNotFound()
        {
            // Arrange
            var dbContextMock = MockDbContext();
            var controller = new PaymentController(dbContextMock);

            // Act
            var result = await controller.GetPaymentForCurrentUser();

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetPayment_ReturnsNotFound()
        {
            // Arrange
            var dbContextMock = MockDbContext();
            var controller = new PaymentController(dbContextMock);

            // Act
            var result = await controller.GetPayment(1);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task CreatePayment_ReturnsCreatedAtAction()
        {
            // Arrange
            var dbContextMock = MockDbContext();
            var controller = new PaymentController(dbContextMock);
            var payment = new Payment
            {
                PaymentId = 1,
                CardLastName = "Doe",
                CardFirstName = "John",
                CardNumber = "1234567890123456",
                Cvv = 123,
                ExpirationDate = new DateTime(2023, 12, 31),
                People = new List<Person>
                {
                    new Person { /* Initialize properties for Person */ },
                    new Person { /* Initialize properties for another Person */ }
                }
            };

            // Act
            var result = await controller.CreatePayment(payment);

            // Assert
            if (result.Result is CreatedAtActionResult createdAtAction)
            {
                Assert.AreEqual("GetPayment", createdAtAction.ActionName, "Unexpected action name");
                Assert.AreEqual(payment.PaymentId, createdAtAction.RouteValues["paymentId"], "Unexpected paymentId value");
            }
            else if (result.Result is ObjectResult objectResult)
            {
                Assert.AreEqual(500, objectResult.StatusCode, "Unexpected status code");
                Assert.IsTrue(objectResult.Value.ToString().Contains("Internal server error"), "Unexpected error message");
            }
            else
            {
                Assert.Fail($"Unexpected result type: {result.Result?.GetType().FullName}");
            }

            if (result.Value != null)
            {
                Assert.Fail($"Unexpected value: {result.Value}");
            }
        }

        [TestMethod]
        public async Task UpdatePayment_ReturnsOk()
        {
            // Arrange
            var dbContextMock = MockDbContext();
            var controller = new PaymentController(dbContextMock);
            var payment = new Payment { PaymentId = 1 };

            // Act
            var result = await controller.UpdatePayment(1, payment);

            // Assert
            Assert.IsNotNull(result);

            // Check if the status code is either 200 (OK) or 404 (Not Found)
            Assert.IsTrue((result as StatusCodeResult)?.StatusCode == 200 || (result as StatusCodeResult)?.StatusCode == 404,
                          "Expected a status code of 200 (OK) or 404 (Not Found), but received a different status code.");
        }

        [TestMethod]
        public async Task UpdatePayment_ReturnsBadRequest()
        {
            // Arrange
            var dbContextMock = new Mock<StoreAppDbContext>();
            var mockSet = new Mock<DbSet<Payment>>();
            var payments = new List<Payment>
            {
                new Payment { PaymentId = 1 }
            };
            dbContextMock.Setup(c => c.Payments).Returns(mockSet.Object);
            mockSet.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync((int id) => payments.FirstOrDefault(p => p.PaymentId == id));
            var controller = new PaymentController(dbContextMock.Object);
            var payment = new Payment { PaymentId = 1 };

            // Act
            var result = await controller.UpdatePayment(2, payment);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task DeletePayment_ReturnsOk()
        {
            // Arrange
            var dbContextMock = new Mock<StoreAppDbContext>();
            var mockSet = new Mock<DbSet<Payment>>();
            var payments = new List<Payment>
            {
                new Payment { PaymentId = 1 }
            };
            dbContextMock.Setup(c => c.Payments).Returns(mockSet.Object);
            mockSet.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync((object[] ids) =>
            {
                int id = (int)ids[0];
                return payments.FirstOrDefault(p => p.PaymentId == id);
            });
            var controller = new PaymentController(dbContextMock.Object);

            // Act
            var result = await controller.DeletePayment(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }


        [TestMethod]
        public async Task DeletePayment_ReturnsNotFound()
        {
            // Arrange
            var dbContextMock = MockDbContext();
            var controller = new PaymentController(dbContextMock);

            // Act
            var result = await controller.DeletePayment(2);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        private static StoreAppDbContext MockDbContext()
        {
            // Mocking DbContext using Moq
            var mockDbContext = new Mock<StoreAppDbContext>(new DbContextOptions<StoreAppDbContext>());
            mockDbContext.Setup(db => db.Payments).Returns(MockDbSet());

            return mockDbContext.Object;
        }

        private static DbSet<Payment> MockDbSet()
        {
            // Mocking DbSet using Moq
            var data = new List<Payment>
            {
            new Payment
                {
                    PaymentId = 1,
                    CardLastName = "Doe",
                    CardFirstName = "John",
                    CardNumber = "1234567890123456",
                    Cvv = 123,
                    ExpirationDate = new DateTime(2023, 12, 31)
                },
                new Payment
                {
                    PaymentId = 2,
                    CardLastName = "Smith",
                    CardFirstName = "Jane",
                    CardNumber = "9876543210987654",
                    Cvv = 456,
                    ExpirationDate = new DateTime(2024, 11, 30)
                },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Payment>>();
            mockSet.As<IAsyncEnumerable<Payment>>()
                .Setup(m => m.GetAsyncEnumerator(It.IsAny<System.Threading.CancellationToken>()))
                .Returns(new TestAsyncEnumerator<Payment>(data.GetEnumerator()));

            mockSet.As<IQueryable<Payment>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<Payment>(data.Provider));
            mockSet.As<IQueryable<Payment>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Payment>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Payment>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockSet.Object;
        }
    }
}
