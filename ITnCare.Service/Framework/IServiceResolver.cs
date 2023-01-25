namespace ITnCare.Service.Framework
{
    public interface IServiceResolver
    {
        Task<Uri?> ResolveServiceUriAsync(string serviceName, CancellationToken cancellationToken);
        Task<Uri?> ResolveServiceUriAsync(string serviceName, string path, CancellationToken cancellationToken);
    }
}
