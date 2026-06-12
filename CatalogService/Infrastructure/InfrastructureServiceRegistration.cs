using Core.Events;
using Core.Monitoring.HealthChecks;
using Core.Scheduling.Hangfire;
using Core.Security.Encryption;
using Core.Security.Jwt;
using Core.Security.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfireScheduler(configuration);

            services.AddDomainEvent();

            services
              .AddJwtServices()
              .AddEncryptServices()
              .AddRedisSecurityServices(configuration);

            //services.AddOTelIntegration(configuration);

            services.AddMonitoring(configuration, builder =>
            {
                builder.AddSqlServer(connectionString: configuration.GetConnectionString("CatalogDbConnection"), name: "catalogservice-db", tags: new[] { "services" });
            });


            return services;
        }
    }
}
