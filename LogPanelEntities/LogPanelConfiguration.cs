using LogPanelEntities.Entities;
using LogPanelEntities.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace LogPanelEntities;

public static class LogPanelConfiguration
{
    public static IServiceCollection AddClientLogConfig(this IServiceCollection services, Action<List<BaseClient>> clientesConfig)
    {
        IClientLogDbConfig config = new ClientLogDbConfig();
        List<BaseClient> clientes = new List<BaseClient>();
        clientesConfig(clientes);

        services.AddTransient<ILogRepository, LogRepository>();

        clientes.ForEach(c => config.AddClientDb(c));

        services.AddSingleton(config);

        return services;
    }
}