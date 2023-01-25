using ITnCare.EventHandler.Events;
using ITnCare.Service.Framework;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ITnCare.EventHandler
{
    public class Worker : IHostedService
    {
        private readonly IOptions<ApplicationOptions> _options;
        private readonly IEventOutboxHandler _eventOutboxHandler;

        public Worker(IOptions<ApplicationOptions> options, IEventOutboxHandler eventOutboxHandler)
        {
            _options = options;
            _eventOutboxHandler = eventOutboxHandler;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(async () => await _eventOutboxHandler.SubscribeAsync(_options.Value.ConnectionStrings["ITnCare.Sample_MongoDB"], "ITnCare_Sample", "OrderEvents", async (eventContainer) =>
            {
                var type = eventContainer.EventType;
                switch (type)
                {
                    case "ITnCare.Sample.Events.OrderCreatedEvent":
                        var orderCreatedEvent = JsonConvert.DeserializeObject<OrderCreatedEvent>(eventContainer.Event);
                        break ;
                    case "ITnCare.Sample.Events.OrderUpdatedEvent":
                        var orderUpdatedEvent = JsonConvert.DeserializeObject<OrderUpdatedEvent>(eventContainer.Event);
                        break;
                }
            }, cancellationToken));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
