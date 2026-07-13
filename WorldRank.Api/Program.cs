using WorldRank.Api;
using WorldRank.Api.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using WorldRank.Application.Interfaces;
using WorldRank.Infrastructure.Caching;
using NLog.Web;


var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Host.UseNLog();
builder.Services.AddWorldRankApi(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssemblyContaining<AddPlayerRequestValidator>();
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