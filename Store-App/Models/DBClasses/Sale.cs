using System.ComponentModel.DataAnnotations;

namespace Store_App.Models.DBClasses
{
    public partial class Sale
    {
        [Required]
        public int SaleId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public decimal PercentOff { get; set; }
    }
}