using Application.Clients;
using Core.Events;
using Core.Messaging;
using Core.Messaging.Transport.RabbitMq;
using Infrastructure.Clients;
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

            services.AddHttpClient<IBasketServiceClient, BasketServiceClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["ExternalServices:BasketService"]);
            });
            //.AddCustomPolicyHandlers()
            //.AddServiceDiscovery()
            //.AddAuthTokenHandler();


            services.AddHttpClient<ICustomerServiceClient, CustomerServiceClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["ExternalServices:CustomerService"]);
            });
            //  .AddCustomPolicyHandlers()
            //.AddServiceDiscovery()
            //.AddAuthTokenHandler();

            //services
            //   .AddJwtServices()
            //   .AddEncryptServices()
            //   .AddRedisSecurityServices(configuration);

            services.AddRabbitMqTransport(configuration)
                .AddMessagingSerializer()
                .AddEvent();

            //services.AddOTelIntegration(configuration);

            //services.AddMonitoring(configuration, builder =>
            //{
            //    builder.AddMongoDb(clientFactory: sp =>
            //    {
            //        var settings = configuration.GetSection("MongoSettings");
            //        var connectionString = settings["ConnectionString"];
            //        return new MongoClient(connectionString);
            //    }, databaseNameFactory: sp => "SalesServiceDb", name: "salesservice-db", failureStatus: HealthStatus.Unhealthy, tags: new[] { "services" });

            //});

            return services;
        }
    }
}
