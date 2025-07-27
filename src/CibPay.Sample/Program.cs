using CibPay.Http.Configuration;
using CibPay.Sdk;
using CibPaySdk.Core.Models;
using CibPaySdk.Core.Types;
using Dumpify;

var cibPayClient = CibPayClientFactory.Create(
    new SdkOptions
    {
        Username = "cibpay",
        Password = "gxIO8aH6N3j13FREp2",
        BaseUrl = "https://api-preprod.cibpay.co",
        ReturnUrl = "https://cloud4.ninco.org:2053/api/payment/info/",
        PaymentUrl = "https://checkout-preprod.cibpay.co/pay/",
        CertificatePassword = "3/tYTB7OSPV4",
        CertificatePath = "Certificate/api-cibpay.p12"
    }
);

// var creationResponse = await cibPayClient.Orders.CreateAsync(
//     new CreateOrderRequest { Amount = 100 }
// );

(await cibPayClient.Orders.GetAsync("94856904049551304", OrderExpansions.Client)).Dump();
