using IngfoPolinema.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IngfoPolinema.Core.Data;

public interface IPingDataSource
{
    public IEnumerable<PingTarget> GetPingTargets();
    public IEnumerable<PingHistory> GetPingHistories(Uri address);
    public Task SavePingHistory(Uri address, PingHistory history);
}
