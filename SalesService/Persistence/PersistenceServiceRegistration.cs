using Application.Repositories;
using Core.Persistence.Repositories.MongoDb.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceService(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoSettings = configuration.GetSection("MongoSettings").Get<MongoConnectionSettings>();

            services.AddSingleton(mongoSettings);

            services.AddScoped<IOrderRepository, OrderRepository>();
            return services;
        }
    }
}
