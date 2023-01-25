using ITnCare.Service.Framework;
using ITnCare.Service.Models;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace ITnCare.Service.Event
{
    public class EventInbox<T> : IEventInbox<T> where T : IEvent
    {
        private readonly IMongoCollection<EventContainer> _collection;

        public EventInbox(string serviceName, string collectionName, string connectionString)
        {
            var client = new MongoClient(connectionString);
            var normalizedDatabaseName = serviceName.Replace(".", "_");
            var database = client.GetDatabase(normalizedDatabaseName);
            _collection = database.GetCollection<EventContainer>($"EventInbox_{collectionName}");
        }

        public async Task StoreAsync(T model, CancellationToken cancellationToken)
        {
            var eventContainer = new EventContainer
            {
                Event = JsonConvert.SerializeObject(model),
                EventDate = DateTime.UtcNow,
                EventType = model.GetEventType(),
                Status = Enums.EventBoxStatusEnum.Submit
            };
            await _collection.InsertOneAsync(eventContainer, new InsertOneOptions { }, cancellationToken);
        }
    }
}
