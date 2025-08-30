using CibPay.Http.Configuration;
using CibPay.Http.Handlers;
using CibPaySdk.Core.Interfaces;
using CibPaySdk.Core.Models;
using CibPaySdk.Core.Types;

namespace CibPay.Http.Clients;

internal class OrderClient : IOrderClient
{
    private readonly RequestHandler _requestHandler;

    public OrderClient(RequestHandler requestHandler)
    {
        _requestHandler = requestHandler;
    }

    public async Task<OrderProviderResponse> CreateAsync(CreateOrderRequest request)
    {
        var result = await _requestHandler.SendRequestAsync<OrderProviderResponse>(
            HttpMethod.Post,
            ApiEndpoints.Create,
            request
        );
        return result ?? new OrderProviderResponse();
    }

    public async Task<OrderProviderResponse> GetAsync(
        string orderId,
        OrderExpansions orderExpansion = OrderExpansions.Card
    )
    {
        var result = await _requestHandler.SendRequestAsync<OrderProviderResponse>(
            HttpMethod.Get,
            ApiEndpoints
                .Get.Replace("{id}", orderId)
                .Replace("{expand}", orderExpansion.ToQueryParam())
        );
        return result ?? new OrderProviderResponse();
    }
}
