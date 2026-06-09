using Application.Services.Categories;
using Application.Services.ProductSpecifications;
using Core.Abstractions.Rules;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Validation;
using Core.Cqrs;
using Core.Extensions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assemblies = Core.Extensions.AssemblyExtensions.GetDomainAssemblies("Application");

            // CQRS ve MediatR Pipeline Davranışları
            services.AddCqrs(assemblies, services =>
            {
            //    services.AddTransient(typeof(IPipelineBehavior<,>), typeof(OtelDiagnosticsRequestBehavior<,>));
                services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
               services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));

            });

            //Cross cutting Concerns 
            //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(cfg => cfg.AddMaps(assemblies));

            services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));
            
            //Services
            services.AddScoped<ICategoryService, CategoryManager>()
                    .AddScoped<IProductSpecificationService, ProductSpecificationManager>();

            return services;
        }
    }
}
