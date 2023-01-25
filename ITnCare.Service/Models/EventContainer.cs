using ITnCare.Service.Enums;
using ITnCare.Service.Framework;
using MongoDB.Bson.Serialization.Attributes;

namespace ITnCare.Service.Models
{
    public class EventContainer : IEventContainer
    {
        [BsonId]
        public Guid Id { get; set; }
        public string EventType { get; internal set; } = default!;
        public DateTime EventDate { get; internal set; } = default!;
        public DateTime? EventLastUpdateDate { get; internal set; } = default!;
        public string Event { get; internal set; } = default!;
        public EventBoxStatusEnum Status { get; internal set; } = default!;
    }
}
