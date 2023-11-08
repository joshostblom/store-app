using System;
using System.Collections.Generic;

namespace Store_App.Models.DBClasses;

public partial class Person
{
    public int PersonId { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? AddressId { get; set; }

    public int? PaymentId { get; set; }

    public int? CartId { get; set; }

    public virtual Address? Address { get; set; }

    public virtual Cart? Cart { get; set; }

    public virtual Payment? Payment { get; set; }
}
