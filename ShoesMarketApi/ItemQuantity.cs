using System;
using System.Collections.Generic;

namespace ShoesMarketApi;

public partial class ItemQuantity
{
    public int ItemQuantityId { get; set; }

    public int OrderNumberFk { get; set; }

    public string ArticleNumberFk { get; set; } = null!;

    public int Quantity { get; set; }

    public virtual Product ArticleNumberFkNavigation { get; set; } = null!;

    public virtual Order OrderNumberFkNavigation { get; set; } = null!;
}
