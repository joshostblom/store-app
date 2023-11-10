using System;
using System.Collections.Generic;

namespace Store_App.Models.DBClasses
{
    public partial class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public double? Price { get; set; }

        public byte[]? ImageUrl { get; set; }

        public int? SaleId { get; set; }

        public virtual ICollection<DetailedProduct> DetailedProducts { get; set; } = new List<DetailedProduct>();

        public virtual ICollection<ProductToCart> ProductToCarts { get; set; } = new List<ProductToCart>();

        public virtual ICollection<ProductToCategory> ProductToCategories { get; set; } = new List<ProductToCategory>();
    }
}