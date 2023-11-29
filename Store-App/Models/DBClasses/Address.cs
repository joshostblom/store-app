using System;
using System.Collections.Generic;

namespace Store_App.Models.DBClasses
{
    public partial class Address
    {
        public int AddressId { get; set; }

        public string Street { get; set; } = null!;

        public string City { get; set; } = null!;

        public string State { get; set; } = null!;

        public string Country { get; set; } = null!;

        public string PostalCode { get; set; } = null!;

        public virtual ICollection<Person> People { get; set; } = new List<Person>();
    }
}