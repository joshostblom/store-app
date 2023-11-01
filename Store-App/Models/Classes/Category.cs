using Store_App.Models.Interfaces;

namespace Store_App.Models.Classes
{
    public class Category : ICategory
    {
        public string Name { get; set; }
        public List<Product> Products { get; set; }
        public string SaleInfo { get; set; }

        public Category(string name, List<Product> products, string saleInfo)
        {
            this.Name = name;
            this.Products = products;
            this.SaleInfo = saleInfo;
        }
    }
}
