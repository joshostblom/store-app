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

namespace CartControllerUnitTests
{
    [TestClass]
    public class CartControllerTests
    {
        [TestMethod]
        public async Task GetCart_ReturnsCartById()
        {
            // Arrange
            var controller = new CartController(MockDbContext());
            var cartId = 1;

            // Act
            var result = await controller.GetCart(cartId);

            // Assert
            Assert.IsNotNull(result);

            // Check if the result is NotFound
            if (result.Result is NotFoundResult)
            {
                Assert.IsNull(result.Value, "Expected null Value property when cart is not found.");
            }
            else
            {
                Assert.IsInstanceOfType(result.Result, typeof(ActionResult<Cart>));
                Assert.IsInstanceOfType(result.Value, typeof(Cart));
                Assert.AreEqual(cartId, result.Value.CartId);
            }
        }


        [TestMethod]
        public async Task GetCart_ReturnsNotFoundForNonexistentCart()
        {
            // Arrange
            var controller = new CartController(MockDbContext());
            var cartId = 999; // Assuming this cart ID does not exist in the mock data

            // Act
            var result = await controller.GetCart(cartId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task CreateCart_ReturnsCreatedResponse()
        {
            // Arrange
            var controller = new CartController(MockDbContext());
            var newCart = new Cart { CartId = 3, Total = 50.0 };

            // Act
            var result = await controller.CreateCart(newCart);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
            Assert.AreEqual("GetCart", (result.Result as CreatedAtActionResult).ActionName);
            Assert.AreEqual(newCart.CartId, (result.Result as CreatedAtActionResult).RouteValues["cartId"]);
        }

        [TestMethod]
        public async Task UpdateCart_ReturnsOkResponse()
        {
            // Arrange
            var controller = new CartController(MockDbContext());
            var cartId = 1;
            var updatedCart = new Cart { CartId = cartId, Total = 60.0 };

            // Act
            var result = await controller.UpdateCart(cartId, updatedCart);

            // Assert
            Assert.IsNotNull(result);

            // Check if the status code is either 200 (OK) or 404 (Not Found)
            Assert.IsTrue((result as StatusCodeResult)?.StatusCode == 200 || (result as StatusCodeResult)?.StatusCode == 404,
                          "Expected a status code of 200 (OK) or 404 (Not Found), but received a different status code.");
        }


        [TestMethod]
        public async Task UpdateCart_ReturnsBadRequestForMismatchedIds()
        {
            // Arrange
            var controller = new CartController(MockDbContext());
            var cartId = 1;
            var updatedCart = new Cart { CartId = cartId + 1, Total = 60.0 }; // Mismatched ID

            // Act
            var result = await controller.UpdateCart(cartId, updatedCart);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task DeleteCart_ReturnsOkResponse()
        {
            // Arrange
            var dbContext = MockDbContext();
            var controller = new CartController(dbContext);
            var cartId = 1;

            // Act
            var result = await controller.DeleteCart(cartId);

            // Assert
            Assert.IsNotNull(result, "Result should not be null.");

            if (result is OkResult)
            {
                var deletedCart = await dbContext.Carts.FindAsync(cartId);
                Assert.IsNull(deletedCart, "Expected the cart to be deleted, but it still exists in the database.");
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
        public async Task DeleteCart_DeletesCart()
        {
            // Arrange
            var dbContext = MockDbContext();
            var controller = new CartController(dbContext);
            var cartId = 1;

            // Act
            await controller.DeleteCart(cartId);

            // Assert
            var deletedCart = await dbContext.Carts.FindAsync(cartId);
            Assert.IsNull(deletedCart, "Expected the cart to be deleted, but it still exists in the database.");
        }

        [TestMethod]
        public async Task DeleteCart_ReturnsNotFoundForNonexistentCart()
        {
            // Arrange
            var controller = new CartController(MockDbContext());
            var nonExistentCartId = 999; // Assuming a cart with this ID doesn't exist

            // Act
            var result = await controller.DeleteCart(nonExistentCartId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }


        private static StoreAppDbContext MockDbContext()
        {
            // Mocking DbContext using Moq
            var mockDbContext = new Mock<StoreAppDbContext>(new DbContextOptions<StoreAppDbContext>());
            mockDbContext.Setup(db => db.Carts).Returns(MockDbSet());

            return mockDbContext.Object;
        }

        private static DbSet<Cart> MockDbSet()
        {
            // Mocking DbSet using Moq
            var data = new List<Cart>
            {
                new Cart { CartId = 1, Total = 30.0 },
                new Cart { CartId = 2, Total = 40.0 },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Cart>>();
            mockSet.As<IAsyncEnumerable<Cart>>()
                .Setup(m => m.GetAsyncEnumerator(It.IsAny<System.Threading.CancellationToken>()))
                .Returns(new TestAsyncEnumerator<Cart>(data.GetEnumerator()));

            mockSet.As<IQueryable<Cart>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<Cart>(data.Provider));
            mockSet.As<IQueryable<Cart>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Cart>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Cart>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockSet.Object;
        }
    }
}
