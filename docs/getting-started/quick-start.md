# Quick Start Guide

This guide will walk you through creating your first payment order using the CibPay .NET SDK in just a few minutes.

## Prerequisites

Before you begin, ensure you have:

- ✅ .NET 8.0 or later installed
- ✅ CibPay merchant credentials (username and password)
- ✅ SSL certificate file (.p12) provided by CibPay
- ✅ Certificate password from CibPay

!!! info "Getting the credentials"
    You should contact with CibPay in order to have these credentials

## Step 1: Install the SDK

Install the CibPay SDK via NuGet Package Manager:

```shell
dotnet add package CibPay.Sdk
```

Or using Package Manager Console in Visual Studio:

```shell
Install-Package CibPay.Sdk
```

## Step 2: Set Up Your Certificate

1. **Place your certificate file** in your project (e.g., `Certificate/api-cibpay.p12`)

2. **Configure the certificate in your project file** to be copied to output directory:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <!-- Other project settings -->
  
  <ItemGroup>
    <None Update="Certificate\api-cibpay.p12">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>
  </ItemGroup>
</Project>
```

!!! warning "Security Note"
    Never commit certificate files to version control. Use environment-specific configurations for production deployments.

## Step 3: Console Application Example

Create a simple console application to test the SDK:

```csharp
using CibPay.Http.Configuration;
using CibPay.Sdk;
using CibPaySdk.Core.Models;
using CibPaySdk.Core.Types;

// Configure the SDK with your credentials
var options = new SdkOptions
{
    Username = "your-username",
    Password = "your-password",
    BaseUrl = "https://api-preprod.cibpay.co", // Use preprod for testing
    CertificatePath = "Certificate/api-cibpay.p12",
    CertificatePassword = "your-certificate-password",
};

try
{
    // Create the client
    var client = CibPayClientFactory.Create(options);
    
    // Create a new order
    var createRequest = new CreateOrderRequest
    {
        Amount = 10.50m, // 10.50 AZN
        Currency = "AZN"
    };
    
    var orderResponse = await client.Orders.CreateAsync(createRequest);
    
    Console.WriteLine("✅ Order created successfully!");
    Console.WriteLine($"Order ID: {orderResponse.Data.Id}");
    Console.WriteLine($"Status: {orderResponse.Data.Status}");
    Console.WriteLine($"Amount: {orderResponse.Data.Amount} {orderResponse.Data.Currency}");
    
    // Retrieve the order details
    var retrievedOrder = await client.Orders.GetAsync(
        orderResponse.Data.Id, 
        OrderExpansions.Card
    );
    
    Console.WriteLine("\n📋 Order Details:");
    Console.WriteLine($"Created: {retrievedOrder.Data.Created}");
    Console.WriteLine($"Updated: {retrievedOrder.Data.Updated}");
    Console.WriteLine($"Status: {retrievedOrder.Data.Status}");
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Error: {ex.Message}");
}
```

## Step 4: ASP.NET Core Web API Example

For web applications, configure the SDK with dependency injection:

### Configure Services (Program.cs)

```csharp
using CibPay.Http.Configuration;
using CibPay.Sdk;

var builder = WebApplication.CreateBuilder(args);

// Add SDK as singleton (thread-safe)
builder.Services.AddSingleton<CibPayClient>(serviceProvider =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    
    var options = new SdkOptions
    {
        Username = configuration["CibPay:Username"]!,
        Password = configuration["CibPay:Password"]!,
        BaseUrl = configuration["CibPay:BaseUrl"]!,
        CertificatePath = configuration["CibPay:CertificatePath"]!,
        CertificatePassword = configuration["CibPay:CertificatePassword"]!,
    };
    
    return CibPayClientFactory.Create(options);
});

var app = builder.Build();

// Configure endpoints
app.MapGet("/", () => "CibPay SDK Web API");

app.MapPost("/orders", async (CibPayClient client, CreateOrderRequest request) =>
{
    try
    {
        var response = await client.Orders.CreateAsync(request);
        return Results.Ok(response);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
});

app.MapGet("/orders/{orderId}", async (CibPayClient client, string orderId) =>
{
    try
    {
        var response = await client.Orders.GetAsync(orderId, OrderExpansions.Card);
        return Results.Ok(response);
    }
    catch (Exception ex)
    {
        return Results.NotFound(new { error = ex.Message });
    }
});

app.Run();
```

### Configuration (appsettings.json)

```json
{
  "CibPay": {
    "Username": "your-username",
    "Password": "your-password",
    "BaseUrl": "https://api-preprod.cibpay.co",
    "CertificatePath": "Certificate/api-cibpay.p12",
    "CertificatePassword": "your-certificate-password",
  }
}
```


## Success Indicators

You've successfully integrated CibPay SDK when you see:

✅ **Order Creation**: Orders are created with status "New"  
✅ **No Authentication Errors**: Certificate and credentials are working  
✅ **Proper Response Format**: Structured JSON responses with order data  
✅ **Order Retrieval**: Can fetch order details by ID  

## Common First-Time Issues

| Issue | Solution |
|-------|----------|
| Certificate not found | Check file path and ensure `CopyToOutputDirectory` is set |
| Authentication failed | Verify username, password, and certificate password |
| Network errors | Confirm BaseUrl and internet connectivity |
| SSL errors | Ensure certificate is valid and not expired |

## Next Steps

Now that you have a working integration:

1. **[Learn about Configuration](configuration.md)** - Understand all configuration options
2. **[Explore Core Concepts](../core-concepts/authentication.md)** - Deep dive into authentication and order lifecycle
3. **[API Reference](../api-reference/client-setup.md)** - Complete API documentation
4. **Production Setup** - Configure for production environment

## Full Working Examples

The SDK includes complete working examples in the repository:

- **Console Application**: `samples/CibPay.Sample/`
- **Web API**: `samples/CibPay.Sample.Api/`

Clone the repository to explore these examples:

```bash
git clone https://github.com/alistein/cibpay-sdk-dotnet.git
cd cibpay-sdk-dotnet/samples
```

!!! tip "Pro Tip"
    Start with the console sample to understand the basic flow, then move to the web API sample for production-ready patterns.
