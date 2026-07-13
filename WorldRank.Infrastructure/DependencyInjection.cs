using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorldRank.Application.Interfaces;
using WorldRank.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace WorldRank.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services , IConfiguration configuration)
	{
		services.AddScoped<IPlayerRepository, DbPlayerRepository>();
		services.AddScoped<IWalletRepository, DbWalletRepository>();
		services.AddDbContext<WorldRankDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Default")));
		return services;
	}
}
