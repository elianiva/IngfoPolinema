using IngfoPolinema.Core.Data;
using IngfoPolinema.Core.DomainModels;

namespace IngfoPolinema.Services;

public sealed class PingService
{
    private readonly IPingDataSource _pingRepository;

    public PingService(IPingDataSource pingRepository)
    {
        _pingRepository = pingRepository;
    }

    public IEnumerable<PingTarget> GetPingTargets()
    {
        return _pingRepository.GetPingTargets();
    }

    public IEnumerable<PingHistory> GetPingHistories(Uri address)
    {
        return _pingRepository.GetPingHistories(address);
    }
}
