using Store_App.Models.Interfaces;

namespace Store_App.Models.Classes
{
    public class SaleInfo : ISaleInfo
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double PercentOff { get; set; }

        public SaleInfo(DateTime startDate, DateTime endDate, double percentOff)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.PercentOff = percentOff;
        }
    }
}
