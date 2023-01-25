namespace ITnCare.Commission.Models
{
    public class Commission
    {
        public decimal TotalFee { get; private set; }
        public decimal TotalTax { get; private set; }

        public Commission(decimal totalFee, decimal totalTax)
        {
            TotalFee = totalFee;
            TotalTax = totalTax;
        }
    }
}
