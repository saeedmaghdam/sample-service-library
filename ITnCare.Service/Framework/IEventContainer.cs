using ITnCare.Service.Enums;

namespace ITnCare.Service.Framework
{
    public interface IEventContainer
    {
        Guid Id { get; }
        string EventType { get; }
        DateTime EventDate { get; }
        string Event { get; }
        EventBoxStatusEnum Status { get; }
    }
}
