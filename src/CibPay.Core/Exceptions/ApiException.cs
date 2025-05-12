using System.Net;

namespace CibPaySdk.Core.Exceptions;

public class CibPayApiException(string failureMessage, string failureType, HttpStatusCode httpStatusCode, string? orderId = null)
    : Exception(failureMessage)
{
    public string FailureMessage { get; } = failureMessage;
    public string FailureType { get; } = failureType;
    public string? OrderId { get; } = orderId;
    public HttpStatusCode StatusCode { get; set; }
}