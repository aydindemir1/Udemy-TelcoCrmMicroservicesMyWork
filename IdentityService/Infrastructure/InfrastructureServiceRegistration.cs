using Core.Mailing;
using Core.Monitoring.HealthChecks;
using Core.Scheduling.Hangfire;
using Core.Security.EmailAuthenticator;
using Core.Security.Encryption;
using Core.Security.Hashing;
using Core.Security.Jwt;
using Core.Security.Redis;
using Core.Tracing;
using Infrastructure.Scheduler;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfireScheduler(configuration);

            services.AddHostedService<SchedulerBootstrapperHostedService>();

            services.AddMailing();

            services
                .AddEmailAuthenticatorServices()
                .AddHashingServices()
                .AddJwtServices()
                .AddEncryptServices()
                .AddRedisSecurityServices(configuration);

            services.AddOTelIntegration(configuration);

            services.AddMonitoring(configuration, builder =>
            {

                builder.AddSqlServer(connectionString: configuration.GetConnectionString("IdentityDbConnection"), name: "identityservice-db", tags: new[] { "services" });
            });

            return services;
        }
    }
}
