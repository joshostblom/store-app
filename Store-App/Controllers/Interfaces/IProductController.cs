using Microsoft.AspNetCore.Mvc;
using Store_App.Models.DBClasses;

namespace Store_App.Controllers.Interfaces
{
    public interface IProductController
    {
        IEnumerable<Product> SearchProducts(string query);
        Task<ActionResult<IEnumerable<Product>>> GetProducts();
        Task<ActionResult<Product>> GetProduct(int productId);
        Task<ActionResult<Product>> PostProduct(Product product);
        Task<ActionResult> PutProduct(int id, Product product);
        Task<ActionResult> DeleteProduct(int id);
    }
}
