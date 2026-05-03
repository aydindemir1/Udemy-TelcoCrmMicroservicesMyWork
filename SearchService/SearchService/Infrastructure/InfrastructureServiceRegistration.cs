using Core.ElasticSearch;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services//, IConfiguration configuration
                                                                                                   )
        {
            //services
            //   .AddJwtServices()
            //   .AddEncryptServices()
            //   .AddRedisSecurityServices(configuration);

            //services.AddRabbitMqTransport(configuration)
            //    .AddMessagingCore()
            //    .AddMessagingSerializer()
            //    .AddHostedSubscriber()
            //    .AddEvent();

            services.AddElasticSearch();

            //services.AddOTelIntegration(configuration);

            //services.AddMonitoring(configuration, builder =>
            //{
            //    builder.AddElasticsearch(elasticsearchUri: configuration["ElasticSearch:ConnectionString"], name: "searchservice-db", tags: new[] { "services" });
            //});

            return services;
        }
    }
}
