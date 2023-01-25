namespace ITnCare.Service.Framework
{
    public interface IEventOutboxBuilder
    {
        Task Register<T>(string collectionName) where T : IEvent;
    }
}
