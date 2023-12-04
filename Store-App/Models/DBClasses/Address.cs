using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Store_App.Models.DBClasses
{
    public partial class Address
    {
        [Required]
        public int AddressId { get; set; }

        [Required]
        public string Street { get; set; } = null!;

        [Required]
        public string City { get; set; } = null!;

        [Required]
        public string Country { get; set; } = null!;

        [Required]
        public string PostalCode { get; set; } = null!;

        [Required]
        public virtual ICollection<Person> People { get; set; } = new List<Person>();
    }
}