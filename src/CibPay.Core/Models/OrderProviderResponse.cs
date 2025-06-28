using System.Text.Json.Serialization;

namespace CibPaySdk.Core.Models;

public class OrderProviderResponse
{
    [JsonPropertyName("orders")] 
    public List<OrderResponse> Orders { get; set; } = [];
}
