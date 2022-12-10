using System;
using System.Collections.Generic;

namespace IngfoPolinema.Core.DomainModels;

public class PingTarget
{
    required public string Name { get; init; }
    required public string Description { get; init; }
    required public Uri Address { get; init; }
    required public TimeSpan Interval { get; init; }
    required public TimeSpan Timeout { get; init; }
    public List<PingHistory> Histories { get; set; } = new();
};