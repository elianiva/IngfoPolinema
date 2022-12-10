using System;
using System.Net;

namespace IngfoPolinema.ViewModels;

public sealed record class History(DateTimeOffset TimeStamp, HttpStatusCode StatusCode);
