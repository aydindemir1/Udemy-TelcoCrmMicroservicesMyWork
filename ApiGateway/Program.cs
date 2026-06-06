using Core.CrossCuttingConcerns.Exceptions.Extensions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Eureka;
using Ocelot.Provider.Polly;
using Steeltoe.Discovery.Client;

var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment;

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"ocelot.{env.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

//builder.Services.AddOTelIntegration(builder.Configuration);

builder.Services.AddOcelot()
    .AddEureka()
    .AddPolly();

builder.Services.AddDiscoveryClient();

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
    app.ConfigureExceptionMiddleware();

app.Lifetime.ApplicationStarted.Register(() =>
{
    app.Logger.LogInformation("ApiGateway started.");
});

app.Lifetime.ApplicationStopped.Register(() =>
{
    app.Logger.LogInformation("ApiGateway is shutting down.");
});


await app.UseOcelot();


app.Run();