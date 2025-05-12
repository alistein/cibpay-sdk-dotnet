using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using CibPaySdk.Core.Exceptions;
using CibPaySdk.Core.Models;

namespace CibPay.Http.Handlers;

internal class RequestHandler
{
    private readonly HttpClient _httpClient;
    private readonly X509Certificate2 _certificate;
    private readonly SdkOptions _sdkOptions;

    internal RequestHandler(HttpClient httpClient, SdkOptions sdkOptions)
    {
        _httpClient = httpClient;
        _sdkOptions = sdkOptions;
        _certificate = GetCertificate();

        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _httpClient.BaseAddress = new Uri(sdkOptions.BaseUrl);
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Basic", sdkOptions.Credentials);
    }

    internal async Task<T?> SendRequestAsync<T>(HttpMethod method, string endpoint, object? body = null,
        CancellationToken cancellationToken = default)
    {
        using var handler = new HttpClientHandler { ClientCertificates = { _certificate } };

        using var httpClientWithCertificate = new HttpClient(handler);
        httpClientWithCertificate.BaseAddress = _httpClient.BaseAddress;
        httpClientWithCertificate.DefaultRequestHeaders.Authorization = _httpClient.DefaultRequestHeaders.Authorization;

        using var request = new HttpRequestMessage(method, endpoint);

        T? response = default;

        if (body is not null)
        {
            var jsonContent = JsonSerializer.Serialize(body);
            request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        }

        using var providerResponse = await httpClientWithCertificate.SendAsync(request, cancellationToken);

        var stringResponseContent = await providerResponse.Content.ReadAsStringAsync(cancellationToken);

        if (providerResponse.IsSuccessStatusCode)
        {
            response = JsonSerializer.Deserialize<T>(stringResponseContent);
        }
        else
        {
            var deserializeErrorContent = JsonSerializer.Deserialize<CibPayErrorResponse>(stringResponseContent);

            throw new CibPayApiException(
                failureMessage: deserializeErrorContent?.FailureMessage!,
                failureType: deserializeErrorContent?.FailureType!, 
                httpStatusCode: providerResponse.StatusCode,
                orderId: deserializeErrorContent?.OrderId);
        }

        return response;
    }
    
    private X509Certificate2 GetCertificate()
    {
        return new X509Certificate2(_sdkOptions.CertificatePath, _sdkOptions.CertificatePassword);
    }
}