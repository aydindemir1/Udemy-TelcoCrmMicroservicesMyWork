using Application.Repositories;
using Core.Abstractions.ContextExecutions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CustomerDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("CustomerDbConnection"), npgsql =>
            {
                npgsql.MigrationsAssembly(typeof(CustomerDbContext).Assembly.FullName);
            }).UseSnakeCaseNamingConvention());

            services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<CustomerDbContext>());

            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IIndividualCustomerRepository, IndividualCustomerRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IDistrictRepository, DistrictRepository>();
            services.AddScoped<IContactMediumRepository, ContactMediumRepository>();
            services.AddScoped<IBillingAccountRepository, BillingAccountRepository>();
            return services;
        }
    }
}
