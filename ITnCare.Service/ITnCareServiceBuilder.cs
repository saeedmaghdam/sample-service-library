using ITnCare.Service.Bootstrapper;
using ITnCare.Service.Framework;
using ITnCare.Service.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace ITnCare.Service
{
    internal class ITnCareServiceBuilder : IITnCareServiceBuilder
    {
        private readonly IServiceCollection? _services = default;

        private BootstrapperModel? _bootstrapperModel = default;

        private ITnCareServiceBuilder() { }

        internal ITnCareServiceBuilder(IServiceCollection services, IConfiguration configuration)
        {
            _services = services;
            SetCurrentDirectory();
            Initialize(configuration);
        }

        public Task AddCaching()
        {
            new CacheBoostrapper(_bootstrapperModel!).Bootstrap();
            return Task.CompletedTask;
        }

        public Task AddCaching(string instanceName)
        {
            new CacheBoostrapper(_bootstrapperModel!).Bootstrap(instanceName);
            return Task.CompletedTask;
        }

        public Task AddCentralizedConfiguration<T>(bool optional = true, bool reloadOnChange = false) where T : class
        {
            new CentralizedConfigurationBootstrapper(_bootstrapperModel!).Bootstrap<T>(optional, reloadOnChange);
            return Task.CompletedTask;
        }

        public Task AddDebugToConsole()
        {
            new DebugToConsoleBootstrapper(_bootstrapperModel!).Bootstrap();
            return Task.CompletedTask;
        }

        public Task AddHealthCheck()
        {
            new HealthCheckBootstrapper(_bootstrapperModel!).Bootstrap();
            return Task.CompletedTask;
        }

        public Task AddLogging()
        {
            new LoggingBootstrapper(_bootstrapperModel!).Bootstrap();
            return Task.CompletedTask;
        }

        public Task AddMetrics()
        {
            throw new NotImplementedException();
        }

        public Task AddServiceDiscovery()
        {
            new ServiceDiscoveryBootstrapper(_bootstrapperModel!).Bootstrap();
            return Task.CompletedTask;
        }

        public Task AddServiceResolver()
        {
            new ServiceResolverBootstrapper(_bootstrapperModel!).Bootstrap();
            return Task.CompletedTask;
        }

        public Task AddTracing()
        {
            new TracingBootstrapper(_bootstrapperModel!).Bootstrap();
            return Task.CompletedTask;
        }

        public Task AddRabbitMQ()
        {
            new RabbitMQBootstrapper(_bootstrapperModel!).Bootstrap();
            return Task.CompletedTask;
        }

        public Task AddKafka()
        {
            new KafkaBootstrapper(_bootstrapperModel!).Bootstrap();
            return Task.CompletedTask;
        }

        public Task AddHangFire()
        {
            throw new NotImplementedException();
        }

        public Task AddMediatR<T>() where T : class
        {
            new MediatRBootstrapper(_bootstrapperModel!).Bootstrap<T>();
            return Task.CompletedTask;
        }

        public Task AddEventOutbox(Action<IEventOutboxBuilder> builder)
        {
            new EventOutboxBootstrapper(_bootstrapperModel!).Bootstrap(builder);
            return Task.CompletedTask;
        }

        public Task AddEventOutboxHandler()
        {
            new EventOutboxHandlerBootstrapper(_bootstrapperModel!).Bootstrap();
            return Task.CompletedTask;
        }

        public Task AddEventInbox(Action<IEventInboxBuilder> builder)
        {
            new EventInboxBootstrapper(_bootstrapperModel!).Bootstrap(builder);
            return Task.CompletedTask;
        }

        public Task AddEventInboxHandler()
        {
            new EventInboxHandlerBootstrapper(_bootstrapperModel!).Bootstrap();
            return Task.CompletedTask;
        }

        private Task SetCurrentDirectory()
        {
            var pathToExe = Process.GetCurrentProcess().MainModule!.FileName;
            var pathToContentRoot = Path.GetDirectoryName(pathToExe);

            Directory.SetCurrentDirectory(pathToContentRoot!);

            return Task.CompletedTask;
        }

        private Task Initialize(IConfiguration localConfiguration)
        {
            var instanceId = Guid.NewGuid().ToString();
            var consulUrl = localConfiguration["ITnCareService:Consul:Host"];
            var serviceName = localConfiguration["ITnCareService:Name"];

            var serviceConfiguration = Helper.LoadAppSettings(serviceName, consulUrl, true, false).Result;
            var globalConfiguration = Helper.LoadAppSettings(null, consulUrl, true, false).Result;

            var serviceOptions = new ServiceOptions();
            serviceConfiguration.Bind(serviceOptions);

            var globalOptions = new GlobalOptions();
            globalConfiguration.Bind(globalOptions);

            _bootstrapperModel = new BootstrapperModel(_services!, localConfiguration, globalConfiguration, serviceConfiguration, globalOptions, serviceOptions, serviceName, instanceId, consulUrl);
            Context.InstanceId = instanceId;
            Context.BootstrapperModel = _bootstrapperModel;

            _services.AddOptions();
            _services.Configure<GlobalOptions>(globalConfiguration);
            _services.Configure<ServiceOptions>(serviceConfiguration);

            _services.AddHostedService<ITnCareWorker>();

            return Task.CompletedTask;
        }
    }
}
