using System.Runtime.InteropServices;

namespace Store_App.Models.Classes
{
    public class Address
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
