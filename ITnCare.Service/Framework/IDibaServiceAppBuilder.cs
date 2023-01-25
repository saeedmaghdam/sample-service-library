namespace ITnCare.Service.Framework
{
    public interface IITnCareServiceAppBuilder
    {
        /// <summary>
        /// Adds a consumer to RabbitMQ's queue.
        /// </summary>
        /// <param name="builder">Subscriber model builder.</param>
        /// <returns>Returns a task</returns>
        Task AddRabbitMQConsumer(Action<IRabbitMQSubscriber> builder);
    }
}
