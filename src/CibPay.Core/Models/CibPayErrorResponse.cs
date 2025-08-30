using System.Net;
using System.Text.Json.Serialization;

namespace CibPaySdk.Core.Models;

public class CibPayErrorResponse
{
    [JsonPropertyName("failure_message")]
    public string FailureMessage { get; set; } = "Unexpected error";

    [JsonPropertyName("failure_type")]
    public string FailureType { get; set; } = "error";

    [JsonPropertyName("order_id")]
    public string? OrderId { get; set; }

    [JsonPropertyName("errors")]
    public List<FailureDetail> Errors { get; set; } = [];
}

public class FailureDetail
{
    [JsonPropertyName("attribute")]
    public string? Attribute { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("uri")]
    public string? Uri { get; set; }

    [JsonPropertyName("details")]
    public List<object>? Details { get; set; }
}