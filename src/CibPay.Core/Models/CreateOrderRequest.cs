using System.Text.Json.Serialization;

namespace CibPaySdk.Core.Models;

public class CreateOrderRequest
{
    [JsonPropertyName("amount")]
    public required decimal? Amount { get; set; }

    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    [JsonPropertyName("extra_fields")]
    public ExtraFields? ExtraFields { get; set; }

    [JsonPropertyName("merchant_order_id")]
    public string? MerchantOrderId { get; set; }

    [JsonPropertyName("options")]
    public Options? Options { get; set; }

    [JsonPropertyName("custom_fields")]
    public CustomFields? CustomFields { get; set; }

    [JsonPropertyName("client")]
    public RequestedClient? Client { get; set; }

}

public class ExtraFields
{
    [JsonPropertyName("invoice_id")]
    public string? InvoiceId { get; set; }

    [JsonPropertyName("oneclick")]
    public OneClick? OneClick { get; set; }
}

public class OneClick
{
    [JsonPropertyName("customer_id")]
    public string? CustomerId { get; set; }

    [JsonPropertyName("prechecked")]
    public int? Prechecked { get; set; }
}

public class Options
{
    [JsonPropertyName("auto_charge")]
    public bool? AutoCharge { get; set; }

    [JsonPropertyName("expiration_timeout")]
    public string? ExpirationTimeout { get; set; }

    [JsonPropertyName("force3d")]
    public int? Force3d { get; set; }

    [JsonPropertyName("language")]
    public string? Language { get; set; }

    [JsonPropertyName("return_url")]
    public string? ReturnUrl { get; set; }

    [JsonPropertyName("terminal")]
    public string? Terminal { get; set; }

    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("recurring")]
    public int? Recurring { get; set; }
}

public class CustomFields
{
    [JsonPropertyName("region_code")]
    public int? RegionCode { get; set; }

    [JsonPropertyName("home_phone_country_code")]
    public string? HomePhoneCountryCode { get; set; }

    [JsonPropertyName("home_phone_subscriber")]
    public string? HomePhoneSubscriber { get; set; }

    [JsonPropertyName("mobile_phone_country_code")]
    public string? MobilePhoneCountryCode { get; set; }

    [JsonPropertyName("mobile_phone_subscriber")]
    public string? MobilePhoneSubscriber { get; set; }

    [JsonPropertyName("work_phone_country_code")]
    public string? WorkPhoneCountryCode { get; set; }

    [JsonPropertyName("work_phone_subscriber")]
    public string? WorkPhoneSubscriber { get; set; }
}

public class RequestedClient
{
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("city")]
    public string? City { get; set; }

    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("address")]
    public string? Address { get; set; }

    [JsonPropertyName("zip")]
    public string? Zip { get; set; }
}