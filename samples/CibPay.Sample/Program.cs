using CibPay.Http.Configuration;
using CibPay.Sdk;
using CibPaySdk.Core.Types;
using Dumpify;
var cibPayClient = CibPayClientFactory.Create(
    new SdkOptions
    {
        Username = "test_username_123",
        Password = "test_password_456",
        BaseUrl = "https://sandbox-api.example.com",
        ReturnUrl = "https://your-test-site.com/callback",
        PaymentUrl = "https://sandbox-checkout.example.com/pay",
        CertificatePassword = "test_cert_pass_789",
        CertificatePath = "Certificate/test-certificate.p12"
    }
);

// var creationResponse = await cibPayClient.Orders.CreateAsync(
//     new CreateOrderRequest { Amount = 100 }
// );

// creationResponse.Dump();

(await cibPayClient.Orders.GetAsync("94856904049551304", OrderExpansions.Issuer)).Dump();