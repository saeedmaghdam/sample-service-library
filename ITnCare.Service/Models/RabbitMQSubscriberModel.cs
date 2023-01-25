using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ITnCare.Service.Models
{
    public class RabbitMQSubscriberModel
    {
        public string QueueName { get; internal set; } = default!;
        public bool Durable { get; internal set; } = true;
        public bool Exclusive { get; internal set; } = false;
        public bool AutoDelete { get; internal set; } = false;
        public IDictionary<string, object> Arguments { get; internal set; } = null;
        public bool AutoAck { get; internal set; } = true;
        public Action<IModel, object?, BasicDeliverEventArgs> EventHandler { get; internal set; } = null;
    }
}
