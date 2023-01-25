using ITnCare.Service.Models;

namespace ITnCare.EventHandler.Events
{
    public class OrderUpdatedEvent : Event
    {
        public OrderUpdatedEvent() : base(typeof(OrderUpdatedEvent).FullName)
        {
        }

        public string OrderId { get; set; }
    }
}
