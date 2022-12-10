using IngfoPolinema.Core.Services.Interfaces;
using IngfoPolinema.Services.BackgroundServices;
using IngfoPolinema.Services.Stores;
using Microsoft.Extensions.DependencyInjection;

namespace IngfoPolinema.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddVisitorService(this IServiceCollection services)
    {
        services.AddTransient<IVisitor, VisitorService>();
        return services;
    }

    public static IServiceCollection AddBackgroundServices(this IServiceCollection services)
    {
        services.AddHostedService<WorkerService>();
        services.AddHostedService<PersistorService>();
        return services;
    }

    public static IServiceCollection AddPingEventStore(this IServiceCollection services)
    {
        services.AddSingleton<IPingEventStore, PingEventStore>();
        return services;
    }

    public static IServiceCollection AddPingService(this IServiceCollection services)
    {
        services.AddScoped<PingService>();
        return services;
    }
}
