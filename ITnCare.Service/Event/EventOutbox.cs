using ITnCare.Service.Framework;
using ITnCare.Service.Models;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace ITnCare.Service.Event
{
    public class EventOutbox<T> : IEventOutbox<T> where T : IEvent
    {
        private readonly IMongoCollection<EventContainer> _collection;

        public EventOutbox(string serviceName, string collectionName, string connectionString)
        {
            var client = new MongoClient(connectionString);
            var normalizedDatabaseName = serviceName.Replace(".", "_");
            var database = client.GetDatabase(normalizedDatabaseName);
            _collection = database.GetCollection<EventContainer>($"EventOutbox_{collectionName}");
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
