using Core.CrossCuttingConcerns.Exceptions.Extensions;
using Infrastructure;
using Application;
using Microsoft.AspNetCore.Identity;
using WebApi;
using Infrastructure;
using Core.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
//builder.Services.AddDiscoveryClient();
builder.Services.AddHttpContextAccessor();

//TokenOptions? tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidIssuer = tokenOptions?.Issuer,
//            ValidAudience = tokenOptions?.Audience,
//            IssuerSigningKey = builder.Services.BuildServiceProvider().GetRequiredService<ISigningCredentialsProvider>().GetSigningCredentials().Key,
//            ClockSkew = TimeSpan.Zero
//        };
//    });

var app = builder.Build();

await CreateCustomerIndex.EnsureElasticIndexCreateAsync(app.Services);
ServiceActivator.Configure(app.Services);

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
    app.ConfigureExceptionMiddleware();
//app.UseMonitoring();
app.UseRouting();
//app.UseAuthentication();
//app.UseAuthorization();
app.MapControllers();

app.Run();


