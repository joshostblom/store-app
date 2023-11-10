using System;
using System.Collections.Generic;

namespace Store_App.Models.DBClasses
{
    public partial class DetailedProduct
    {
        public int DetailedProductId { get; set; }

        public int ProductId { get; set; }

        public string Sku { get; set; } = null!;

        public double Rating { get; set; }

        public string? Description { get; set; }

        public string? ManufacturerInformation { get; set; }

        public double? ProdHeight { get; set; }

        public double? ProdWidth { get; set; }

        public double? ProdLength { get; set; }

        public double? ProdWeight { get; set; }

        public virtual Product Product { get; set; } = null!;
    }
}