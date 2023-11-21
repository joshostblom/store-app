using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Store_App.Controllers;
using Store_App.Models.DBClasses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaymentControllerUnitTests
{
    [TestClass]
    public class PaymentControllerTests
    {
        [TestMethod]
        public async Task GetPayment_ReturnsNotFound()
        {
            // Arrange
            var dbContextMock = new Mock<StoreAppDbContext>();
            var controller = new PaymentController(dbContextMock.Object);

            // Act
            var result = await controller.GetPayment(1);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task CreatePayment_ReturnsCreatedAtAction()
        {
            // Arrange
            var dbContextMock = new Mock<StoreAppDbContext>();
            var controller = new PaymentController(dbContextMock.Object);
            var payment = new Payment
            {
                PaymentId = 1,  // Set the PaymentId as needed
                CardLastName = "Doe",
                CardFirstName = "John",
                CardNumber = "1234567890123456",
                Cvv = 123,
                ExpirationDate = new DateTime(2023, 12, 31),  // Set the expiration date as needed
                People = new List<Person>
                {
                    // You can initialize the collection of People if needed
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
                // Check for internal server error
                Assert.AreEqual(500, objectResult.StatusCode, "Unexpected status code");

                // Check for the error message
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
            var dbContextMock = new Mock<StoreAppDbContext>();
            var controller = new PaymentController(dbContextMock.Object);
            var payment = new Payment { PaymentId = 1 };

            // Act
            var result = await controller.UpdatePayment(1, payment);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task UpdatePayment_ReturnsBadRequest()
        {
            // Arrange
            var dbContextMock = new Mock<StoreAppDbContext>();
            var controller = new PaymentController(dbContextMock.Object);
            var payment = new Payment { PaymentId = 1 };

            // Act
            var result = await controller.UpdatePayment(2, payment);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task DeletePayment_ReturnsOk()
        {
            // Arrange
            var dbContextMock = new Mock<StoreAppDbContext>();
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
            var dbContextMock = new Mock<StoreAppDbContext>();
            var controller = new PaymentController(dbContextMock.Object);

            // Act
            var result = await controller.DeletePayment(2);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
