using Microsoft.AspNetCore.Mvc;
using Store_App.Models.DBClasses;

namespace Store_App.Controllers.Interfaces
{
    public interface ISaleController
    {
        Task<ActionResult<IEnumerable<Sale>>> GetSales();
        Task<ActionResult<Sale>> GetSale(int saleId);
        Task<ActionResult<Sale>> CreateSale(Sale sale);
        Task<IActionResult> UpdateSale(int saleId, Sale updatedSale);
        Task<ActionResult> DeleteSale(int saleId);
    }
}
