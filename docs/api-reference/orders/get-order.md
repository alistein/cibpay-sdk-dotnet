# Get Order

Retrieve order information and status.

## Method

```csharp
Task<OrderProviderResponse> GetAsync(string orderId, OrderExpansions orderExpansion = OrderExpansions.Card)
```

## Basic Usage

```csharp
var response = await client.Orders.GetAsync("94856904049551304");

if (response.Success)
{
    var order = response.Data;
    Console.WriteLine($"Status: {order.Status}");
    Console.WriteLine($"Amount: {order.Amount} {order.Currency}");
}
```

## Order Expansions

Available expansion options:

- `Card` - Card information (default)
- `Client` - Customer information  
- `Issuer` - Bank/issuer details
- `Location` - Geographic data

```csharp
var response = await client.Orders.GetAsync(orderId, OrderExpansions.Issuer);
```

## Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `orderId` | `string` | ✅ | Order identifier |
| `orderExpansion` | `OrderExpansions` | ❌ | Additional data to include |