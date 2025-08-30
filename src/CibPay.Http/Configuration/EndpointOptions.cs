namespace CibPay.Http.Configuration;

internal static class ApiEndpoints
{
    internal const string Ping = "ping";
    internal const string Create = "orders/create";
    internal const string Get = "orders/{id}?expand={expand}";
}
