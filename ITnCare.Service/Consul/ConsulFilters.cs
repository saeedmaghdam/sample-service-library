using Consul.Filtering;

namespace ITnCare.Service.Consul
{
    internal static class ConsulFilters
    {
        internal static Filter Eq<TSelector>(TSelector selector, string value)
            where TSelector : Selector => new ConsulEqualsFilter<TSelector>
            {
                Selector = selector,
                Value = value,
            };
    }
}
