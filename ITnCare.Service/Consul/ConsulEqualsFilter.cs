using Consul.Filtering;

namespace ITnCare.Service.Consul
{
    internal sealed class ConsulEqualsFilter<TSelector> : Filter where TSelector : Selector
    {
        public TSelector Selector { get; set; }
        public string Value { get; set; }

        public override string Encode() => $"{Selector.Encode()} == {Quote(Value)}";
    }
}
