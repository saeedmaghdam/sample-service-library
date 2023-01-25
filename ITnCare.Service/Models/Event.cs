using ITnCare.Service.Framework;

namespace ITnCare.Service.Models
{
    public abstract class Event : IEvent
    {
        private string _eventType;

        public Event(string eventType)
        {
            _eventType = eventType;
        }

        public string GetEventType()
        {
            return _eventType;
        }
    }
}
