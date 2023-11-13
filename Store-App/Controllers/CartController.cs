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
    public class CartController : ControllerBase
    {
        private readonly StoreAppDbContext _cartContext;

        public CartController(StoreAppDbContext cartContext)
        {
            _cartContext = cartContext;
        }

        [HttpGet("{cartId}")]
        public async Task<ActionResult<Cart>> GetCart(int cartId)
        {
            var cart = await _cartContext.Carts.FindAsync(cartId);

            if (cart == null)
            {
                return NotFound();
            }

            return cart;
        }

        [HttpPost]
        public async Task<ActionResult<Cart>> CreateCart(Cart cart)
        {
            _cartContext.Carts.Add(cart);
            await _cartContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCart), new { cartId = cart.CartId }, cart);
        }

        [HttpPut("{cartId}")]
        public async Task<ActionResult> UpdateCart(int cartId, Cart cart)
        {
            if (cartId != cart.CartId)
            {
                return BadRequest();
            }

            _cartContext.Entry(cart).State = EntityState.Modified;

            try
            {
                await _cartContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok();
        }

        [HttpDelete("{cartId}")]
        public async Task<ActionResult> DeleteCart(int cartId)
        {
            var cart = await _cartContext.Carts.FindAsync(cartId);

            if (cart == null)
            {
                return NotFound();
            }

            _cartContext.Carts.Remove(cart);
            await _cartContext.SaveChangesAsync();

            return Ok();
        }
    }
}