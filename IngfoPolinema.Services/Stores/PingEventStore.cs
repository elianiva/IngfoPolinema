using IngfoPolinema.Core.DomainModels;
using IngfoPolinema.Core.Services.Interfaces;
using System.Reactive.Subjects;

namespace IngfoPolinema.Services.Stores;

public class PingEventStore : IPingEventStore
{
    private readonly Subject<(PingTarget Target, PingHistory Result)> _subject = new();

    public void Publish(PingTarget target, PingHistory result)
    {
        _subject.OnNext((Target: target, Result: result));
    }

    public IDisposable Subscribe(Func<PingTarget, PingHistory, Task> subscriber)
    {
        return _subject.Subscribe((@event) => subscriber.Invoke(@event.Target, @event.Result));
    }

    public void Dispose()
    {
        _subject.Dispose();
    }
}
