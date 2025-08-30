namespace CibPaySdk.Core.Types;

public enum OrderExpansions
{
    Card,
    Client,
    Location,
    CustomFields,
    Issuer,
    Secure3D,
    OperationsCashflow
}

public static class OrderExpansionsExtensions
{
    public static string ToQueryParam(this OrderExpansions orderExpansion) =>
        orderExpansion switch
        {
            OrderExpansions.Card => "card",
            OrderExpansions.Client => "client",
            OrderExpansions.Location => "location",
            OrderExpansions.CustomFields => "custom_fields",
            OrderExpansions.Issuer => "issued",
            OrderExpansions.Secure3D => "secure3d",
            OrderExpansions.OperationsCashflow => "operations.cashflow",
            _ => ""
        };
}
