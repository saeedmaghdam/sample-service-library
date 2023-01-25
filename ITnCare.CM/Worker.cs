using ITnCare.CM.Bootstrapper;

namespace ITnCare.CM
{
    public class Worker : IHostedService
    {
        private readonly CacheBootstrapper _cacheBootstrapper;

        public Worker(IServiceScopeFactory serviceScopeFactory)
        {
            var scope = serviceScopeFactory.CreateScope();
            _cacheBootstrapper = scope.ServiceProvider.GetRequiredService<CacheBootstrapper>();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _cacheBootstrapper.Bootstrap(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
