using CibPaySdk.Core.Models;

namespace CibPaySdk.Core.Interfaces;

public interface IOrderClient
{
    public Task<OrderProviderResponse> CreateAsync(CreateOrderRequest request);
}