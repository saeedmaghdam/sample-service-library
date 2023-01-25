using ITnCare.CM.Enums;

namespace ITnCare.CM.Framework
{
    public interface ICacheHelper
    {
        Task SetTradingCodeOrigin(string tradingCode, CustomerOriginEnum customerOrigin, CancellationToken cancellationToken);
        Task<CustomerOriginEnum?> GetTradingCodeOrigin(string tradingCode, CancellationToken cancellationToken);
    }
}
