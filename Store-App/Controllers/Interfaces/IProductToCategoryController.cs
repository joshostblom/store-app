using Microsoft.AspNetCore.Mvc;
using Store_App.Models.DBClasses;

namespace Store_App.Controllers.Interfaces
{
    public interface IProductToCategoryController
    {
        Task<ActionResult<IEnumerable<ProductToCategory>>> GetProductsInCategory(int categoryId);
        Task<ActionResult<ProductToCategory>> AddProductToCategory(int categoryId, int productId);
        Task<ActionResult> RemoveProductFromCategory(int categoryId, int productId);
    }
}
