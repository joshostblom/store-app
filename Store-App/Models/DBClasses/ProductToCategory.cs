using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Store_App.Models.DBClasses
{
    public partial class ProductToCategory
    {
        [Required]
        public int ProdToCatId { get; set; }

        public int CategoryId { get; set; }

        public int ProductId { get; set; }
    }
}