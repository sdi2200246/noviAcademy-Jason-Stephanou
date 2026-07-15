using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace WorldRank.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services , IConfiguration configuration)
	{
		services.AddDbContext<WorldRankDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Default")));
		return services;
	}
}
