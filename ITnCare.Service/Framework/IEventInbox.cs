namespace ITnCare.Service.Framework
{
    public interface IEventInbox<T> where T : IEvent
    {
        Task StoreAsync(T model, CancellationToken cancellationToken);
    }
}
