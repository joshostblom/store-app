using System;
using System.Collections.Generic;

namespace Store_App.Models.DBClasses
{
    public partial class Sale
    {
        public int SaleId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal PercentOff { get; set; }
    }
}