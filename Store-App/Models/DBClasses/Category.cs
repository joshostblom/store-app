using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Store_App.Models.DBClasses
{
    public partial class Category
    {
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public virtual ICollection<ProductToCategory> ProductToCategories { get; set; } = new List<ProductToCategory>();
    }
}