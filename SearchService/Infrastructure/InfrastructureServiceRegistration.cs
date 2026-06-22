using Core.ElasticSearch;
using Core.Events;
using Core.Messaging;
using Core.Messaging.Transport.RabbitMq;
using Core.Monitoring.HealthChecks;
using Core.Security.Encryption;
using Core.Security.Jwt;
using Core.Security.Redis;
using Core.Tracing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration
                                                                                                   )
        {
            services
               .AddJwtServices()
               .AddEncryptServices()
               .AddRedisSecurityServices(configuration);

            services.AddRabbitMqTransport(configuration)
                .AddMessagingCore()
                .AddMessagingSerializer()
                .AddHostedSubscriber()
                .AddEvent();

            services.AddElasticSearch();

            services.AddOTelIntegration(configuration);

            services.AddMonitoring(configuration, builder =>
            {
                builder.AddElasticsearch(elasticsearchUri: configuration["ElasticSearch:ConnectionString"], name: "searchservice-db", tags: new[] { "services" });
            });

            return services;
        }
    }
}
