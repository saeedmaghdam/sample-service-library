using ITnCare.Service.Framework;
using ITnCare.Service.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ITnCare.Service.RabbitMQ
{
    public class RabbitMQSubscriber : IRabbitMQSubscriber
    {
        public RabbitMQSubscriber() 
        {
        }

        public Task Subscribe(Func<RabbitMQSubscriberModelBuilder, RabbitMQSubscriberModel> builder)
        {
            var key = $"{RabbitMQOptionsOrigin.Global}_Default";
            if (!Context.RabbitMQChannels.ContainsKey(key))
                throw new Exception("No RabbitMQ options found with key Default on the global appsettings.json");

            return Subscribe(RabbitMQOptionsOrigin.Global, "Default", builder);
        }

        public Task Subscribe(string rabbitMQOptionsId, Func<RabbitMQSubscriberModelBuilder, RabbitMQSubscriberModel> builder)
        {
            return Subscribe(RabbitMQOptionsOrigin.Global, rabbitMQOptionsId, builder);
        }

        public Task SubscribeUsingServiceOptions(string rabbitMQOptionsId, Func<RabbitMQSubscriberModelBuilder, RabbitMQSubscriberModel> builder)
        {
            return Subscribe(RabbitMQOptionsOrigin.Service, rabbitMQOptionsId, builder);
        }

        private Task Subscribe(RabbitMQOptionsOrigin optionsOrigin, string rabbitMQOptionsId, Func<RabbitMQSubscriberModelBuilder, RabbitMQSubscriberModel> builder)
        {
            var consumerModel = builder(new RabbitMQSubscriberModelBuilder());

            var key = $"{optionsOrigin}_{rabbitMQOptionsId}";
            if (!Context.RabbitMQChannels.ContainsKey(key))
                throw new Exception($"No RabbitMQ Options found with key {rabbitMQOptionsId} on the {optionsOrigin} appsettings.json");

            var channel = Context.RabbitMQChannels[key];

            channel.QueueDeclare(queue: consumerModel.QueueName,
                                 durable: consumerModel.Durable,
                                 exclusive: consumerModel.Exclusive,
                                 autoDelete: consumerModel.AutoDelete,
                                 arguments: consumerModel.Arguments);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                consumerModel.EventHandler(channel, model, ea);
            };

            channel.BasicConsume(queue: consumerModel.QueueName,
                                 autoAck: consumerModel.AutoAck,
                                 consumer: consumer);

            return Task.CompletedTask;
        }
    }
}
