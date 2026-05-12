using System;
using System.Collections.Generic;

namespace ShoesMarketApi;

public partial class Product
{
    public int IdProduct { get; set; }

    public string ArticleNumber { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string UnitMeasurement { get; set; } = null!;

    public decimal Price { get; set; }

    public int SupplierFk { get; set; }

    public int ProducerFk { get; set; }

    public int ProductCategoryFk { get; set; }

    public int? CurrentDiscount { get; set; }

    public int QuantityInWarehouse { get; set; }

    public string ProductDescription { get; set; } = null!;

    public string? Photo { get; set; }

    public virtual ICollection<ItemQuantity> ItemQuantities { get; set; } = new List<ItemQuantity>();

    public virtual Supplier? SupplierFkNavigation { get; set; }

    public virtual Producer? ProducerFkNavigation { get; set; }

    public virtual ProductCategory? ProductCategoryFkNavigation { get; set; }
}
