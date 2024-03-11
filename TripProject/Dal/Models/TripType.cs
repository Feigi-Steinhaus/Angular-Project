using System;
using System.Collections.Generic;

namespace Dal;

public partial class TripType
{
    public int TypeCode { get; set; }

    public string? TypeName { get; set; }

    public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();
}
