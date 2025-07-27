using System.Text.Json.Serialization;

namespace CibPaySdk.Core.Models;

public class OrderProviderResponse
{
    [JsonPropertyName("orders")]
    public List<OrderResponse> Orders { get; set; } = [];
    public string? PaymentUrl { get; set; }
    public string? RawResult { get; set; }
}
