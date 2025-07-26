using System.Text;
using System.Text.Json;
using CibPaySdk.Core.Exceptions;
using CibPaySdk.Core.Models;

namespace CibPay.Http.Handlers;

public class RequestHandler
{
    private readonly HttpClient _httpClient;

    public RequestHandler(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    internal async Task<T?> SendRequestAsync<T>(HttpMethod method, string endpoint, object? body = null,
        CancellationToken cancellationToken = default)
    {
        
        using var request = new HttpRequestMessage(method, endpoint);

        T? response = default;

        if (body is not null)
        {
            var jsonContent = JsonSerializer.Serialize(body);
            request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        }

        using var providerResponse = await _httpClient.SendAsync(request, cancellationToken);

        var stringResponseContent = await providerResponse.Content.ReadAsStringAsync(cancellationToken);

        if (providerResponse.IsSuccessStatusCode)
        {
            response = JsonSerializer.Deserialize<T>(stringResponseContent);
        }
        else
        {
            var deserializeErrorContent = JsonSerializer.Deserialize<CibPayErrorResponse>(stringResponseContent);

            throw new CibPayApiException(
                deserializeErrorContent ?? new CibPayErrorResponse(),
                httpStatusCode: providerResponse.StatusCode);
        }

        return response;
    }
}