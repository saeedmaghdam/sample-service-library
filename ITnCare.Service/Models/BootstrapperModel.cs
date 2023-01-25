using ITnCare.Service.Framework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ITnCare.Service.Models
{
    internal class BootstrapperModel
    {
        internal IServiceCollection Services;
        internal IConfiguration LocalConfiguration { get; private set; }
        internal IConfiguration GlobalConfiguration { get; private set; }
        internal IConfiguration ServiceConfiguration { get; private set; }
        internal GlobalOptions GlobalOptions { get; private set; }
        internal ServiceOptions ServiceOptions { get; private set; }
        internal string ServiceName { get; private set; }
        internal string InstanceId { get; private set; }
        internal string ConsulUrl { get; private set; }

        internal BootstrapperModel(IServiceCollection services, IConfiguration localConfiguration, IConfiguration globalConfiguration, IConfiguration serviceConfiguration, GlobalOptions globalOptions, ServiceOptions serviceOptions, string serviceName, string instanceId, string consulUrl)
        {
            Services = services;
            LocalConfiguration = localConfiguration;
            GlobalConfiguration = globalConfiguration;
            ServiceConfiguration = serviceConfiguration;
            GlobalOptions = globalOptions;
            ServiceOptions = serviceOptions;
            ServiceName = serviceName;
            InstanceId = instanceId;
            ConsulUrl = consulUrl;
        }
    }
}
