namespace CibPaySdk.Core.Models;

using System.Text.Json.Serialization;
using CibPaySdk.Core.Types;

public class OrderResponse
{
    [JsonPropertyName("amount")]
    public string? Amount { get; set; }

    [JsonPropertyName("amount_charged")]
    public string? AmountCharged { get; set; }

    [JsonPropertyName("amount_refunded")]
    public string? AmountRefunded { get; set; }

    [JsonPropertyName("auth_code")]
    public string? AuthCode { get; set; }

    [JsonPropertyName("card")]
    public Card? Card { get; set; }

    [JsonPropertyName("client")]
    public Client? Client { get; set; }

    [JsonPropertyName("created")]
    public string? Created { get; set; }

    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("descriptor")]
    public string? Descriptor { get; set; }

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("issuer")]
    public Issuer? Issuer { get; set; }

    [JsonPropertyName("location")]
    public Location? Location { get; set; }

    [JsonPropertyName("merchant_order_id")]
    public string? MerchantOrderId { get; set; }

    [JsonPropertyName("operations")]
    public List<object>? Operations { get; set; }

    [JsonPropertyName("pan")]
    public string? Pan { get; set; }

    [JsonPropertyName("secure3d")]
    public Secure3D? Secure3D { get; set; }

    [JsonPropertyName("segment")]
    public string? Segment { get; set; }

    [JsonPropertyName("status")]
    public string? StatusText { get; set; }

    [JsonIgnore]
    public OrderStatuses? Status => OrderStatusUtils.ParseToOrderStatus(StatusText);

    [JsonPropertyName("updated")]
    public string? Updated { get; set; }
}

public class Card
{
    [JsonPropertyName("holder")]
    public string? Holder { get; set; }

    [JsonPropertyName("subtype")]
    public string? Subtype { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }
}

public class Client
{
    [JsonPropertyName("address")]
    public string? Address { get; set; }

    [JsonPropertyName("city")]
    public string? City { get; set; }

    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    [JsonPropertyName("state")]
    public string? State { get; set; }

    [JsonPropertyName("zip")]
    public string? Zip { get; set; }
}

public class Issuer
{
    [JsonPropertyName("bin")]
    public string? Bin { get; set; }

    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }
}

public class Location
{
    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("city")]
    public string? City { get; set; }

    [JsonPropertyName("region")]
    public string? Region { get; set; }

    [JsonPropertyName("ip")]
    public string? Ip { get; set; }
}

public class Secure3D
{
    [JsonPropertyName("reason")]
    public string? Reason { get; set; }

    [JsonPropertyName("scenario")]
    public string? Scenario { get; set; }

    [JsonPropertyName("authorization_status")]
    public string? AuthorizationStatus { get; set; }
}
