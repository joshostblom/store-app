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
    [Route("api/[controller]")]
    [ApiController]
    public class DetailedProductController : ControllerBase
    {
        private readonly StoreAppDbContext _detailedProductContext;

        public DetailedProductController(StoreAppDbContext detailedProductContext)
        {
            _detailedProductContext = detailedProductContext;
        }

        [HttpGet("{detailedProductId}")]
        public async Task<ActionResult<DetailedProduct>> GetDetailedProduct(int detailedProductId)
        {
            var detailedProduct = await _detailedProductContext.DetailedProducts.FindAsync(detailedProductId);

            if (detailedProduct == null)
            {
                return NotFound();
            }

            return detailedProduct;
        }

        [HttpPost]
        public async Task<ActionResult<DetailedProduct>> CreateDetailedProduct(DetailedProduct detailedProduct)
        {
            _detailedProductContext.DetailedProducts.Add(detailedProduct);
            await _detailedProductContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDetailedProduct), new { detailedProductId = detailedProduct.DetailedProductId }, detailedProduct);
        }

        [HttpPut("{detailedProductId}")]
        public async Task<ActionResult> UpdateDetailedProduct(int detailedProductId, DetailedProduct detailedProduct)
        {
            if (detailedProductId != detailedProduct.DetailedProductId)
            {
                return BadRequest();
            }

            _detailedProductContext.Entry(detailedProduct).State = EntityState.Modified;

            try
            {
                await _detailedProductContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok();
        }

        [HttpDelete("{detailedProductId}")]
        public async Task<ActionResult> DeleteDetailedProduct(int detailedProductId)
        {
            var detailedProduct = await _detailedProductContext.DetailedProducts.FindAsync(detailedProductId);

            if (detailedProduct == null)
            {
                return NotFound();
            }

            _detailedProductContext.DetailedProducts.Remove(detailedProduct);
            await _detailedProductContext.SaveChangesAsync();

            return Ok();
        }
    }
}
