using IngfoPolinema.Core.Services.Interfaces;
using Microsoft.Extensions.Hosting;
using IngfoPolinema.Core.Data;
using Microsoft.Extensions.DependencyInjection;

namespace IngfoPolinema.Services.BackgroundServices;

public class PersistorService : IHostedService, IDisposable
{
    private readonly IPingEventStore _pingEventStore;
    private IDisposable? _subscription;
    public IServiceProvider ServiceProvider { get; }

    public PersistorService(IServiceProvider serviceProvider, IPingEventStore pingEventStore)
    {
        _pingEventStore = pingEventStore;
        ServiceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _subscription = _pingEventStore.Subscribe(async (pingTarget, pingHistory) =>
        {
            using IServiceScope scope = ServiceProvider.CreateScope();
            IPingDataSource dataSource = scope.ServiceProvider.GetRequiredService<IPingDataSource>();
            await dataSource.SavePingHistory(pingTarget.Address, pingHistory);
        });

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _subscription?.Dispose();
        return Task.CompletedTask;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _subscription?.Dispose();
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
