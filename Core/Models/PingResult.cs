using System;

namespace Core.Models;

public sealed record class PingResult(int StatusCode, DateTimeOffset TimeStamp);
