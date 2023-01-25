namespace ITnCare.Service.Framework
{
    public interface IEventOutbox<T> where T : IEvent
    {
        Task StoreAsync(T model, CancellationToken cancellationToken);
    }
}
