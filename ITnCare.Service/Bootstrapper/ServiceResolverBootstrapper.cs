using Consul;
using ITnCare.Service.Framework;
using ITnCare.Service.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace ITnCare.Service.Bootstrapper
{
    internal class ServiceResolverBootstrapper
    {
        private readonly BootstrapperModel _bootstrapperModel;

        internal ServiceResolverBootstrapper(BootstrapperModel bootstrapperModel)
        {
            _bootstrapperModel = bootstrapperModel;
        }

        internal Task Bootstrap()
        {
            _bootstrapperModel.Services.AddSingleton<IServiceResolver>((services) =>
            {
                var consulClient = services.CreateScope().ServiceProvider.GetRequiredService<IConsulClient>();
                return new ServiceResolver(consulClient, new ReadOnlyDictionary<string, string>(_bootstrapperModel.GlobalOptions.ServiceUrls));
            });
            return Task.CompletedTask;
        }
    }
}
