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
    public class ProductToCartController : ControllerBase
    {
        private readonly StoreAppDbContext _context;

        public ProductToCartController(StoreAppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{cartId}/products")]
        public async Task<ActionResult<IEnumerable<ProductToCart>>> GetProductsInCart(int cartId)
        {
            var productsInCart = await _context.ProductToCarts
                .Where(ptc => ptc.CartId == cartId)
                .Include(ptc => ptc.Product)
                .ToListAsync();

            if (productsInCart == null)
            {
                return NotFound();
            }

            return productsInCart;
        }

        [HttpPost("{cartId}/products/{productId}")]
        public async Task<ActionResult<ProductToCart>> AddProductToCart(int cartId, int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            var cart = await _context.Carts.FindAsync(cartId);

            if (product == null || cart == null)
            {
                return NotFound();
            }

            var productToCart = new ProductToCart
            {
                CartId = cartId,
                ProductId = productId
            };

            _context.ProductToCarts.Add(productToCart);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductsInCart), new { cartId = cartId }, productToCart);
        }

        [HttpDelete("{cartId}/products/{productId}")]
        public async Task<ActionResult> RemoveProductFromCart(int cartId, int productId)
        {
            var productToCart = await _context.ProductToCarts
                .Where(ptc => ptc.CartId == cartId && ptc.ProductId == productId)
                .FirstOrDefaultAsync();

            if (productToCart == null)
            {
                return NotFound();
            }

            _context.ProductToCarts.Remove(productToCart);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
