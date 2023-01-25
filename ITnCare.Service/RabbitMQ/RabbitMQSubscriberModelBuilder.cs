using ITnCare.Service.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ITnCare.Service.RabbitMQ
{
    public class RabbitMQSubscriberModelBuilder
    {
        private RabbitMQSubscriberModel _model = new RabbitMQSubscriberModel();
        private Dictionary<string, object> _arguments;

        public RabbitMQSubscriberModelBuilder SetQueueName(string queueName)
        {
            _model.QueueName = queueName;
            return this;
        }

        public RabbitMQSubscriberModelBuilder SetDurable(bool durable)
        {
            _model.Durable = durable;
            return this;
        }

        public RabbitMQSubscriberModelBuilder SetExclusive(bool exclusive)
        {
            _model.Exclusive = exclusive;
            return this;
        }

        public RabbitMQSubscriberModelBuilder SetAutoDelete(bool autoDelete)
        {
            _model.AutoDelete = autoDelete;
            return this;
        }

        public RabbitMQSubscriberModelBuilder AddArgument(string key, object value)
        {
            if (_arguments == null)
                _arguments = new Dictionary<string, object>();

            _arguments.Add(key, value);
            return this;
        }

        public RabbitMQSubscriberModelBuilder SetAutoAck(bool autoAck)
        {
            _model.AutoAck = autoAck;
            return this;
        }

        public RabbitMQSubscriberModelBuilder SetEventHandler(Action<IModel, object?, BasicDeliverEventArgs> eventHandler)
        {
            _model.EventHandler = eventHandler;
            return this;
        }

        public RabbitMQSubscriberModel Build()
        {
            if (string.IsNullOrEmpty(_model.QueueName))
                throw new Exception("Queue name is required.");

            if (string.IsNullOrEmpty(_model.QueueName.Trim()))
                throw new Exception("Queue name is required.");

            if (_model.EventHandler == null)
                throw new Exception("Queue name is required.");

            _model.Arguments = _arguments;
            return _model;
        }
    }
}
