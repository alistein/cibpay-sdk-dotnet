using CibPay.Http.Configuration;
using CibPay.Http.Handlers;
using CibPaySdk.Core.Interfaces;
using CibPaySdk.Core.Models;

namespace CibPay.Http.Clients;

public class OrderClient : IOrderClient
{
    private readonly RequestHandler _requestHandler;

    public OrderClient(RequestHandler requestHandler)
    {
        _requestHandler = requestHandler;
    }

    public async Task<OrderProviderResponse> CreateAsync(CreateOrderRequest request)
    {
        var result = await _requestHandler.SendRequestAsync<OrderProviderResponse>(HttpMethod.Post, ApiEndpoints.Create, request);
        return result ?? new OrderProviderResponse();
    }
}