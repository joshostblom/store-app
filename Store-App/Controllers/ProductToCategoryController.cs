using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store_App.Models.DBClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Store_App.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ProductToCategoryController : ControllerBase
    {
        private readonly StoreAppDbContext _context;

        public ProductToCategoryController(StoreAppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{categoryId}/products")]
        public async Task<ActionResult<IEnumerable<ProductToCategory>>> GetProductsInCategory(int categoryId)
        {
            var productsInCategory = await _context.ProductToCategories
                .Where(ptc => ptc.CategoryId == categoryId)
                .Include(ptc => ptc.Product)
                .ToListAsync();

            if (productsInCategory == null)
            {
                return NotFound();
            }

            return productsInCategory;
        }

        [HttpPost("{categoryId}/products/{productId}")]
        public async Task<ActionResult<ProductToCategory>> AddProductToCategory(int categoryId, int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            var category = await _context.Categories.FindAsync(categoryId);

            if (product == null || category == null)
            {
                return NotFound();
            }

            var productToCategory = new ProductToCategory
            {
                CategoryId = categoryId,
                ProductId = productId
            };

            _context.ProductToCategories.Add(productToCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductsInCategory), new { categoryId = categoryId }, productToCategory);
        }

        [HttpDelete("{categoryId}/products/{productId}")]
        public async Task<ActionResult> RemoveProductFromCategory(int categoryId, int productId)
        {
            var productToCategory = await _context.ProductToCategories
                .Where(ptc => ptc.CategoryId == categoryId && ptc.ProductId == productId)
                .FirstOrDefaultAsync();

            if (productToCategory == null)
            {
                return NotFound();
            }

            _context.ProductToCategories.Remove(productToCategory);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
