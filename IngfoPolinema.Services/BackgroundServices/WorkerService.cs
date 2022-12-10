using IngfoPolinema.Core.Data;
using IngfoPolinema.Core.DomainModels;
using IngfoPolinema.Core.Services;
using IngfoPolinema.Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IngfoPolinema.Services.BackgroundServices;

public sealed class WorkerService : IHostedService, IAsyncDisposable
{
    private readonly IVisitor _visitor;
    private readonly IPingEventStore _pingEventStore;
    private IEnumerable<PingWorker> _workers = Enumerable.Empty<PingWorker>();
    public IServiceProvider ServiceProvider { get; }

    public WorkerService(IVisitor visitor, IServiceProvider serviceProvider, IPingEventStore pingEventStore)
    {
        _visitor = visitor;
        _pingEventStore = pingEventStore;
        ServiceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        using IServiceScope scope = ServiceProvider.CreateScope();
        IPingDataSource dataSource = scope.ServiceProvider.GetRequiredService<IPingDataSource>();
        IEnumerable<PingTarget> pingTargets = dataSource.GetPingTargets();
        _workers = pingTargets.Select(target => new PingWorker(_visitor, _pingEventStore, target));
        foreach (PingWorker worker in _workers)
        {
            worker.Start();
        }
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        foreach (PingWorker worker in _workers)
        {
            worker.Stop();
        }
        return Task.CompletedTask;
    }

    public async ValueTask DisposeAsync()
    {
        foreach (PingWorker worker in _workers)
        {
            await worker.DisposeAsync();
        }
    }
}
