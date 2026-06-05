using Core.Events;
using Core.Messaging;
using Core.Messaging.Transport.RabbitMq;
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
            //services
            //   .AddJwtServices()
            //   .AddEncryptServices()
            //   .AddRedisSecurityServices(configuration);

            services.AddRabbitMqTransport(configuration)
                .AddHostedSubscriber()
                .AddMessagingSerializer()
                .AddEvent();

            //services.AddOTelIntegration(configuration);

            //services.AddMonitoring(configuration, builder =>
            //{
            //    builder.AddNpgSql(connectionString: configuration.GetConnectionString("InvoiceDbConnection"), name: "invoiceservice-db", tags: new[] { "services" });
            //});

            return services;
        }
    }
}
