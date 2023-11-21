using System;
using System.Collections.Generic;

namespace Store_App.Models.DBClasses
{
    public partial class ProductToCategory
    {
        public int ProdToCatId { get; set; }

        public int CategoryId { get; set; }

        public int? ProductId { get; set; }
    }
}