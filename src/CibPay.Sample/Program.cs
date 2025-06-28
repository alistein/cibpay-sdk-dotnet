// See https://aka.ms/new-console-template for more information

using CibPay.Http.Configuration;
using CibPay.Sdk;
using CibPaySdk.Core.Models;
using Dumpify;

Console.WriteLine("Hello, World!");

var cibPayClient = CibPayClientFactory.Create(new SdkOptions
{
    Username = "cibpay",
    Password = "gxIO8aH6N3j13FREp2",
    BaseUrl = "https://api-preprod.cibpay.co",
    ReturnUrl = "https://cloud4.ninco.org:2053/api/payment/info/",
    PaymentUrl = "https://checkout-preprod.cibpay.co/pay/",
    CertificatePassword = "3/tYTB7OSPV4",
    CertificatePath = "Certificate/api-cibpay.p12"
});

var creationResponse = await cibPayClient.Orders.CreateAsync(new CreateOrderRequest{Amount = 100});

creationResponse.Dump();