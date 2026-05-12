using System;
using System.Collections.Generic;

namespace ShoesMarketApi;

public partial class PickUpPoint
{
    public int IdPickUpPoints { get; set; }

    public int Index { get; set; }

    public string City { get; set; } = null!;

    public string StreetAndHouse { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
