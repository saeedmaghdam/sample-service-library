using ITnCare.Service.Models;

namespace ITnCare.EventHandler.Events
{
    public class NewCustomerRegisteredEvent : Event
    {
        public NewCustomerRegisteredEvent() : base(typeof(NewCustomerRegisteredEvent).FullName)
        {
        }

        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
    }
}
