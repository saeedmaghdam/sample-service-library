using Consul;
using Consul.Filtering;
using ITnCare.Service.Consul;
using ITnCare.Service.Framework;
using ITnCare.Service.Models;
using Microsoft.Extensions.DependencyInjection;

namespace ITnCare.Service.Bootstrapper
{
    internal class ServiceDiscoveryBootstrapper
    {
        private readonly BootstrapperModel _bootstrapperModel;

        public ServiceDiscoveryBootstrapper(BootstrapperModel bootstrapperModel)
        {
            _bootstrapperModel = bootstrapperModel;
        }

        internal Task Bootstrap()
        {
            var consulClient = new ConsulClient(config =>
            {
                config.Address = new Uri(_bootstrapperModel.ConsulUrl);
            });
            _bootstrapperModel.Services.AddSingleton<IConsulClient>(consulClient);
            Context.ServiceResolverMethod = Enums.ServiceResolverMethodEnum.ConsulHealthCheck;

            return Task.CompletedTask;
        }
    }
}
