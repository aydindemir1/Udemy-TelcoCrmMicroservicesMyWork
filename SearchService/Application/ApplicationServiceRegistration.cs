using Core.Cqrs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assemblies = Core.Extensions.AssemblyExtensions.GetDomainAssemblies("Application");

            services.AddCqrs(assemblies);
            //    , services =>
            //{
            //    services.AddTransient(typeof(IPipelineBehavior<,>), typeof(OtelDiagnosticsRequestBehavior<,>));
            //    services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
            //});
            return services;
        }
    }
}
