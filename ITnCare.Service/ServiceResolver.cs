using Consul;
using ITnCare.Service.Framework;

namespace ITnCare.Service
{
    public class ServiceResolver : IServiceResolver
    {
        private readonly IConsulClient _consulClient;
        private readonly IReadOnlyDictionary<string, string> _servicesUrls;
        private const string SCHEMA = "http";

        public ServiceResolver(IConsulClient consulClient) : this(consulClient, null)
        {
            _consulClient = consulClient;
        }

        public ServiceResolver(IConsulClient consulClient, IReadOnlyDictionary<string, string> servicesUrls)
        {
            _consulClient = consulClient;
            _servicesUrls = servicesUrls;
        }

        public async Task<Uri?> ResolveServiceUriAsync(string serviceName, CancellationToken cancellationToken)
        {
            if (Context.ServiceResolverMethod == Enums.ServiceResolverMethodEnum.ConsulHealthCheck)
                return await ResolveServiceUriUsingConsulHealthCheckAsync(serviceName, null, cancellationToken);
            else
                return ResolveServiceUriUsingOptions(serviceName, null);
        }

        public async Task<Uri?> ResolveServiceUriAsync(string serviceName, string path, CancellationToken cancellationToken)
        {
            if (Context.ServiceResolverMethod == Enums.ServiceResolverMethodEnum.ConsulHealthCheck)
                return await ResolveServiceUriUsingConsulHealthCheckAsync(serviceName, path, cancellationToken);
            else
                throw new NotImplementedException("Resolve using a service is not implemented yet.");
        }

        private async Task<Uri?> ResolveServiceUriUsingConsulHealthCheckAsync(string serviceName, string? path, CancellationToken cancellationToken)
        {
            //Get all services registered on Consul
            var services = await _consulClient.Health.Service(serviceName, string.Empty, true);

            if (services == null)
                return null;

            if (services.StatusCode != System.Net.HttpStatusCode.OK)
                return null;

            if (services.Response.Length == 0)
                return null;

            var service = GetRandomInstance(services.Response, serviceName);

            if (service == null)
                throw new Exception($"Consul service: '{serviceName}' was not found.");

            var uriBuilder = default(UriBuilder);
            if (string.IsNullOrEmpty(path))
            {
                uriBuilder = new UriBuilder()
                {
                    Scheme = SCHEMA,
                    Host = service.Service.Address,
                    Port = service.Service.Port
                };
            }
            else
            {
                uriBuilder = new UriBuilder()
                {
                    Scheme = SCHEMA,
                    Host = service.Service.Address,
                    Port = service.Service.Port,
                    Path = path
                };
            }

            return uriBuilder.Uri;
        }

        private Uri? ResolveServiceUriUsingOptions(string serviceName, string? path)
        {
            if (!_servicesUrls.ContainsKey(serviceName))
                return null;

            var uri = new Uri(_servicesUrls[serviceName]);
            if (string.IsNullOrEmpty(path))
                return uri;
            else
                return new Uri(uri, path);
        }

        private ServiceEntry GetRandomInstance(ServiceEntry[] services, string serviceName)
        {
            Random _random = new Random();
            return services[_random.Next(0, services.Length)];
        }
    }
}
