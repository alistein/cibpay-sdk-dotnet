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

    internal async Task<T?> SendRequestAsync<T>(
        HttpMethod method,
        string endpoint,
        object? body = null,
        CancellationToken cancellationToken = default
    )
    {
        using var request = new HttpRequestMessage(method, endpoint);

        T? response = default;

        if (body is not null)
        {
            var jsonContent = JsonSerializer.Serialize(body);
            request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        }

        using var providerResponse = await _httpClient.SendAsync(request, cancellationToken);

        System.Console.WriteLine(endpoint);

        var locationHeader = providerResponse.Headers.Location?.ToString();

        var stringResponseContent = await providerResponse.Content.ReadAsStringAsync(
            cancellationToken
        );

        if (providerResponse.IsSuccessStatusCode)
        {
            response = JsonSerializer.Deserialize<T>(stringResponseContent);

            // If the response is OrderProviderResponse and we have a location header, set it
            if (
                response is OrderProviderResponse orderResponse
            )
            {
                orderResponse.PaymentUrl = string.IsNullOrEmpty(locationHeader) ? null : locationHeader;
                orderResponse.RawResult = stringResponseContent;
            }
        }
        else
        {
            var deserializeErrorContent = JsonSerializer.Deserialize<CibPayErrorResponse>(
                stringResponseContent
            );

            throw new CibPayApiException(
                deserializeErrorContent ?? new CibPayErrorResponse(),
                httpStatusCode: providerResponse.StatusCode
            );
        }

        return response;
    }
}
