using ITnCare.Service.Bootstrapper;
using ITnCare.Service.Framework;
using Microsoft.Extensions.DependencyInjection;

namespace ITnCare.Service.Event
{
    public class EventOutboxBuilder : IEventOutboxBuilder
    {
        private readonly IServiceCollection _services;
        private readonly string _serviceName;
        private readonly string _connectionString;

        public EventOutboxBuilder(IServiceCollection services, string serviceName, string connectionString)
        {
            _services = services;
            _serviceName = serviceName;
            _connectionString = connectionString;
        }

        public Task Register<T>(string collectionName) where T : IEvent
        {
            var eventOutboxInstance = new EventOutbox<T>(_serviceName, collectionName, _connectionString);
            _services.AddSingleton<IEventOutbox<T>>(eventOutboxInstance);

            return Task.CompletedTask;
        }
    }
}
