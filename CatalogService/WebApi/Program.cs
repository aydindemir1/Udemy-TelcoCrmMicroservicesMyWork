using Application;
using Core.CrossCuttingConcerns.Exceptions.Extensions;
using Core.Scheduling.Hangfire;
using Core.Security.Encryption;
using Core.Security.Jwt;
using Hangfire;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Persistence;
using Steeltoe.Discovery.Client;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

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

app.UseHangfireScheduler();
//app.UseMonitoring();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();