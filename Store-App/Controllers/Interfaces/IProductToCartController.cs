using Microsoft.AspNetCore.Mvc;
using Store_App.Models.DBClasses;

namespace Store_App.Controllers.Interfaces
{
    public interface IProductToCartController
    {
        Task<ActionResult<IEnumerable<ProductToCart>>> GetProductsInCartForCurrentUser();
        Task<ActionResult<IEnumerable<ProductToCart>>> GetProductsInCart(int cartId);
        Task<ActionResult<ProductToCart>> AddProductToCart(int cartId, int productId);
        Task<ActionResult> RemoveProductFromCart(int cartId, int productId);
        Task<ActionResult> RemoveAllProductsFromCartForCurrentUser();
    }
}
