using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using WorldRank.Application.Commands;
using WorldRank.Application.Strategies;
using WorldRank.Application.Validation;
using WorldRank.Application.Behaviors;

namespace WorldRank.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
		services.AddMediatR(cfg =>
		{
			cfg.RegisterServicesFromAssemblyContaining<CreatePlayerCommand>();
			cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));   // ← this
		});

        services.AddSingleton<IFundsStrategy, AddFundsStrategy>();
        services.AddSingleton<IFundsStrategy, SubtractFundsStrategy>();
        services.AddSingleton<IFundsStrategy, ForceSubtractFundsStrategy>();

        services.AddValidatorsFromAssemblyContaining<CreateWalletCommandValidator>();
		services.AddValidatorsFromAssemblyContaining<CreatePlayerCommandValidator>();

        return services;
    }
}