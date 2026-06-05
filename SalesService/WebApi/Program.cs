using Microsoft.AspNetCore.Identity;
using Application;
using Infrastructure;
using Persistence;
using Core.CrossCuttingConcerns.Exceptions.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddPersistenceService(builder.Configuration);
//builder.Services.AddDiscoveryClient();
builder.Services.AddHttpContextAccessor();

//TokenOptions? tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidIssuer = tokenOptions.Issuer,
//        ValidAudience = tokenOptions.Audience,
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = builder.Services.BuildServiceProvider().GetRequiredService<ISigningCredentialsProvider>().GetSigningCredentials().Key,
//        ClockSkew = TimeSpan.Zero
//    };
//});

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
    app.ConfigureExceptionMiddleware();
//app.UseMonitoring();
app.UseRouting();
//app.UseAuthentication();
//app.UseAuthorization();
app.MapControllers();


app.Run();
