using System;
using System.Collections.Generic;

namespace Store_App.Models.DBClasses
{
    public partial class Payment
    {
        public int PaymentId { get; set; }

        public string CardLastName { get; set; } = null!;

        public string CardFirstName { get; set; } = null!;

        public string CardNumber { get; set; } = null!;

        public int Cvv { get; set; }

        public DateTime ExpirationDate { get; set; }

        public virtual ICollection<Person> People { get; set; } = new List<Person>();
    }
}