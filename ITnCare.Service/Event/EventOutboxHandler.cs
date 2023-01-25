using ITnCare.Service.Framework;
using ITnCare.Service.Models;
using MongoDB.Driver;

namespace ITnCare.Service.Event
{
    public class EventOutboxHandler : IEventOutboxHandler
    {
        private const int DELAY_IN_MS = 5000;

        public EventOutboxHandler()
        {
        }

        public async Task SubscribeAsync(string connectionString, string databaseName, string collectionName, Action<IEventContainer> eventHandler, CancellationToken cancellationToken)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            var collection = database.GetCollection<EventContainer>($"EventOutbox_{collectionName}");

            do
            {
                var events = await collection.Find(x => x.Status == Enums.EventBoxStatusEnum.Submit).Limit(50).ToListAsync(cancellationToken);
                if (events.Any())
                {
                    foreach (var @event in events)
                    {
                        @event.Status = Enums.EventBoxStatusEnum.Processing;
                        try
                        {
                            eventHandler(@event);
                            @event.Status = Enums.EventBoxStatusEnum.Processed;
                        }
                        catch (Exception ex)
                        {
                            @event.Status = Enums.EventBoxStatusEnum.Failed;
                        }

                        var filter = Builders<EventContainer>.Filter.Eq(s => s.Id, @event.Id);
                        await collection.ReplaceOneAsync(filter, @event, cancellationToken: cancellationToken);
                    }
                }

                await Task.Delay(TimeSpan.FromMilliseconds(DELAY_IN_MS));
        } while (!cancellationToken.IsCancellationRequested);
        }
    }
}
