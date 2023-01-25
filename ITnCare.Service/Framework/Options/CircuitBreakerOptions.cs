namespace ITnCare.Service.Framework.Options
{
    public class CircuitBreakerOptions
    {
        public IDictionary<string, CircuitBreakerBasicPolicyOptions> BasicPolicies { get; set; } = default!;
        public IDictionary<string, CircuitBreakerAdvancedPolicyOptions> AdvancedPolicies { get; set; } = default!;
    }
}
