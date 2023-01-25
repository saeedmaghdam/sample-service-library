using ITnCare.Commission.Commissions;
using ITnCare.Commission.Enums;
using ITnCare.Commission.Framework;
using ITnCare.Commission.ViewModels;

namespace ITnCare.Commission.Services
{
    public class CommissionCalculator : ICommissionCalculator
    {
        private readonly ShareOrRight _shareOrRight;

        public CommissionCalculator()
        {
            _shareOrRight = new ShareOrRight(TradeKindEnum.Retail);
        }

        public CalculateViewModel Calculate(OrderSideEnum orderSide, decimal price, long quantity, CustomerOriginEnum customerOrigin)
        {
            var result = _shareOrRight.Calculate(orderSide, price, quantity, customerOrigin);

            return new CalculateViewModel
            {
                TotalFee = result.TotalFee,
                TotalTax = result.TotalTax
            };
        }
    }
}
