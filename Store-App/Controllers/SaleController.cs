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
    public class SaleController : ControllerBase
    {
        private readonly StoreAppDbContext _context;

        public SaleController(StoreAppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sale>>> GetSales()
        {
            var sales = await _context.Sales.ToListAsync();
            return sales;
        }

        [HttpGet("{saleId}")]
        public async Task<ActionResult<Sale>> GetSale(int saleId)
        {
            var sale = await _context.Sales.FindAsync(saleId);

            if (sale == null)
            {
                return NotFound();
            }

            return sale;
        }

        [HttpPost]
        public async Task<ActionResult<Sale>> CreateSale(Sale sale)
        {
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSale), new { saleId = sale.SaleId }, sale);
        }

        [HttpPut("{saleId}")]
        public async Task<ActionResult> UpdateSale(int saleId, Sale sale)
        {
            if (saleId != sale.SaleId)
            {
                return BadRequest();
            }

            _context.Entry(sale).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok();
        }

        [HttpDelete("{saleId}")]
        public async Task<ActionResult> DeleteSale(int saleId)
        {
            var sale = await _context.Sales.FindAsync(saleId);

            if (sale == null)
            {
                return NotFound();
            }

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
