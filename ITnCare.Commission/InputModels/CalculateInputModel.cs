using ITnCare.Commission.Enums;

namespace ITnCare.Commission.InputModels
{
    public class CalculateInputModel
    {
        public OrderSideEnum OrderSide { get; set; }
        public decimal Price { get; set; }
        public long Quantity { get; set; }
        public CustomerOriginEnum CustomerOrigin { get; set; }
    }
}
