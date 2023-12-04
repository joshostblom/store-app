using Microsoft.AspNetCore.Mvc;
using Store_App.Models.DBClasses;

namespace Store_App.Controllers.Interfaces
{
    public interface IPaymentController
    {
        Task<ActionResult<Payment>> GetPaymentForCurrentUser();
        Task<ActionResult<Payment>> GetPayment(int paymentId);
        Task<ActionResult<Payment>> CreatePayment(Payment payment);
        Task<IActionResult> UpdatePayment(int paymentId, Payment payment);
        Task<ActionResult> DeletePayment(int paymentId);
    }
}
