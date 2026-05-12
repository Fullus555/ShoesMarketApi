using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ShoesMarketApi;

public partial class Supplier
{
    public int IdSupplier { get; set; }

    public string SupplierName { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
