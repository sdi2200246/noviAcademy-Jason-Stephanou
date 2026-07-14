using WorldRank.Application;
using WorldRank.Infrastructure;

namespace WorldRank.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddWorldRankApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplication();
        services.AddInfrastructure(configuration);
        return services;
    }
}