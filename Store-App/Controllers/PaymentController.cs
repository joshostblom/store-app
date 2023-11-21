using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store_App.Helpers;
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
    public class PaymentController : ControllerBase
    {
        private readonly StoreAppDbContext _paymentContext;

        public PaymentController(StoreAppDbContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        [HttpGet]
        public async Task<ActionResult<Payment>> GetPaymentUsingPersonId()
        {
            Person? person = UserHelper.GetCurrentUser();
            Payment? payment = null;

            if (person != null)
            {
                payment = await _paymentContext.Payments.FindAsync(person.getPaymentId());
            }
            if (payment == null)
            {
                return NotFound();
            }
            return payment;
        }

        [HttpGet("{paymentId}")]
        public async Task<ActionResult<Payment>> GetPayment(int paymentId)
        {
            var payment = await _paymentContext.Payments.FindAsync(paymentId);

            if (payment == null)
            {
                return NotFound();
            }

            return payment;
        }

        [HttpPost]
        public async Task<ActionResult<Payment>> CreatePayment(Payment payment)
        {
            try
            {
                if (payment == null)
                {
                    return BadRequest("Payment object is null");
                }

                _paymentContext.Payments.Add(payment);
                await _paymentContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPayment), new { paymentId = payment.PaymentId }, payment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{paymentId}")]
        public async Task<ActionResult> UpdatePayment(int paymentId, Payment payment)
        {
            if (paymentId != payment.PaymentId)
            {
                return BadRequest();
            }

            _paymentContext.Entry(payment).State = EntityState.Modified;

            try
            {
                await _paymentContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok();
        }

        [HttpDelete("{paymentId}")]
        public async Task<ActionResult> DeletePayment(int paymentId)
        {
            var payment = await _paymentContext.Payments.FindAsync(paymentId);

            if (payment == null)
            {
                return NotFound();
            }

            _paymentContext.Payments.Remove(payment);
            await _paymentContext.SaveChangesAsync();

            return Ok();
        }
    }
}
