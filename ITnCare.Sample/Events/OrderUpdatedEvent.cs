using ITnCare.Service.Framework;
using ITnCare.Service.Models;

namespace ITnCare.Sample.Events
{
    public class OrderUpdatedEvent : Event
    {
        public OrderUpdatedEvent() : base(typeof(OrderUpdatedEvent).FullName)
        {
        }

        public string OrderId { get; set; }
    }
}
