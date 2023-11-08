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
    public class AddressController : ControllerBase
    {
        private readonly StoreAppDbContext _addressContext;

        public AddressController(StoreAppDbContext addressContext)
        {
            _addressContext = addressContext;
        }

        [HttpGet("{addressId}")]
        public async Task<ActionResult<Address>> GetAddress(int addressId)
        {
            var address = await _addressContext.Addresses.FindAsync(addressId);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        [HttpPost]
        public async Task<ActionResult<Address>> CreateAddress(Address address)
        {
            _addressContext.Addresses.Add(address);
            await _addressContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAddress), new { addressId = address.AddressId }, address);
        }

        [HttpPut("{addressId}")]
        public async Task<ActionResult> UpdateAddress(int addressId, Address address)
        {
            if (addressId != address.AddressId)
            {
                return BadRequest();
            }

            _addressContext.Entry(address).State = EntityState.Modified;

            try
            {
                await _addressContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok();
        }

        [HttpDelete("{addressId}")]
        public async Task<ActionResult> DeleteAddress(int addressId)
        {
            var address = await _addressContext.Addresses.FindAsync(addressId);

            if (address == null)
            {
                return NotFound();
            }

            _addressContext.Addresses.Remove(address);
            await _addressContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{addressId}/customer")]
        public async Task<ActionResult<Person>> GetCustomerByAddress(int addressId)
        {
            var customer = await _addressContext.People.FirstOrDefaultAsync(c => c.AddressId == addressId);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }
    }
}
