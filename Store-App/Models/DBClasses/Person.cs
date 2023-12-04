using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Store_App.Models.DBClasses
{
    public partial class Person
    {
        [Required]
        public int PersonId { get; set; }

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        public string FirstName { get; set; } = null!;

        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        public int? AddressId { get; set; }

        public int? PaymentId { get; set; }

        public int? CartId { get; set; }

        public virtual Address? Address { get; set; }

        public virtual Cart? Cart { get; set; }

        public virtual Payment? Payment { get; set; }

        public int? getCartId()
        {
            return this.CartId;
        }
        public int? getPaymentId()
        {
            return this.PaymentId;
        }
        public int? getAddressId()
        {
            return this.AddressId;
        }
    }
}