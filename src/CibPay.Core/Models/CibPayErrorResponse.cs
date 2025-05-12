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
}