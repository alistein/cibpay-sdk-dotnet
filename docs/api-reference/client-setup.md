# Client Setup

Initialize and configure the CibPay client.

## Basic Client Creation

```csharp
using CibPay.Http.Configuration;
using CibPay.Sdk;

var options = new SdkOptions
{
    Username = "your-username",
    Password = "your-password",
    BaseUrl = "https://api-preprod.cibpay.co",
    CertificatePath = "Certificate/api-cibpay.p12",
    CertificatePassword = "your-cert-password"
};

var client = CibPayClientFactory.Create(options);
```

## Thread Safety

The client is thread-safe and should be used as a singleton:

```csharp
// ✅ Correct - Single instance for entire application
private static readonly CibPayClient _client = CibPayClientFactory.Create(options);

// ✅ Safe for concurrent use
await _client.Orders.CreateAsync(request);
```

## ASP.NET Core Integration

```csharp
// Program.cs
builder.Services.AddSingleton<CibPayClient>(provider =>
{
    var config = provider.GetRequiredService<IConfiguration>();
    var options = new SdkOptions
    {
        Username = config["CibPay:Username"]!,
        Password = config["CibPay:Password"]!,
        BaseUrl = config["CibPay:BaseUrl"]!,
        CertificatePath = config["CibPay:CertificatePath"]!,
        CertificatePassword = config["CibPay:CertificatePassword"]!
    };
    return CibPayClientFactory.Create(options);
});
```

### Controller Usage

```csharp
[ApiController]
public class PaymentsController : ControllerBase
{
    private readonly CibPayClient _client;
    
    public PaymentsController(CibPayClient client)
    {
        _client = client;
    }
    
    [HttpPost("orders")]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
        var response = await _client.Orders.CreateAsync(request);
        return Ok(response);
    }
}
```

## Error Handling

```csharp
try
{
    var client = CibPayClientFactory.Create(options);
}
catch (ArgumentException ex)
{
    // Configuration validation failed
}
catch (FileNotFoundException ex)
{
    // Certificate file not found
}
```



