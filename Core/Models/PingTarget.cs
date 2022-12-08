using System;

namespace Core.Models;

public sealed record class PingTarget(string Name, string Description, Uri Address, TimeSpan VisitInterval, TimeSpan Timeout);