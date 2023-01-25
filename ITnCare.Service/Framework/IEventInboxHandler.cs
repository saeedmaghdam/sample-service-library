using ITnCare.Service.Models;

namespace ITnCare.Service.Framework
{
    public interface IEventInboxHandler
    {
        Task SubscribeAsync(string connectionString, string databaseName, string collectionName, Action<IEventContainer> eventHandler, CancellationToken cancellationToken);
    }
}
