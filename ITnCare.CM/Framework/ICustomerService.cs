using ITnCare.CM.Entities;

namespace ITnCare.CM.Framework
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllAsync(CancellationToken cancellationToken);
    }
}
