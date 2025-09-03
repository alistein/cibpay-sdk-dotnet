using CibPay.Http.Configuration;
using CibPay.Sdk;
using CibPaySdk.Core.Exceptions;
using CibPaySdk.Core.Models;
using CibPaySdk.Core.Types;
using Microsoft.AspNetCore.Mvc;

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

app.MapPost("/orders", async ([FromServices] CibPayClient client) =>
{
	try
	{

		CreateOrderRequest request = new()
		{
			Amount = 20,
			Currency = "AZN",
			Options = new Options
			{
				Language = "en",
				ReturnUrl = "https://senetgah.com",
				ExpirationTimeout = "15m",
			}

		};

		var response = await client.Orders.CreateAsync(request);
		return Results.Ok(response);
	}
	catch (CibPayApiException ex)
	{
		return Results.BadRequest(new { error = ex.ValidationErrors.First().Message });
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
