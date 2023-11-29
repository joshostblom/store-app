using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store_App.Controllers.Interfaces;
using Store_App.Helpers;
using Store_App.Models.DBClasses;
using System.Data;

namespace Store_App.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AddressController : ControllerBase, IAddressController
    {
        private readonly StoreAppDbContext _addressContext;

        public AddressController(StoreAppDbContext addressContext)
        {
            _addressContext = addressContext;
        }

        [HttpGet]
        public async Task<ActionResult<Address>> GetAddressForCurrentUser()
        {
            Person? person = UserHelper.GetCurrentUser();
            Address? address = null;

            if (person != null)
            {
                address = await _addressContext.Addresses.FindAsync(person.getAddressId());
            }
            if (address == null)
            {
                return NotFound();
            }
            return address;
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

        public async Task<IActionResult> UpdateAddress(int addressId, Address address)
        {
            if (address == null)
            {
                return BadRequest("The 'address' parameter is null.");
            }

            var existingAddress = await _addressContext.Addresses.FindAsync(addressId);

            if (existingAddress == null)
            {
                return NotFound();
            }

            // Update properties of existingAddress with values from the provided 'address'
            existingAddress.Street = address.Street;
            existingAddress.City = address.City;

            await _addressContext.SaveChangesAsync();

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
            // Check for null _context.People
            if (_addressContext.People == null)
            {
                // You may need to handle this case based on your application's logic
                return NotFound(); // Or another appropriate response
            }

            var customer = await _addressContext.People
                // Check for null before applying Where
                .Where(p => p.AddressId == addressId)
                .FirstOrDefaultAsync();

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }
    }
}
