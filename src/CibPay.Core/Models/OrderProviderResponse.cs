using System.Text.Json.Serialization;

namespace CibPaySdk.Core.Models;

public class OrderProviderResponse
{
    [JsonPropertyName("orders")]
    public List<OrderResponse> Orders { get; set; } = [];
    public string? PaymentUrl { get; set; }

    /// <summary>
    /// The pure response json coming from the cibpay api. In case of using custom serialization you can parse this property
    /// </summary>
    public string? RawResult { get; set; }
}
