using Microsoft.AspNetCore.Mvc;
using Store_App.Models.Authentication;
using Store_App.Models.DBClasses;

namespace Store_App.Controllers.Interfaces
{
    public interface IPersonController
    {
        bool Login(LoginRequest request);
        bool Logout();
        Task<ActionResult<Person>> GetPerson(int personId);
        Task<ActionResult<Person>> CreatePerson(Person person);
        Task<ActionResult> UpdatePerson(int personId, Person person);
        Task<ActionResult> DeletePerson(int personId);
    }
}
