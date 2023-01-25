using ITnCare.CM.Framework;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace ITnCare.CM.Bootstrapper
{
    public class CacheBootstrapper
    {
        private readonly ICacheHelper _cacheHelper;
        private readonly ICustomerService _customerService;

        public CacheBootstrapper(IServiceScopeFactory serviceScopeFactory, IDistributedCache distributedCache)
        {
            var scope = serviceScopeFactory.CreateScope();
            _cacheHelper = scope.ServiceProvider.GetRequiredService<ICacheHelper>();
            _customerService = scope.ServiceProvider.GetRequiredService<ICustomerService>();
        }

        public async Task Bootstrap(CancellationToken cancellationToken)
        {
            var customers = await _customerService.GetAllAsync(cancellationToken);
            foreach (var customer in customers)
                await _cacheHelper.SetTradingCodeOrigin(customer.TradingID, customer.Origin, cancellationToken);
        }
    }
}
