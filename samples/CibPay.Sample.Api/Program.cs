using CibPay.Http.Configuration;
using CibPay.Sdk;
using CibPaySdk.Core.Types;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<CibPayClient>(_ => CibPayClientFactory.Create(new SdkOptions
{
	Username = "cibpay",
	Password = "gxIO8aH6N3j13FREp2",
	BaseUrl = "https://api-preprod.cibpay.co",
	ReturnUrl = "https://cloud4.ninco.org:2053/api/payment/info/",
	PaymentUrl = "https://checkout-preprod.cibpay.co/pay/",
	CertificatePassword = "3/tYTB7OSPV4",
	CertificatePath = "Certificate/api-cibpay.p12"
}));

var app = builder.Build();

app.MapGet("/cibpay-orders", async (CibPayClient client, [FromQuery] String orderId) =>
{
	return Results.Ok(await client.Orders.GetAsync(orderId, OrderExpansions.Issuer));
});

app.Run();
