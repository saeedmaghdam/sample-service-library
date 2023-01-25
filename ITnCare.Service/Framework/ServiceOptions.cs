using ITnCare.Service.Framework.Options;

namespace ITnCare.Service.Framework
{
    public class ServiceOptions
    {
        public IDictionary<string, string> ConnectionStrings { get; set; } = default!;
        public IDictionary<string, RabbitMQOptions> RabbitMQs { get; set; } = default!;
        public CircuitBreakerOptions CircuitBreaker { get; set; } = default!;
        public bool TracingEnabled { get; set; }
    }
}
