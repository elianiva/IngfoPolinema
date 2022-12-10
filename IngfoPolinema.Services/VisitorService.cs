using IngfoPolinema.Core.DomainModels;
using IngfoPolinema.Core.Services.Interfaces;
using System.Diagnostics;
using System.Net;
using System.Reactive;

namespace IngfoPolinema.Services;

public sealed class VisitorService : IVisitor
{
    private readonly HttpClient _httpClient;

    public VisitorService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PingHistory> VisitAsync(PingTarget target)
    {
        CancellationTokenSource cts = new();
        cts.CancelAfter(target.Timeout);

        Stopwatch stopwatch = Stopwatch.StartNew();
        try
        {
            HttpRequestMessage request = new(HttpMethod.Head, target.Address.AbsoluteUri);
            HttpResponseMessage response = await _httpClient.SendAsync(request, cts.Token);

            return new PingHistory
            {
                Duration = stopwatch.Elapsed,
                StatusCode = response.StatusCode,
                TimeStamp = DateTimeOffset.Now
            };
        }
        catch (Exception exc)
        {
            // TODO(elianiva): handle exception properly
            if (exc is OperationCanceledException || exc is HttpRequestException)
            {
                return new PingHistory
                {
                    Duration = stopwatch.Elapsed,
                    StatusCode = HttpStatusCode.RequestTimeout,
                    TimeStamp = DateTimeOffset.Now
                };
            }

            return new PingHistory
            {
                Duration = stopwatch.Elapsed,
                StatusCode = HttpStatusCode.RequestTimeout,
                TimeStamp = DateTimeOffset.Now
            };
        }
    }
}
