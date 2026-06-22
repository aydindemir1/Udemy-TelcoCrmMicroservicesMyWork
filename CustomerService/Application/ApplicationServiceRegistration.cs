using Application.Features.IndividualCustomers.Rules;
using Application.Services.Districts;
using Core.Abstractions.Rules;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Validation;
using Core.Cqrs;
using Core.Extensions;
using Core.Tracing.Mediator;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assemblies = Core.Extensions.AssemblyExtensions.GetDomainAssemblies("Application");

            services.AddCqrs(assemblies, services =>
            {
               services.AddTransient(typeof(IPipelineBehavior<,>), typeof(OtelDiagnosticsRequestBehavior<,>));
                services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
                services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
            });

            services.AddScoped<IDistrictService, DistrictManager>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssemblies(assemblies);
            services.AddAutoMapper(assemblies);

            services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));
        
            return services;
        }
    }
}
