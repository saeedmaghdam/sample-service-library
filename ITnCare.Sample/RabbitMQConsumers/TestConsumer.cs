using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ITnCare.Sample.RabbitMQConsumers
{
    public class TestConsumer
    {
        private readonly IMediator _mediator;

        public TestConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task ConsumeAsync(IModel channel, object? model, BasicDeliverEventArgs ea)
        {
            await ConsumeAsync(channel, model, ea, default(CancellationToken));
        }

        public async Task ConsumeAsync(IModel channel, object? model, BasicDeliverEventArgs ea, CancellationToken cancellationToken)
        {
            try
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                await _mediator.Publish(new TestNotification { Message = message }, cancellationToken);
                channel.BasicAck(deliveryTag: ea.DeliveryTag, false);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
