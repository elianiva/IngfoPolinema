using IngfoPolinema.Core.DomainModels;
using IngfoPolinema.Core.Services.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IngfoPolinema.Core.Services;

public sealed class PingWorker : IAsyncDisposable
{
    private readonly IVisitor _visitor;
    private readonly IPingEventStore _store;
    private readonly PingTarget _target;
    private Timer? _timer;

    public PingWorker(IVisitor visitor, IPingEventStore pingEventStore, PingTarget target)
    {
        _visitor = visitor;
        _store = pingEventStore;
        _target = target;
        _timer = new Timer(
            callback: async (state) => await PublishNewPingResult(),
            state: null,
            dueTime: Timeout.InfiniteTimeSpan,
            period: target.Interval
        );
    }

    private async Task PublishNewPingResult()
    {
        PingHistory result = await _visitor.VisitAsync(_target);
        _store.Publish(_target, result);
    }

    public void Start()
    {
        _timer?.Change(TimeSpan.Zero, _target.Interval);
    }

    public void Stop()
    {
        _timer?.Change(Timeout.Infinite, 0);
    }

    public async ValueTask DisposeAsync()
    {
        if (_timer is IAsyncDisposable timer)
        {
            await timer.DisposeAsync();
        }

        _timer = null;
    }
}
