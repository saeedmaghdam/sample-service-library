using Consul;
using ITnCare.Service.Enums;
using ITnCare.Service.Models;
using RabbitMQ.Client;

namespace ITnCare.Service
{
    internal static class Context
    {
        internal static string InstanceId = default!;
        internal static Dictionary<string, IModel> RabbitMQChannels = new Dictionary<string, IModel>();
        internal static BootstrapperModel BootstrapperModel = default!;
        internal static bool IsHealthCheckEnabled = false;
        internal static ServiceResolverMethodEnum ServiceResolverMethod = ServiceResolverMethodEnum.GlobalOptions;
    }
}
