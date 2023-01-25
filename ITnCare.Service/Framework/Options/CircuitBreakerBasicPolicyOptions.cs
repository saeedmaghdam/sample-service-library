namespace ITnCare.Service.Framework.Options
{
    public class CircuitBreakerBasicPolicyOptions
    {
        public int HandledEventsAllowedBeforeBreaking { get; set; }
        public long DurationOfBreakInMs { get; set; }
    }
}
