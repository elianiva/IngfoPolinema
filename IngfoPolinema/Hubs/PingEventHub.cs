using IngfoPolinema.Core.DomainModels;
using IngfoPolinema.Core.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace IngfoPolinema.Hubs;

public sealed class PingEventHub : Hub
{
    private readonly IPingEventStore _pingEventStore;
    private readonly IHubContext<PingEventHub> _hubContext;

    public PingEventHub(IPingEventStore pingEventStore, IHubContext<PingEventHub> hubContext)
    {
        _hubContext = hubContext;
        _pingEventStore = pingEventStore;
        _pingEventStore.Subscribe(SendPingResult);
    }

    public async Task SendPingResult(PingTarget target, PingHistory result)
    {
        await _hubContext.Clients.All.SendAsync("ReceivePingResult", target, result);
    }
}
