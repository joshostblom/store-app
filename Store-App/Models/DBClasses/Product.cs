namespace Store_App.Models.DBClasses
{
    public partial class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public double? Price { get; set; }

        public string? ImageUrl { get; set; }

        public int? SaleId { get; set; }

        public string Sku { get; set; } = null!;

        public double Rating { get; set; }

        public string? Descript { get; set; }

        public string? ManufacturerInformation { get; set; }

        public double? ProdHeight { get; set; }

        public double? ProdWidth { get; set; }

        public double? ProdLength { get; set; }

        public double? ProdWeight { get; set; }
    }
}