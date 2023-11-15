namespace Store_App.Models.DomainClasses
{
    public class Sale
    {
        public int SaleId { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal PercentOff { get; set; }
    }
}
