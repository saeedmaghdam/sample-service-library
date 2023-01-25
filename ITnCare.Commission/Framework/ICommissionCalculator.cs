using ITnCare.Commission.Enums;
using ITnCare.Commission.ViewModels;

namespace ITnCare.Commission.Framework
{
    public interface ICommissionCalculator
    {
        CalculateViewModel Calculate(OrderSideEnum orderSide, decimal price, long quantity, CustomerOriginEnum customerOrigin);
    }
}
