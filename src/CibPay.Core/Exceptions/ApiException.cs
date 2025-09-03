using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using CibPaySdk.Core.Models;

namespace CibPaySdk.Core.Exceptions;

[Serializable]
public class CibPayApiException(CibPayErrorResponse errorResponse, HttpStatusCode httpStatusCode)
    : Exception(errorResponse.FailureMessage, new Exception() { })
{
    public string FailureMessage { get; } = errorResponse.FailureMessage;
    public string FailureType { get; } = errorResponse.FailureType;
    public string? OrderId { get; } = errorResponse.OrderId;
    public List<FailureDetail> ValidationErrors { get; } = errorResponse.Errors;
    public HttpStatusCode StatusCode { get; } = httpStatusCode;

    /// <summary>
    /// Returns the complete error response as JSON string
    /// </summary>
    public string ToJson()
    {
        var errorInfo = new
        {
            FailureMessage,
            FailureType,
            OrderId,
            StatusCode = StatusCode.ToString(),
            ValidationErrors
        };

        return JsonSerializer.Serialize(errorInfo, new JsonSerializerOptions
        {
            WriteIndented = false,            
        });
    }
}