using System;
using System.Collections.Generic;

namespace Store_App.Models.DBClasses;

public partial class Cart
{
    public int CartId { get; set; }

    public double Total { get; set; }

    public virtual ICollection<Person> People { get; set; } = new List<Person>();

    public virtual ICollection<ProductToCart> ProductToCarts { get; set; } = new List<ProductToCart>();
}
