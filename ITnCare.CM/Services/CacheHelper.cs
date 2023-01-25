using ITnCare.CM.Enums;
using ITnCare.CM.Framework;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace ITnCare.CM.Services
{
    public class CacheHelper : ICacheHelper
    {
        private const string PREFIX = "cm.";
        private const string TRADING_CODE_ORIGIN = $"{PREFIX}trading_code_origin:";

        private readonly IDistributedCache _distributedCache;

        public CacheHelper(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<CustomerOriginEnum?> GetTradingCodeOrigin(string tradingCode, CancellationToken cancellationToken)
        {
            var result = await _distributedCache.GetStringAsync($"{TRADING_CODE_ORIGIN}{tradingCode}", cancellationToken);
            if (result is null)
                return null;

            Enum.TryParse(result, out CustomerOriginEnum customerOrigin);
            return customerOrigin;
        }

        public async Task SetTradingCodeOrigin(string tradingCode, CustomerOriginEnum customerOrigin, CancellationToken cancellationToken)
        {
            var customerOriginBytes = Encoding.UTF8.GetBytes(customerOrigin.ToString());
            await _distributedCache.SetAsync($"{TRADING_CODE_ORIGIN}{tradingCode}", customerOriginBytes, cancellationToken);
        }
    }
}
