using Consul;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ITnCare.Service
{
    public class ITnCareWorker : IHostedService
    {
        private readonly ILogger<ITnCareWorker> _logger;
        private readonly IConfiguration _configuration;

        public ITnCareWorker(ILogger<ITnCareWorker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("ITnCare service started.");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("ITnCare service is shutting down ...");

            var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            var client = new ConsulClient(config =>
            {
                config.Address = new Uri(_configuration["ITnCareService:Consul:Host"]);
            });
            client.Agent.ServiceDeregister(Context.InstanceId).Wait(cancellationTokenSource.Token);

            _logger.LogInformation("ITnCare service shutdown.");
            return Task.CompletedTask;
        }
    }
}
