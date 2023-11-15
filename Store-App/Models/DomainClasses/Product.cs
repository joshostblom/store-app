namespace Store_App.Models.DomainClasses
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;

        public double? Price { get; set; }

        public string? ImageUrl { get; set; }

        public Sale? Sale { get; set; }
    }
}
