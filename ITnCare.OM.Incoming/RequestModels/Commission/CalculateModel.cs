using ITnCare.OM.Incoming.Enums;

namespace ITnCare.OM.Incoming.RequestModels.Commission
{
    public class CalculateModel
    {
        public OrderSideEnum OrderSide { get; set; }
        public decimal Price { get; set; }
        public long Quantity { get; set; }
        public CustomerOriginEnum CustomerOrigin { get; set; }
    }
}
