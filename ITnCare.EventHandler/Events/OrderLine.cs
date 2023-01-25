namespace ITnCare.EventHandler.Events
{
    public class OrderLine
    {
        public string ProductId { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
    }
}
