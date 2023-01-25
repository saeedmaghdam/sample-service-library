using ITnCare.Service.Models;

namespace ITnCare.EventHandler.Events
{
    public class OrderCreatedEvent : Event
    {
        public OrderCreatedEvent() : base(typeof(OrderCreatedEvent).FullName)
        {
        }

        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public List<OrderLine> OrderLines { get; set; }
    }
}
