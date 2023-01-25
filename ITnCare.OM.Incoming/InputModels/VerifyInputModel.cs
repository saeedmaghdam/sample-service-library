using ITnCare.OM.Incoming.Enums;

namespace ITnCare.OM.Incoming.InputModels
{
    public class VerifyInputModel
    {
        public string TradingCode { get; set; }
        public OrderSideEnum OrderSide { get; set; }
        public decimal Price { get; set; }
        public long Quantity { get; set; }
    }
}
