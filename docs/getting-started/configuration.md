# Configuration Guide

Complete reference for configuring the CibPay .NET SDK.

## Basic Configuration

```csharp
using CibPay.Http.Configuration;

var options = new SdkOptions
{
    // Required
    Username = "your-username",
    Password = "your-password",
    BaseUrl = "https://api-preprod.cibpay.co",
    CertificatePath = "Certificate/api-cibpay.p12",
    CertificatePassword = "your-certificate-password",
  
};
```

## Required Parameters

### Authentication Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `Username` | `string` | Your CibPay merchant username |
| `Password` | `string` | Your CibPay merchant password |
| `CertificatePath` | `string` | Path to your .p12 certificate file |
| `CertificatePassword` | `string` | Password for the certificate file |

### Service Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `BaseUrl` | `string` | CibPay API base URL |

**Environment URLs:**
- **Staging/Testing**: `https://api-preprod.cibpay.co`
- **Production**: Contact CibPay for production URL



## ASP.NET Core Integration

### appsettings.json
```json
{
  "CibPay": {
    "Username": "your-username",
    "Password": "your-password",
    "BaseUrl": "https://api-preprod.cibpay.co",
    "CertificatePath": "Certificate/api-cibpay.p12",
    "CertificatePassword": "your-cert-password",
    "ReturnUrl": "https://yoursite.com/payment/return"
  }
}
```

### Service Registration
```csharp
builder.Services.AddSingleton<CibPayClient>(provider =>
{
    var config = provider.GetRequiredService<IConfiguration>();
    var options = new SdkOptions
    {
        Username = config["CibPay:Username"]!,
        Password = config["CibPay:Password"]!,
        BaseUrl = config["CibPay:BaseUrl"]!,
        CertificatePath = config["CibPay:CertificatePath"]!,
        CertificatePassword = config["CibPay:CertificatePassword"]!,
    };
    return CibPayClientFactory.Create(options);
});
```

## Certificate Management

### Project Setup
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <ItemGroup>
    <None Update="Certificate\*.p12">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>
  </ItemGroup>
</Project>
```

### Security Best Practices
- Never commit certificates to version control
- Use environment variables for sensitive data
- Store certificates securely in production
- Monitor certificate expiration dates

## Environment Variables

For production, use environment variables:

```csharp
var options = new SdkOptions
{
    Username = Environment.GetEnvironmentVariable("CIBPAY_USERNAME")!,
    Password = Environment.GetEnvironmentVariable("CIBPAY_PASSWORD")!,
    BaseUrl = Environment.GetEnvironmentVariable("CIBPAY_BASE_URL")!,
    CertificatePath = Environment.GetEnvironmentVariable("CIBPAY_CERT_PATH")!,
    CertificatePassword = Environment.GetEnvironmentVariable("CIBPAY_CERT_PASSWORD")!
};
```

## Common Errors

| Error | Solution |
|-------|----------|
| `Username cannot be null or empty` | Set `Username` property |
| `Certificate file not found` | Check file path and existence |
| `BaseUrl must be a valid absolute URI` | Use complete URL with protocol |


