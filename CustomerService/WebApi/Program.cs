
using Application;
using Application.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Extensions;
using Microsoft.AspNetCore.Components.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query;
using Persistence;
using Persistence.Contexts;
using Persistence.Repositories;
using System.Text.Json.Serialization;
using Infrastructure;
using Core.Messaging.Postgres.Extensions;
using Steeltoe.Discovery.Client;
using Core.Security.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Core.Security.Encryption;
using Core.Monitoring.HealthChecks;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();



builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddDiscoveryClient();
builder.Services.AddHttpContextAccessor();

TokenOptions? tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions?.Issuer,
            ValidAudience = tokenOptions?.Audience,
            IssuerSigningKey = builder.Services.BuildServiceProvider().GetRequiredService<ISigningCredentialsProvider>().GetSigningCredentials().Key,
            ClockSkew = TimeSpan.Zero
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
   app.ConfigureExceptionMiddleware();
app.UseMonitoring();
app.UseRouting();
await app.UsePostgresMessaging();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();