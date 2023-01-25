using ITnCare.Service.Framework.Options;

namespace ITnCare.Service.Framework
{
    public class GlobalOptions
    {
        public IDictionary<string, string> ConnectionStrings { get; set; } = default!;
        public ServiceDiscoveryOptions ServiceDiscovery { get; set; } = default!;
        public IDictionary<string, RabbitMQOptions> RabbitMQs { get; set; } = default!;
        public JaegerOptions Jaeger { get; set; } = default!;
        public bool TracingEnabled { get; set; }
        public IDictionary<string, string> ServiceUrls { get; set; }
    }   
}
