using ITnCare.Service.AppBootstrapper;
using ITnCare.Service.Framework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ITnCare.Service
{
    public static class HostingExtensions
    {
        public static IServiceCollection AddITnCareService(this IServiceCollection services, IConfiguration configuration, Action<IITnCareServiceBuilder> builder)
        {
            builder(new ITnCareServiceBuilder(services, configuration));
            return services;
        }

        public static IApplicationBuilder UseITnCareService(this IApplicationBuilder builder, Action<IITnCareServiceAppBuilder> appBuilder)
        {
            appBuilder(new ITnCareServiceAppBuilder());
            UseITnCareService(builder);
            return builder;
        }

        public static IApplicationBuilder UseITnCareService(this WebApplication builder, Action<IITnCareServiceAppBuilder> appBuilder)
        {
            appBuilder(new ITnCareServiceAppBuilder());
            UseITnCareService(builder);
            return builder;
        }

        public static IApplicationBuilder UseITnCareService(this IApplicationBuilder builder)
        {
            new ServiceDiscoveryAppBootstrapper().Bootstrap(builder);
            return builder;
        }

        public static IApplicationBuilder UseITnCareService(this WebApplication builder)
         {
            if (Context.IsHealthCheckEnabled)
                builder.MapHealthChecks("/health/status");

            new ServiceDiscoveryAppBootstrapper().Bootstrap(builder);
            return builder;
        }
    }
}