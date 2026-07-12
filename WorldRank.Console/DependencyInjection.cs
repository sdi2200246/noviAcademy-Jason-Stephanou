using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using NLog.Extensions.Logging;
using WorldRank.Application;
using WorldRank.Infrastructure;

namespace WorldRank.Console;

public static class DependencyInjection
{
	// Composition root: wires up every layer's services in one place.
	public static IServiceCollection AddWorldRank(this IServiceCollection services , IConfiguration configuration)
	{
		services.AddLogging(builder =>
		{
			builder.ClearProviders();
			builder.SetMinimumLevel(LogLevel.Trace); // NLog rules in nlog.config decide the real thresholds
			builder.AddNLog();
		});

		services.AddApplication();
		services.AddInfrastructure(configuration);

		return services;
	}
}
