using Microsoft.AspNetCore.Mvc;
using Store_App.Models.DBClasses;

namespace Store_App.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class TestController
    {
        [HttpGet]
        public Product GetTest()
        {
            return new Product()
            {
                ProductName = "This product was retrieved from the TestController GetTest method!",
            };
        }
    }
}
