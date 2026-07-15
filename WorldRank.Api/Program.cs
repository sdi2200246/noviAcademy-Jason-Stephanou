using WorldRank.Api;
using WorldRank.Application.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using WorldRank.Application.Interfaces;
using WorldRank.Infrastructure.Caching;
using WorldRank.Application;
using WorldRank.Infrastructure;
using NLog.Web;
using Autofac;                                   
using Autofac.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(cb =>
{
    cb.RegisterModule(new ApplicationModule());
    cb.RegisterModule(new InfrastructureModule());
});

builder.Logging.ClearProviders();
builder.Host.UseNLog();

builder.Services.AddWorldRankApi(builder.Configuration);


builder.Services.AddControllers()
    .AddJsonOptions(o =>
        o.JsonSerializerOptions.Converters.Add(
            new System.Text.Json.Serialization.JsonStringEnumConverter()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssemblyContaining<CreatePlayerCommandValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<ICache, MemoryCacheStore>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();