# Create Order

Create a new payment order.

## Method

```csharp
Task<OrderProviderResponse> CreateAsync(CreateOrderRequest request)
```

## Basic Usage

```csharp
var request = new CreateOrderRequest
{
    Amount = 50.00m,
    Currency = "AZN"
};

var response = await client.Orders.CreateAsync(request);
Console.WriteLine($"Order ID: {response.Data.Id}");
```

## With Options

```csharp
var request = new CreateOrderRequest
{
    Amount = 199.99m,
    Currency = "AZN",
    MerchantOrderId = "ORD-12345",
    
    Options = new Options
    {
        ReturnUrl = "https://mystore.com/payment/return",
        Language = "en",
        AutoCharge = true,
        Force3d = 1,
        ExpirationTimeout = "30m",
        Terminal = "web",
        Country = "AZ"
    },
    
    Client = new RequestedClient
    {
        Email = "customer@example.com",
        Phone = "+994501234567"
    },
    
    ExtraFields = new ExtraFields
    {
        InvoiceId = "INV-2025-001",
        OneClick = new OneClick
        {
            CustomerId = "CUST-12345",
            Prechecked = 1
        }
    },
    
    CustomFields = new CustomFields
    {
        RegionCode = "AZ-BA" // Baku region
    }
};

var response = await client.Orders.CreateAsync(request);
```

## Parameters

| Property | Type | Required | Description |
|----------|------|----------|-------------|
| `Amount` | `decimal?` | ✅ | Payment amount |
| `Currency` | `string?` | ❌ | Currency code |
| `MerchantOrderId` | `string?` | ❌ | Your order reference |
| `Options` | `Options?` | ❌ | Payment options |



