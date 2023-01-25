namespace ITnCare.Service.Framework.Options
{
    public class CircuitBreakerAdvancedPolicyOptions
    {
        public double FailureThreshold { get; set; }
        public long SamplingDurationInMs { get; set; }
        public int MinimumThroughput { get; set; }
        public long DurationOfBreakInMs { get; set; }
    }
}
