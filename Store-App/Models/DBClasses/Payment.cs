using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Store_App.Models.DBClasses
{
    public partial class Payment
    {
        [Required]
        public int PaymentId { get; set; }

        [Required]
        public string CardLastName { get; set; } = null!;

        [Required]
        public string CardFirstName { get; set; } = null!;

        [Required]
        public string CardNumber { get; set; } = null!;

        [Required]
        public int Cvv { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        public virtual ICollection<Person> People { get; set; } = new List<Person>();
    }
}