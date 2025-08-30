using CibPaySdk.Core.Models;
using CibPaySdk.Core.Types;

namespace CibPaySdk.Core.Interfaces;

public interface IOrderClient
{
    public Task<OrderProviderResponse> CreateAsync(CreateOrderRequest request);
    public Task<OrderProviderResponse> GetAsync(
        string orderId,
        OrderExpansions orderExpansion = OrderExpansions.Card
    );
}
