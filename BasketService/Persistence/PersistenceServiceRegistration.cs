using Application.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IBasketRepository>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                var connection = ConnectionMultiplexer.Connect(config.GetConnectionString("BasketDbConnection"));
                return new BasketRepository(connection);
            });
            return services;
        }
    }
}
