using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Store_App.Models.DBClasses
{
    public partial class Cart
    {
        [Required]
        public int CartId { get; set; }

        [Required]
        public double Total { get; set; }

        public virtual ICollection<Person> People { get; set; } = new List<Person>();

        public virtual ICollection<ProductToCart> ProductToCarts { get; set; } = new List<ProductToCart>();
    }
}