using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IngfoPolinema;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPingEventHubConnection(this IServiceCollection services)
    {
        services.AddScoped(provider =>
        {
            NavigationManager navigation = provider.GetRequiredService<NavigationManager>();
            return new HubConnectionBuilder()
                .WithUrl(navigation.ToAbsoluteUri("/PingEventHub"))
                .ConfigureLogging(logging =>
                {
                    logging.AddConsole();
                    logging.SetMinimumLevel(LogLevel.Information);
                })
                .WithAutomaticReconnect()
                .Build();
        });
        return services;
    }
}
