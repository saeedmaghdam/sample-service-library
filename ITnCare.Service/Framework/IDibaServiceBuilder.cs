using ITnCare.Service.Bootstrapper;

namespace ITnCare.Service.Framework
{
    public interface IITnCareServiceBuilder
    {
        /// <summary>
        /// Adds a simple debuger worker to check the functionality of the service.
        /// </summary>
        /// <returns>Returns a task.</returns>
        Task AddDebugToConsole();

        /// <summary>
        /// Adds health check service.
        /// </summary>
        /// <returns>Returns a task.</returns>
        Task AddHealthCheck();

        /// <summary>
        /// Add metrics service.
        /// </summary>
        /// <returns>Returns a task.</returns>
        Task AddMetrics();

        /// <summary>
        /// Register service to service discovery.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Returns a task.</returns>
        Task AddServiceDiscovery();

        /// <summary>
        /// Resolves a service url.
        /// </summary>
        /// <returns>Returns a task.</returns>
        Task AddServiceResolver();

        /// <summary>
        /// Adds distributed tracing.
        /// </summary>
        /// <returns>Returns a task.</returns>
        Task AddTracing();

        /// <summary>
        /// Adds logging configurations including distributed configurations.
        /// </summary>
        /// <returns>Returns a task.</returns>
        Task AddLogging();

        /// <summary>
        /// Adds centralized configurations.
        /// </summary>
        /// <typeparam name="T">Options model to be binded with configurations.</typeparam>
        /// <param name="optional">Determines if the options are optional or required.</param>
        /// <param name="reloadOnChange">Determines to reload the options on change.</param>
        /// <returns>Returns a task.</returns>
        Task AddCentralizedConfiguration<T>(bool optional = false, bool reloadOnChange = false) where T : class;

        /// <summary>
        /// Adds a distributed caching.
        /// </summary>
        /// <returns>Returns a task.</returns>
        Task AddCaching();

        /// <summary>
        /// Adds a distributed caching.
        /// </summary>
        /// <param name="instanceName">Instance name</param>
        /// <returns>Returns a task.</returns>
        Task AddCaching(string instanceName);

        /// <summary>
        /// Adds HangFire service.
        /// </summary>
        /// <returns>Returns a task.</returns>
        Task AddHangFire();

        /// <summary>
        /// Adds RabbitMQ service.
        /// </summary>
        /// <returns>Returns a task.</returns>
        Task AddRabbitMQ();

        /// <summary>
        /// Adds Kafka service.
        /// </summary>
        /// <returns>Returns a task.</returns>
        Task AddKafka();

        /// <summary>
        /// Adds MediatR service.
        /// </summary>
        /// <typeparam name="T">Always must be Program class.</typeparam>
        /// <returns>Returns a task.</returns>
        Task AddMediatR<T>() where T : class;

        /// <summary>
        /// Adds event outbox.
        /// </summary>
        /// <param name="builder">Event outbox builder.</param>
        /// <returns></returns>
        Task AddEventOutbox(Action<IEventOutboxBuilder> builder);

        /// <summary>
        /// Adds event outbox handler.
        /// </summary>
        /// <returns>Returns a task.</returns>
        Task AddEventOutboxHandler();

        /// <summary>
        /// Adds event inbox.
        /// </summary>
        /// /// <param name="builder">Event inbox builder.</param>
        /// <returns>Returns a task.</returns>
        Task AddEventInbox(Action<IEventInboxBuilder> builder);
    }
}
