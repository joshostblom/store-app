using Microsoft.AspNetCore.Mvc;
using Store_App.Models.DBClasses;

namespace Store_App.Controllers.Interfaces
{
    public interface IAddressController
    {
        Task<ActionResult<Address>> GetAddressForCurrentUser();
        Task<ActionResult<Address>> GetAddress(int addressId);
        Task<ActionResult<Address>> CreateAddress(Address address);
        Task<IActionResult> UpdateAddress(int addressId, Address address);
        Task<ActionResult> DeleteAddress(int addressId);
        Task<ActionResult<Person>> GetCustomerByAddress(int addressId);
    }
}
