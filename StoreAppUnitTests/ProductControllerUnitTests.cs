using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Store_App.Controllers;
using Store_App.Models.DBClasses;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace ProductControllerUnitTests
{
    [TestClass]
    public class ProductControllerTests
    {
        [TestMethod]
        public void GetTest_ReturnsProduct()
        {
            // Arrange
            var controller = new ProductController(MockDbContext());

            // Act
            var result = controller.GetTest();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Product));
        }

        [TestMethod]
        public void SearchProducts_ReturnsFilteredProducts()
        {
            // Arrange
            var controller = new ProductController(MockDbContext());
            var query = "Test";

            // Act
            var result = controller.SearchProducts(query);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.All(p => p.ProductName.ToLower().Contains(query.ToLower())));
        }

        [TestMethod]
        public async Task GetProducts_ReturnsAllProducts()
        {
            // Arrange
            var controller = new ProductController(MockDbContext());

            // Act
            var result = await controller.GetProducts();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Value, typeof(List<Product>));
        }

        [TestMethod]
        public async Task GetProduct_ReturnsProductById()
        {
            // Arrange
            var controller = new ProductController(MockDbContext());
            var productId = 1;

            // Act
            var result = await controller.GetProduct(productId);

            // Assert
            Assert.IsNotNull(result);

            // Check if the result is NotFound
            if (result.Result is NotFoundResult)
            {
                Assert.IsNull(result.Value); // Ensure Value is null when product is not found
            }
            else
            {
                Assert.IsInstanceOfType(result.Value, typeof(Product));
                Assert.AreEqual(productId, result.Value.ProductId);
            }
        }

        private static StoreAppDbContext MockDbContext()
        {
            // Mocking DbContext using Moq
            var mockDbContext = new Mock<StoreAppDbContext>(new DbContextOptions<StoreAppDbContext>());
            mockDbContext.Setup(db => db.Products).Returns(MockDbSet());

            return mockDbContext.Object;
        }

        private static DbSet<Product> MockDbSet()
        {
            // Mocking DbSet using Moq
            var data = new List<Product>
            {
                new Product { ProductId = 1, ProductName = "TestProduct1" },
                new Product { ProductId = 2, ProductName = "TestProduct2" },
                // Add more sample data as needed
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IAsyncEnumerable<Product>>()
                .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new TestAsyncEnumerator<Product>(data.GetEnumerator()));

            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<Product>(data.Provider));
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockSet.Object;
        }

        // Add these classes to support asynchronous queries in the mock
        public class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
        {
            private readonly IEnumerator<T> _enumerator;

            public TestAsyncEnumerator(IEnumerator<T> enumerator) => _enumerator = enumerator;

            public ValueTask<bool> MoveNextAsync() => new ValueTask<bool>(_enumerator.MoveNext());

            public T Current => _enumerator.Current;

            public ValueTask DisposeAsync() => new ValueTask(Task.CompletedTask);
        }

        public class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
        {
            private readonly IQueryProvider _inner;

            internal TestAsyncQueryProvider(IQueryProvider inner) => _inner = inner;

            public IQueryable CreateQuery(Expression expression) => new TestAsyncEnumerable<TEntity>(expression);

            public IQueryable<TElement> CreateQuery<TElement>(Expression expression) => new TestAsyncEnumerable<TElement>(expression);

            public object Execute(Expression expression) => _inner.Execute(expression);

            public TResult Execute<TResult>(Expression expression) => _inner.Execute<TResult>(expression);

            public IAsyncEnumerable<TResult> ExecuteAsync<TResult>(Expression expression) =>
                new TestAsyncEnumerable<TResult>(expression);

            public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken) =>
                Execute<TResult>(expression);
        }

        public class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
        {
            public TestAsyncEnumerable(IEnumerable<T> enumerable) : base(enumerable) { }

            public TestAsyncEnumerable(Expression expression) : base(expression) { }

            public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default) =>
                new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

    }
}
