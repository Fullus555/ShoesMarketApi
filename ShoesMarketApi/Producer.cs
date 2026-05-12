using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ShoesMarketApi;

public partial class Producer
{
    public int IdProducer { get; set; }

    public string ProducerName { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
