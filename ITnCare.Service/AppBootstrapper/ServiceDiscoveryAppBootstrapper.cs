using Consul;
using Consul.Filtering;
using ITnCare.Service.Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ITnCare.Service.AppBootstrapper
{
    internal class ServiceDiscoveryAppBootstrapper
    {
        private IFeatureCollection _features;
        private IConsulClient _consulClient;

        public ServiceDiscoveryAppBootstrapper()
        {
        }

        internal Task Bootstrap(IApplicationBuilder builder)
        {
            var hostApplicationLifetime = builder.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();
            var server = builder.ApplicationServices.GetRequiredService<IServer>();
            _consulClient = builder.ApplicationServices.GetRequiredService<IConsulClient>();
            _features = server.Features;
            hostApplicationLifetime.ApplicationStarted.Register(() => BootstrapServiceDiscovery());

            return Task.CompletedTask;
        }

        private Task BootstrapServiceDiscovery()
        {
            var addressFeature = _features.Get<IServerAddressesFeature>();
            var addresses = addressFeature.Addresses;
            if (!addresses.Any())
                throw new Exception("A problem has occured in web builder.");

            var url = addresses.SingleOrDefault();
            if (string.IsNullOrEmpty(url))
                throw new Exception("A problem has occured in web builder.");

            var bootstrapperModel = Context.BootstrapperModel;
            var uri = new Uri(url);

            var healthCheckUrl = $"{url}/health/status";
            //healthCheckUrl = $"http://host.docker.internal:{uri.Port}/health/status";
            var agentReg = new AgentServiceRegistration()
            {
                Address = uri.Host,
                ID = bootstrapperModel.InstanceId,
                Name = bootstrapperModel.ServiceName,
                Port = uri.Port,
                Check = new AgentServiceCheck()
                {
                    HTTP = healthCheckUrl,
                    Notes = $"Checks {healthCheckUrl}",
                    Interval = TimeSpan.FromSeconds(bootstrapperModel.GlobalOptions.ServiceDiscovery.HealthCheck.Interval),
                    Timeout = TimeSpan.FromSeconds(bootstrapperModel.GlobalOptions.ServiceDiscovery.HealthCheck.Timeout),
                },
            };

#if DEBUG
            var consulFilter = ConsulFilters.Eq<ServiceSelector>(new ServiceSelector(), bootstrapperModel.ServiceName);
            var services = _consulClient.Agent.Services(consulFilter, default(CancellationToken)).GetAwaiter().GetResult();
            if (services.Response != null)
            {
                foreach (var service in services.Response)
                    _consulClient.Agent.ServiceDeregister(service.Value.ID).Wait(default(CancellationToken));
            }
#endif

            var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            _consulClient.Agent.ServiceDeregister(agentReg.ID).Wait(cancellationTokenSource.Token);
            _consulClient.Agent.ServiceRegister(agentReg).Wait(cancellationTokenSource.Token);

            return Task.CompletedTask;
        }
    }
}
