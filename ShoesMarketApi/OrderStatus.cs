using System;
using System.Collections.Generic;

namespace ShoesMarketApi;

public partial class OrderStatus
{
    public int IdOrderStatus { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
