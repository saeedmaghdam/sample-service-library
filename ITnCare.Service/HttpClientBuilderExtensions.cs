using ITnCare.Service.Framework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.CircuitBreaker;

namespace ITnCare.Service
{
    public static class HttpClientBuilderExtensions
    {
        private static ServiceOptions _serviceOptions = default!;

        public static IHttpClientBuilder UseCircuitBreakerPolicy(this IHttpClientBuilder httpClientBuilder, IConfiguration configuration)
        {
            return UseCircuitBreakerPolicyPrivate(httpClientBuilder, null, configuration);
        }

        public static IHttpClientBuilder UseCircuitBreakerPolicy(this IHttpClientBuilder httpClientBuilder, string policyName, IConfiguration configuration)
        {
            return UseCircuitBreakerPolicyPrivate(httpClientBuilder, policyName, configuration);
        }

        public static IHttpClientBuilder UseAdvancedCircuitBreakerPolicy(this IHttpClientBuilder httpClientBuilder, IConfiguration configuration)
        {
            return UseAdvancedCircuitBreakerPolicyPrivate(httpClientBuilder, null, configuration);
        }

        public static IHttpClientBuilder UseAdvancedCircuitBreakerPolicy(this IHttpClientBuilder httpClientBuilder, string policyName, IConfiguration configuration)
        {
            return UseAdvancedCircuitBreakerPolicyPrivate(httpClientBuilder, policyName, configuration);
        }

        private static IHttpClientBuilder UseCircuitBreakerPolicyPrivate(this IHttpClientBuilder httpClientBuilder, string policyName, IConfiguration configuration)
        {
            InitializeOptions(configuration);

            var finalPolicyName = policyName ?? "Default";
            if (!_serviceOptions.CircuitBreaker.BasicPolicies.ContainsKey(finalPolicyName))
                throw new Exception($"No basic policy with name {finalPolicyName} found.");

            var policy = _serviceOptions.CircuitBreaker.BasicPolicies[finalPolicyName];

            var basicCircuitBreakerPolicy = Policy
                .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .CircuitBreakerAsync(policy.HandledEventsAllowedBeforeBreaking, TimeSpan.FromMilliseconds(policy.DurationOfBreakInMs));

            return httpClientBuilder.AddPolicyHandler(basicCircuitBreakerPolicy);
        }

        private static IHttpClientBuilder UseAdvancedCircuitBreakerPolicyPrivate(this IHttpClientBuilder httpClientBuilder, string policyName, IConfiguration configuration)
        {
            InitializeOptions(configuration);

            var finalPolicyName = policyName ?? "Default";
            if (!_serviceOptions.CircuitBreaker.AdvancedPolicies.ContainsKey(finalPolicyName))
                throw new Exception($"No advanced policy with name {finalPolicyName} found.");

            var policy = _serviceOptions.CircuitBreaker.AdvancedPolicies[finalPolicyName];

            var advancedCircuitBreakerPolicy = Policy
                .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .AdvancedCircuitBreakerAsync(policy.FailureThreshold, TimeSpan.FromMilliseconds(policy.SamplingDurationInMs), policy.MinimumThroughput, TimeSpan.FromMilliseconds(policy.DurationOfBreakInMs));

            return httpClientBuilder.AddPolicyHandler(advancedCircuitBreakerPolicy);
        }

        private static void InitializeOptions(IConfiguration configuration)
        {
            if (_serviceOptions == null)
            {
                var consulUrl = configuration["ITnCareService:Consul:Host"];
                var serviceName = configuration["ITnCareService:Name"];
                var serviceConfiguration = Helper.LoadAppSettings(serviceName, consulUrl, true, false).Result;
                var serviceOptions = new ServiceOptions();
                serviceConfiguration.Bind(serviceOptions);
                _serviceOptions = serviceOptions;
            }
        }
    }
}
