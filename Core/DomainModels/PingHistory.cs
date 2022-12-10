using System;
using System.Net;

namespace IngfoPolinema.Core.DomainModels;

public class PingHistory
{
    required public TimeSpan Duration { get; init; }
    required public HttpStatusCode StatusCode { get; init; }
    required public DateTimeOffset TimeStamp { get; init; }
};
