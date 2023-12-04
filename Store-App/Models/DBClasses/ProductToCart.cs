using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Store_App.Models.DBClasses
{
    public partial class ProductToCart
    {
        [Required]
        public int ProdToCartId { get; set; }

        public int? CartId { get; set; }

        public int? ProductId { get; set; }

        public virtual Cart? Cart { get; set; }

        public virtual Product? Product { get; set; }
    }
}