# Authentication & Security

CibPay SDK uses certificate-based authentication combined with basic HTTP authentication.

## Certificate Setup

1. **Get Certificate**: Contact CibPay to receive your `.p12` certificate file and password
2. **Place in Project**: Add certificate to `Certificate/` folder
3. **Configure Project**: Set certificate to copy to output directory

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <ItemGroup>
    <None Update="Certificate\api-cibpay.p12">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>
  </ItemGroup>
</Project>
```

## Authentication Configuration

```csharp
var options = new SdkOptions
{
    Username = "your-username",
    Password = "your-password",
    CertificatePath = "Certificate/api-cibpay.p12",
    CertificatePassword = "your-certificate-password",
    BaseUrl = "https://api-preprod.cibpay.co"
};

var client = CibPayClientFactory.Create(options);
```

## Security Best Practices

- Never hardcode credentials in source code
- Use environment variables for sensitive data
- Store certificates securely in production
- Monitor certificate expiration dates
- Use HTTPS URLs only

## Common Issues

| Issue | Solution |
|-------|----------|
| Certificate file not found | Check file path and `CopyToOutputDirectory` setting |
| Authentication failed | Verify username, password, and certificate password |
| Certificate expired | Contact CibPay for renewed certificate |


