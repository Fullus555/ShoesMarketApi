using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ShoesMarketApi;

public partial class ProductCategory
{
    public int IdProductCategory { get; set; }

    public string ProductCategory1 { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
