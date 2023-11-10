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
    public class PaymentController : ControllerBase
    {
        private readonly StoreAppDbContext _paymentContext;

        public PaymentController(StoreAppDbContext paymentContext)
        {
            _paymentContext = paymentContext;
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
            _paymentContext.Payments.Add(payment);
            await _paymentContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPayment), new { paymentId = payment.PaymentId }, payment);
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
