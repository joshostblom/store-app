using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Store_App.Models.DBClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Store_App.Helpers;

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

        [HttpGet]
        public async Task<ActionResult<Cart>> GetCartForCurrentUser()
        {
            Person? person = UserHelper.GetCurrentUser();
            Cart? cart = null;

            if (person != null)
            {
                cart = await _cartContext.Carts.FindAsync(person.getCartId());
            }
            if (cart == null)
            {
                return NotFound();
            }
            return cart;
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
            // Check if the provided cartId matches the ID in the cart object
            if (cartId != cart.CartId)
            {
                return BadRequest(); // Return BadRequestResult for mismatched IDs
            }

            var existingCart = await _cartContext.Carts.FindAsync(cartId);

            if (existingCart == null)
            {
                return NotFound();
            }

            // Update the properties of existingCart
            existingCart.Total = cart.Total;

            // Set the entity state to Modified
            _cartContext.Entry(existingCart).State = EntityState.Modified;

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
                return NotFound(); // Return NotFoundResult if the cart doesn't exist
            }

            _cartContext.Carts.Remove(cart);
            await _cartContext.SaveChangesAsync();

            return Ok(); // Return OkResult upon successful deletion
        }
    }
}