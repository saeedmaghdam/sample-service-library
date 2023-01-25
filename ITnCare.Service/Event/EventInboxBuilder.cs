using ITnCare.Service.Framework;
using Microsoft.Extensions.DependencyInjection;

namespace ITnCare.Service.Event
{
    public class EventInboxBuilder : IEventInboxBuilder
    {
        private readonly IServiceCollection _services;
        private readonly string _serviceName;
        private readonly string _connectionString;

        public EventInboxBuilder(IServiceCollection services, string serviceName, string connectionString)
        {
            _services = services;
            _serviceName = serviceName;
            _connectionString = connectionString;
        }

        public Task Register<T>(string collectionName) where T : IEvent
        {
            var instance = new EventInbox<T>(_serviceName, collectionName, _connectionString);
            _services.AddSingleton<IEventInbox<T>>(instance);

            return Task.CompletedTask;
        }
    }
}
