namespace CibPay.Http.Configuration;

public static class ApiEndpoints
{
    public const string Ping = "ping";
    public const string Create = "orders/create";
    public const string Get = "orders/{id}?expand={expand}";
}
