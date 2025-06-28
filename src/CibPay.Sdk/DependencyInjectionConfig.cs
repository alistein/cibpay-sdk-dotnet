using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using CibPay.Http.Clients;
using CibPay.Http.Configuration;
using CibPay.Http.Handlers;
using CibPaySdk.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CibPay.Sdk;

public static class DependencyInjectionConfig
{
    public static IServiceCollection AddCibPayClient(this IServiceCollection services, SdkOptions sdkOptions)
    {
        services.AddSingleton(sdkOptions);

        services.AddHttpClient<RequestHandler>((provider, httpClient) =>
        {
            var options = provider.GetRequiredService<SdkOptions>();
            httpClient.BaseAddress = new Uri(options.BaseUrl);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", options.Credentials);
        })
        .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler()
        {
            ClientCertificates =
            {
                new X509Certificate2(sdkOptions.CertificatePath, sdkOptions.CertificatePassword)
            }
        });
        
        services.AddScoped<IOrderClient, OrderClient>();
        
        services.AddScoped<CibPayClient>();
        
        return services;
    }
}