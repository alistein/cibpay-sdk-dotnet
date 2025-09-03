using System.Net;
using System.Text.Json.Serialization;

namespace CibPaySdk.Core.Models;

/// <summary>
/// Represents an error response from the CibPay API containing failure information and details.
/// </summary>
public class CibPayErrorResponse
{
    /// <summary>
    /// Gets the human-readable failure message describing what went wrong.
    /// </summary>
    /// <value>The failure message. Defaults to "Unexpected error" if not specified.</value>
    [JsonPropertyName("failure_message")]
    public string FailureMessage { get; init; } = "Unexpected error";

    /// <summary>
    /// Gets the type of failure that occurred.
    /// </summary>
    /// <value>
    /// The failure type. Defaults to "error" if not specified.
    /// Possible values include:
    /// <list type="bullet">
    /// <item><description>"declined" - The acquirer rejected the transaction</description></item>
    /// <item><description>"fraud" - The transaction was rejected by the gateway's anti-fraud system</description></item>
    /// <item><description>"rejected" - The transaction was rejected by the gateway</description></item>
    /// <item><description>"error" - Rejection due to an error on the acquirer or gateway side</description></item>
    /// <item><description>"validation" - The request was not processed because it has not been validated</description></item>
    /// </list>
    /// </value>
    [JsonPropertyName("failure_type")]
    public string FailureType { get; init; } = "error";

    /// <summary>
    /// Gets the order ID associated with the failed request, if applicable.
    /// </summary>
    /// <value>The order ID, or null if the error is not related to a specific order.</value>
    [JsonPropertyName("order_id")]
    public string? OrderId { get; init; }

    /// <summary>
    /// Gets the collection of detailed error information.
    /// </summary>
    /// <value>A list of <see cref="FailureDetail"/> objects containing specific error details.</value>
    [JsonPropertyName("errors")]
    public List<FailureDetail> Errors { get; init; } = [];
}

/// <summary>
/// Represents detailed information about a specific failure or validation error.
/// </summary>
public class FailureDetail
{
    /// <summary>
    /// Gets the name of the attribute or field that caused the error.
    /// </summary>
    /// <value>The attribute name, or null if not applicable.</value>
    [JsonPropertyName("attribute")]
    public string? Attribute { get; init; }

    /// <summary>
    /// Gets the error message for this specific failure detail.
    /// </summary>
    /// <value>The error message, or null if not provided.</value>
    [JsonPropertyName("message")]
    public string? Message { get; init; }

    /// <summary>
    /// Gets the URI or reference related to this error, if applicable.
    /// </summary>
    /// <value>The URI reference, or null if not applicable.</value>
    [JsonPropertyName("uri")]
    public string? Uri { get; init; }

    /// <summary>
    /// Gets additional details about the error.
    /// </summary>
    /// <value>A list of additional error details, or null if not provided.</value>
    [JsonPropertyName("details")]
    public List<object>? Details { get; init; }
}