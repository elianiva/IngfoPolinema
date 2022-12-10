using IngfoPolinema.Core.DomainModels;
using System;
using System.Threading.Tasks;

namespace IngfoPolinema.Core.Services.Interfaces;

public interface IPingEventStore
{
    public void Publish(PingTarget target, PingHistory result);
    public IDisposable Subscribe(Func<PingTarget, PingHistory, Task> subscriber);
}
