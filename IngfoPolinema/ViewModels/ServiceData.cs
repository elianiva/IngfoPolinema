using System;
using System.Collections.Generic;

namespace IngfoPolinema.ViewModels;

public sealed record class ServicePingDetail
{
    required public string Name { get; init; }
    required public string Description { get; init; }
    required public Uri Address { get; init; }
    required public ServiceStatus Status { get; init; }
    required public List<History> Histories { get; set; }
};

