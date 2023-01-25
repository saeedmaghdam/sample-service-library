using ITnCare.Sample.Events;
using ITnCare.Sample.RabbitMQConsumers;
using ITnCare.Service.Framework;

namespace ITnCare.Sample
{
    public class Worker : IHostedService
    {
        private readonly IRabbitMQSubscriber _rabbitMQSubscriber;
        private readonly TestConsumer _testConsumer;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IServiceResolver _serviceResolver;
        private readonly IEventOutbox<OrderCreatedEvent> _orderCreatedEventOutbox;
        private readonly IEventOutbox<OrderUpdatedEvent> _orderUpdatedEventOutbox;
        private readonly IEventOutbox<NewCustomerRegisteredEvent> _newCustomerRegisteredEventOutbox;
        private readonly IEventInbox<NewCustomerRegisteredEvent> _newCustomerRegisteredEventInbox;

        public Worker(IRabbitMQSubscriber rabbitMQSubscriber, TestConsumer testConsumer, IHttpClientFactory httpClientFactory, IServiceResolver serviceResolver, IEventOutbox<OrderCreatedEvent> orderCreatedEventOutbox, IEventOutbox<OrderUpdatedEvent> orderUpdatedEventOutbox, IEventOutbox<NewCustomerRegisteredEvent> newCustomerRegisteredEventOutbox, IEventInbox<NewCustomerRegisteredEvent> newCustomerRegisteredEventInbox)
        {
            _rabbitMQSubscriber = rabbitMQSubscriber;
            _testConsumer = testConsumer;
            _httpClientFactory = httpClientFactory;
            _serviceResolver = serviceResolver;
            _orderCreatedEventOutbox = orderCreatedEventOutbox;
            _orderUpdatedEventOutbox = orderUpdatedEventOutbox;
            _newCustomerRegisteredEventOutbox = newCustomerRegisteredEventOutbox;
            _newCustomerRegisteredEventInbox = newCustomerRegisteredEventInbox;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var orderCreatedEvent = CreateNewOrderCreatedEvent();
            _orderCreatedEventOutbox.StoreAsync(orderCreatedEvent, cancellationToken).GetAwaiter().GetResult();

            var orderUpdatedEvent = CreateNewOrderUpdatedEvent();
            _orderUpdatedEventOutbox.StoreAsync(orderUpdatedEvent, cancellationToken).GetAwaiter().GetResult();

            var newCustomerRegisteredEvent = CreateNewCustomerRegisteredEvent();
            _newCustomerRegisteredEventOutbox.StoreAsync(newCustomerRegisteredEvent, cancellationToken).GetAwaiter().GetResult();
            _newCustomerRegisteredEventInbox.StoreAsync(newCustomerRegisteredEvent, cancellationToken).GetAwaiter().GetResult();

            var sample = _serviceResolver.ResolveServiceUriAsync("ITnCare.Sample", cancellationToken).Result;

            //Task.Run(async () =>
            //{
            //    var httpClient = _httpClientFactory.CreateClient("TestService");

            //    for (int i = 0; i < 100; i++)
            //    {
            //        try
            //        {
            //            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync($"test2");
            //        }
            //        catch (Exception ex)
            //        {
            //            var msg = ex.Message;
            //        }
            //    }
            //});

            _rabbitMQSubscriber.Subscribe(consumerBuilder =>
            {
                return consumerBuilder
                    .SetQueueName("itncare.scheduler.modifyrequest")
                    .SetAutoAck(false)
                    .SetEventHandler(async (channel, model, ea) =>
                    {
                        await _testConsumer.ConsumeAsync(channel, model, ea, cancellationToken);
                    })
                    .Build();
            });

            return Task.CompletedTask;
        }

        private NewCustomerRegisteredEvent CreateNewCustomerRegisteredEvent()
        {
            return new NewCustomerRegisteredEvent
            {
                CustomerId = Guid.NewGuid().ToString(),
                CustomerName = "Saeed"
            };
        }

        private OrderUpdatedEvent CreateNewOrderUpdatedEvent()
        {
            return new OrderUpdatedEvent
            {
                OrderId = "ac2e3e68-5019-4c01-8b5e-45e87af91d3f"
            };
        }

        private OrderCreatedEvent CreateNewOrderCreatedEvent()
        {
            return new OrderCreatedEvent
            {
                CustomerId = Guid.NewGuid().ToString(),
                OrderId = Guid.NewGuid().ToString(),
                OrderLines = new List<OrderLine>
                {
                    new OrderLine
                    {
                        ProductId = Guid.NewGuid().ToString(),
                        Count = 50,
                        Price = 16000
                    },
                    new OrderLine
                    {
                        ProductId = Guid.NewGuid().ToString(),
                        Count = 20,
                        Price = 90000
                    }
                }
            };
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
