using Autofac;
using  WorldRank.Application.Decorators;
using MediatR;

namespace WorldRank.Application;
public class ApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(ThisAssembly)
            .Where(t => t.Name.EndsWith("Handler"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        builder.RegisterGenericDecorator(
            typeof(LoggingRequestHandler<,>),
            typeof(IRequestHandler<,>));
    }
}