
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
            services.AddDbContext<InvoiceDbContext>(op => op.UseNpgsql(configuration.GetConnectionString("InvoiceDbConnection"), npgsql =>
            {
                npgsql.MigrationsAssembly(typeof(InvoiceDbContext).Assembly.FullName);
            }).UseSnakeCaseNamingConvention()
            );
            services.AddScoped<IUnitOfWork>(provider =>
            provider.GetRequiredService<InvoiceDbContext>());

            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            return services;
        }
    }
}
