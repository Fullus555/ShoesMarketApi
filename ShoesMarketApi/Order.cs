using System;
using System.Collections.Generic;

namespace ShoesMarketApi;

public partial class Order
{
    public int OrderNumber { get; set; }

    public DateTime OrderDate { get; set; }

    public DateTime DeliveryDate { get; set; }

    public int PickUpPointFk { get; set; }

    public int FullNameOfEmployeeFk { get; set; }

    public int ReceiptCode { get; set; }

    public int OrderStatusFk { get; set; }

    public virtual Employee FullNameOfEmployeeFkNavigation { get; set; } = null!;

    public virtual ICollection<ItemQuantity> ItemQuantities { get; set; } = new List<ItemQuantity>();

    public virtual OrderStatus OrderStatusFkNavigation { get; set; } = null!;

    public virtual PickUpPoint PickUpPointFkNavigation { get; set; } = null!;
}
