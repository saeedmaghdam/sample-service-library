using ITnCare.Service.Models;
using ITnCare.Service.RabbitMQ;

namespace ITnCare.Service.Framework
{
    public interface IRabbitMQSubscriber
    {
        /// <summary>
        /// Subscribe to a queue using global options.
        /// </summary>
        /// <param name="builder">Subscriber model builder.</param>
        /// <returns>Returns a task</returns>
        Task Subscribe(Func<RabbitMQSubscriberModelBuilder, RabbitMQSubscriberModel> builder);

        /// <summary>
        /// Subscribe to a queue using global options.
        /// </summary>
        /// <param name="rabbitMQOptionsId">Options id defined in global appsettings.json</param>
        /// <param name="builder">Subscriber model builder.</param>
        /// <returns>Returns a task</returns>
        Task Subscribe(string rabbitMQOptionsId, Func<RabbitMQSubscriberModelBuilder, RabbitMQSubscriberModel> builder);

        /// <summary>
        /// Subscribe to a queue using service options.
        /// </summary>
        /// <param name="rabbitMQOptionsId">Options id defined in service's appsettings.json</param>
        /// <param name="builder">Subscriber model builder.</param>
        /// <returns>Returns a task</returns>
        Task SubscribeUsingServiceOptions(string rabbitMQOptionsId, Func<RabbitMQSubscriberModelBuilder, RabbitMQSubscriberModel> builder);
    }
}
