using ITnCare.Service.Framework;
using ITnCare.Service.RabbitMQ;

namespace ITnCare.Service
{
    public class ITnCareServiceAppBuilder : IITnCareServiceAppBuilder
    {
        public Task AddRabbitMQConsumer(Action<Framework.IRabbitMQSubscriber> builder)
        {
            builder(new RabbitMQSubscriber());
            return Task.CompletedTask;
        }
    }
}
