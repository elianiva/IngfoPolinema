using DomainModels = IngfoPolinema.Core.DomainModels;
using DataModels = IngfoPolinema.Infrastructure.Models;
using IngfoPolinema.Core.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace IngfoPolinema.Infrastructure.DataSource;

public sealed class PingRepository : IPingDataSource
{
    private readonly PingDbContext _dbContext;

    public PingRepository(PingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<DomainModels.PingTarget> GetPingTargets()
    {
        IEnumerable<DataModels.PingTarget> pingTargets = _dbContext.PingTargets.Include(target => target.Histories).OrderBy(target => target.Interval);
        return pingTargets.Select(target => new DomainModels.PingTarget
        {
            Name = target.Name,
            Description = target.Description,
            Address = target.Address,
            Interval = target.Interval,
            Timeout = target.Timeout,
            Histories = target.Histories.Select(history => new DomainModels.PingHistory
            {
                Duration = history.Duration,
                TimeStamp = history.TimeStamp,
                StatusCode = history.StatusCode,
            }).ToList(),
        });
    }

    public IEnumerable<DomainModels.PingHistory> GetPingHistories(Uri address)
    {
        IEnumerable<DataModels.PingHistory> histories = _dbContext.PingTargets.First(target => target.Address == address).Histories;
        return histories.Select(history => new DomainModels.PingHistory 
        {
            Duration = history.Duration,
            TimeStamp = history.TimeStamp,
            StatusCode = history.StatusCode,
        });
    }

    public async Task SavePingHistory(Uri address, DomainModels.PingHistory history)
    {
        DataModels.PingTarget? target = await _dbContext.PingTargets.SingleOrDefaultAsync(target => target.Address == address);
        if (target is null) return; // TODO(elianiva): handle this properly

        target.Histories.Add(new DataModels.PingHistory
        {
            Duration = history.Duration,
            StatusCode = history.StatusCode,
            TimeStamp = history.TimeStamp,
        });
        await _dbContext.SaveChangesAsync();
    }
}
