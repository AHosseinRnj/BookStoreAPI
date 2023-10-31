namespace Domain.Entities
{
    public class OrderBook
    {
        public int OrderId { get; set; }
        public int BookId{ get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}