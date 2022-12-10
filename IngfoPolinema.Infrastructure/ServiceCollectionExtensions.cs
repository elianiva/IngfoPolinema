using IngfoPolinema.Core.Data;
using IngfoPolinema.Infrastructure.DataSource;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace IngfoPolinema.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPingRepository(this IServiceCollection services)
    {
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        DataSourceOptions dataSourceOptions = serviceProvider.GetRequiredService<IOptions<DataSourceOptions>>().Value;
        services.AddDbContext<PingDbContext>(options =>
        {
            options.UseSqlite(dataSourceOptions.ConnectionString);
        });
        services.AddScoped<IPingDataSource, PingRepository>();
        return services;
    }
}
