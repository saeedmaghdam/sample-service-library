using ITnCare.Gateway.Enums;

namespace ITnCare.Gateway.RequestModels.Commission
{
    public class CalculateModel
    {
        public OrderSideEnum OrderSide { get; set; }
        public decimal Price { get; set; }
        public long Quantity { get; set; }
        public string TradingCode { get; set; }
    }
}
