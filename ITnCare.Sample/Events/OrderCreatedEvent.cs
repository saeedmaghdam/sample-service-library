using ITnCare.Service.Framework;
using ITnCare.Service.Models;

namespace ITnCare.Sample.Events
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
