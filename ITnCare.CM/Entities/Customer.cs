using ITnCare.CM.Enums;

namespace ITnCare.CM.Entities
{
    public class Customer
    {
        public long Id { get; set; }
        public string TradingID { get; set; }
        public CustomerOriginEnum Origin { get; set; }
    }
}
