namespace ITnCare.Service.Framework
{
    public interface IRabbitMQProducer
    {
        /// <summary>
        /// Publish a message to a queue using global options.
        /// </summary>
        /// <param name="queueName">Queue name.</param>
        /// <param name="message">Message to be publish into the queue.</param>
        /// <returns>Returns a task</returns>
        Task Publish(string queueName, string message);

        /// <summary>
        /// Publish a message to a queue using global options.
        /// </summary>
        /// <param name="rabbitMQOptionsId">Options id defined in global appsettings.json</param>
        /// <param name="queueName">Queue name.</param>
        /// <param name="message">Message to be publish into the queue.</param>
        /// <returns>Returns a task</returns>
        Task Publish(string rabbitMQOptionsId, string queueName, string message);

        /// <summary>
        /// Publish a message to a queue using service options.
        /// </summary>
        /// <param name="rabbitMQOptionsId">Options id defined in service's appsettings.json</param>
        /// <param name="queueName">Queue name.</param>
        /// <param name="message">Message to be publish into the queue.</param>
        /// <returns>Returns a task</returns>
        Task PublishUsingServiceOptions(string rabbitMQOptionsId, string queueName, string message);
    }
}
