using System.Net;
using CibPaySdk.Core.Models;

namespace CibPaySdk.Core.Exceptions;

public class CibPayApiException(CibPayErrorResponse errorResponse, HttpStatusCode httpStatusCode)
    : Exception(errorResponse.FailureMessage)
{
    public string FailureMessage { get; } = errorResponse.FailureMessage;
    public string FailureType { get; } = errorResponse.FailureType;
    public string? OrderId { get; } = errorResponse.OrderId;
    public List<FailureDetail> ValidationErrors { get; } = errorResponse.Errors;
    public HttpStatusCode StatusCode { get; } = httpStatusCode;
}