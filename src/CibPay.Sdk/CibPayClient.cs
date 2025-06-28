using CibPay.Http.Clients;
using CibPay.Http.Configuration;
using CibPay.Http.Handlers;
using CibPaySdk.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CibPay.Sdk;

public class CibPayClient
{
    public CibPayClient(IOrderClient orderClient)
    {
        Orders = orderClient ?? throw new ArgumentNullException(nameof(orderClient));
    }

    public IOrderClient Orders { get; }
}

public static class CibPayClientFactory
{
    public static CibPayClient Create(SdkOptions options)
    {
        var services = new ServiceCollection();

        services.AddCibPayClient(options);
        
        var serviceProvider = services.BuildServiceProvider();
        
        return serviceProvider.GetRequiredService<CibPayClient>();
    }
} 