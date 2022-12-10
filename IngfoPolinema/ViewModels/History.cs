using System;
using System.Net;

namespace IngfoPolinema.ViewModels;

public sealed record class History(TimeSpan Duration, DateTimeOffset TimeStamp, HttpStatusCode StatusCode);
