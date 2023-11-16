using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Store_App.Models.DBClasses;

namespace Store_App.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly StoreAppDbContext _productContext;
        public ProductController(StoreAppDbContext productContext)
        {
            _productContext = productContext;
        }

        [HttpGet]
        public Product GetTest()
        {
            return new Product()
            {
                ProductName = "This product was retrieved from the ProductController GetTest method!",
            };
        }

        [HttpGet("{query}")]
        public IEnumerable<Product> SearchProducts(string query)
        {
            if (_productContext.Products == null)
            {
                return new List<Product>();
            }

            IEnumerable<Product> products = _productContext.Products.ToList().Where(x => x.ProductName.ToLower().Contains(query.ToLower()));

            return products;
        }

        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            var products = new List<Product>();

            foreach(Product product in _productContext.Products)
            {
                var returnProduct = product;
                returnProduct.Sale = _productContext.Sales.Find(product.SaleId);
                products.Add(returnProduct);
            }

            return products;
        }

        [HttpGet("{id}")]
        public Product? GetProduct(int id)
        {
            var product = _productContext.Products.Find(id);

            if (product != null)
            {
                product.Sale = _productContext.Sales.Find(product.SaleId);
            }

            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _productContext.Products.Add(product);
            await _productContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutProduct(int id, Product product)
        {
            if(id != product.ProductId)
            {
                return BadRequest();
            }

            _productContext.Entry(product).State = EntityState.Modified;
            try
            {
                await _productContext.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            if(_productContext.Products == null)
            {
                return NotFound();
            }

            var product = await _productContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _productContext.Products.Remove(product);
            await _productContext.SaveChangesAsync();

            return Ok();
        }

    }
}
