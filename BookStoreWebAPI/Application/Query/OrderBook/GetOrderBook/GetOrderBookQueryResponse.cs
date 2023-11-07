namespace Application.Query.GetOrderBook
{
    public class GetOrderBookQueryResponse
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}