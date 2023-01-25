using ITnCare.Service.Models;

namespace ITnCare.Service.Framework
{
    public interface IEventOutboxHandler
    {
        Task SubscribeAsync(string databaseName, string connectionString, string collectionName, Action<IEventContainer> eventHandler, CancellationToken cancellationToken);
    }
}
