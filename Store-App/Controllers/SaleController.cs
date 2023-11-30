using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store_App.Controllers.Interfaces;
using Store_App.Models.DBClasses;

namespace Store_App.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class SaleController : ControllerBase, ISaleController
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

        private bool SaleExists(int saleId)
        {
            return _context.Sales.Any(e => e.SaleId == saleId);
        }

        [HttpPut("{saleId}")]
        public async Task<IActionResult> UpdateSale(int saleId, Sale updatedSale)
        {
            try
            {
                if (saleId != updatedSale?.SaleId)
                {
                    return BadRequest();
                }

                var existingSale = await _context.Sales.FindAsync(saleId);

                if (existingSale == null)
                {
                    return NotFound();
                }

                if (_context == null)
                {
                    // Log the issue or throw an appropriate response
                    Console.WriteLine("UpdateSale: The '_context' is null.");
                    return StatusCode(500, "Internal Server Error");
                }

                if (existingSale == null)
                {
                    // Log the issue or throw an appropriate response
                    Console.WriteLine("UpdateSale: The 'existingSale' is null.");
                    return StatusCode(500, "Internal Server Error");
                }

                // Ensure that the entity is being tracked by the context
                _context.Attach(existingSale);

                // Update only the necessary properties
                existingSale.StartDate = updatedSale.StartDate;
                existingSale.EndDate = updatedSale.EndDate;
                existingSale.PercentOff = updatedSale.PercentOff;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleExists(saleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in UpdateSale: {ex}");
                throw;
            }
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
