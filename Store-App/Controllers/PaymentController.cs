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
        public async Task<ActionResult<Payment>> GetPaymentForCurrentUser()
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

        private bool PaymentExists(int paymentId)
        {
            return _paymentContext.Payments.Any(p => p.PaymentId == paymentId);
        }

        [HttpPut("{paymentId}")]
        public async Task<IActionResult> UpdatePayment(int paymentId, Payment payment)
        {
            if (payment == null || paymentId != payment.PaymentId)
            {
                return BadRequest("Invalid payment data");
            }

            var existingPayment = await _paymentContext.Payments.FindAsync(paymentId);

            if (existingPayment == null)
            {
                return NotFound();
            }

            // Update existingPayment properties with values from the incoming payment
            existingPayment.CardLastName = payment.CardLastName;
            existingPayment.CardFirstName = payment.CardFirstName;
            existingPayment.CardNumber = payment.CardNumber;
            existingPayment.Cvv = payment.Cvv;
            existingPayment.ExpirationDate = payment.ExpirationDate;

            try
            {
                _paymentContext.Entry(existingPayment).State = EntityState.Modified;
                await _paymentContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(paymentId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
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
