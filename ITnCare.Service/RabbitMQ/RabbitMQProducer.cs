using ITnCare.Service.Framework;
using RabbitMQ.Client;
using System.Text;

namespace ITnCare.Service.RabbitMQ
{
    public class RabbitMQProducer : IRabbitMQProducer
    {
        public RabbitMQProducer() 
        {
        }

        public Task Publish(string queueName, string message)
        {
            var key = $"{RabbitMQOptionsOrigin.Global}_Default";
            if (!Context.RabbitMQChannels.ContainsKey(key))
                throw new Exception("No RabbitMQ options found with key Default on the global appsettings.json");

            return Publish(RabbitMQOptionsOrigin.Global, "Default", queueName, message);
        }

        public Task Publish(string rabbitMQOptionsId, string queueName, string message)
        {
            return Publish(RabbitMQOptionsOrigin.Global, rabbitMQOptionsId, queueName, message);
        }

        public Task PublishUsingServiceOptions(string rabbitMQOptionsId, string queueName, string message)
        {
            return Publish(RabbitMQOptionsOrigin.Service, rabbitMQOptionsId, queueName, message);
        }

        private Task Publish(RabbitMQOptionsOrigin optionsOrigin, string rabbitMQOptionsId, string queueName, string message)
        {
            var key = $"{optionsOrigin}_{rabbitMQOptionsId}";
            if (!Context.RabbitMQChannels.ContainsKey(key))
                throw new Exception($"No RabbitMQ Options found with key {rabbitMQOptionsId} on the {optionsOrigin} appsettings.json");

            var channel = Context.RabbitMQChannels[key];

            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: "",
                                 routingKey: queueName,
                                 basicProperties: null,
                                 body: body);

            return Task.CompletedTask;
        }
    }
}
