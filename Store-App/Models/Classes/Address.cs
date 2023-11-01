using System.Runtime.InteropServices;
using Store_App.Models.Interfaces;

namespace Store_App.Models.Classes
{
    public class Address : IAddress
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }

        public Address(string street, string city, string country, string postalCode)
        {
            this.Street = street;
            this.City = city;
            this.Country = country;
            this.PostalCode = postalCode;
        }
    }
}
