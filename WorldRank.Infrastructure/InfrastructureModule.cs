using Autofac;
using WorldRank.Infrastructure.Persistence.Commands;
using WorldRank.Infrastructure.Persistence.Querys;
using WorldRank.Application.Commands;
using WorldRank.Application.Querys;
namespace WorldRank.Infrastructure;

public class InfrastructureModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(ThisAssembly)
            .Where(t => t.Name.EndsWith("Persistence"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        builder.RegisterDecorator<PlayerPersistanceCacheDecorator, ICreatePlayerPersistence>();
        builder.RegisterDecorator< ListPlayerPersistenceCachingDecorator ,IReadPlayerPersistence>();
        builder.RegisterDecorator< CreateWalletPersistenceCachingDecorator , ICreateWlletPersistence>(); 
    }
}