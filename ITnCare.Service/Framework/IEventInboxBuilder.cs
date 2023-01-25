namespace ITnCare.Service.Framework
{
    public interface IEventInboxBuilder
    {
        Task Register<T>(string collectionName) where T : IEvent;
    }
}
