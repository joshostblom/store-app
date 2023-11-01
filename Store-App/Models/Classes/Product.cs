using Store_App.Models.Interfaces;

namespace Store_App.Models.Classes
{
    public class Product : IProduct
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string ImageURL { get; set; }
        public string? SaleInfo { get; set; }

        public Product(string name, double price, string imageURL, string? saleInfo)
        {
            this.Name = name;
            this.Price = price;
            this.ImageURL = imageURL;
            this.SaleInfo = saleInfo;
        }

    }
}
