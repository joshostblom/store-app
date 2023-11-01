namespace Store_App.Models.Classes
{
    public class DetailedProduct : Product
    {
        public string Sku { get; set;}
        public double Rating { get; set;}
        public string Description { get; set;}
        public string ManufacturerInformation { get; set;}
        public double Height { get; set;}
        public double Width { get; set;}
        public double Length { get; set;}
        public double Weight { get; set;}

        public DetailedProduct(string name, double price, string imageUrl, string saleInfo, string sku, double rating, string description, string manufacturerInfo, double height,
            double width, double length, double weight) : base(name, price, imageUrl, saleInfo)
        {
            this.Sku = sku;
            this.Rating = rating;
            this.Description = description;
            this.ManufacturerInformation = manufacturerInfo;
            this.Height = height;
            this.Width = width;
            this.Length = length;
            this.Weight = weight;
        }
    }
}
