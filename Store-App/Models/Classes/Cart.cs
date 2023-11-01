using Store_App.Models.Interfaces;

namespace Store_App.Models.Classes
{
    public class Cart : ICart
    {
        public List<Product>? Products { get; set; }
        public double TotalPrice { get; set; }

        public Cart()
        {
            this.Products = null;
            this.TotalPrice = 0.0;
        }
    }
}
