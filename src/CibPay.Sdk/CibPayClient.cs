using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using CibPay.Http.Clients;
using CibPay.Http.Configuration;
using CibPay.Http.Handlers;
using CibPaySdk.Core.Interfaces;

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
    /// <summary>
    /// Creates a thread-safe instance of CibPayClient.
    /// Safe to use as singleton across multiple threads.
    /// </summary>
    public static CibPayClient Create(SdkOptions options)
    {
        if (options == null)
            throw new ArgumentNullException(nameof(options));

        ValidateOptions(options);

        var handler = new HttpClientHandler();
        handler.ClientCertificates.Add(
            new X509Certificate2(options.CertificatePath, options.CertificatePassword)
        );

        var httpClient = new HttpClient(handler);
        httpClient.BaseAddress = new Uri(options.BaseUrl);
        httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json")
        );
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Basic",
            options.Credentials
        );

        var requestHandler = new RequestHandler(httpClient);
        var orderClient = new OrderClient(requestHandler);

        return new CibPayClient(orderClient);
    }

    private static void ValidateOptions(SdkOptions options)
    {
        if (string.IsNullOrWhiteSpace(options.Username))
            throw new ArgumentException("Username cannot be null or empty", nameof(options));

        if (string.IsNullOrWhiteSpace(options.Password))
            throw new ArgumentException("Password cannot be null or empty", nameof(options));

        if (string.IsNullOrWhiteSpace(options.BaseUrl))
            throw new ArgumentException("BaseUrl cannot be null or empty", nameof(options));

        if (!Uri.TryCreate(options.BaseUrl, UriKind.Absolute, out _))
            throw new ArgumentException("BaseUrl must be a valid absolute URI", nameof(options));

        if (string.IsNullOrWhiteSpace(options.CertificatePath))
            throw new ArgumentException("CertificatePath cannot be null or empty", nameof(options));

        if (!File.Exists(options.CertificatePath))
            throw new FileNotFoundException(
                $"Certificate file not found: {options.CertificatePath}"
            );
    }
}
