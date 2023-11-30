using Microsoft.AspNetCore.Mvc;
using Store_App.Models.DBClasses;

namespace Store_App.Controllers.Interfaces
{
    public interface ICartController
    {
        Task<ActionResult<Cart>> GetCartForCurrentUser();
        Task<ActionResult<Cart>> GetCart(int cartId);
        Task<ActionResult<Cart>> CreateCart(Cart cart);
        Task<ActionResult> UpdateCart(int cartId, Cart cart);
        Task<ActionResult> SetCurrentCartTotalToZero();
        Task<ActionResult> DeleteCart(int cartId);
    }
}
