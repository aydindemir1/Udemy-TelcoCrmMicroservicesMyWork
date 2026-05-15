using Application.Repositories;
using Core.Abstractions.ContextExecutions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CatalogDbContext>(op => op.UseSqlServer(configuration.GetConnectionString("CatalogDbConnection")));
            services.AddScoped<IUnitOfWork>(provider =>
            provider.GetRequiredService<CatalogDbContext>());

            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IModelRepository, ModelRepository>();
            services.AddScoped<IProductOfferingRepository, ProductOfferingRepository>();
            services.AddScoped<IProductOfferingPriceRepository, ProductOfferingPriceRepository>();
            services.AddScoped<IProductSpecCharacteristicRepository, ProductSpecCharacteristicRepository>();
            services.AddScoped<IProductSpecificationRepository, ProductSpecificationRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductSpecCharacteristicValueRepository, ProductSpecCharacteristicValueRepository>();
            return services;
        }
    }
}
