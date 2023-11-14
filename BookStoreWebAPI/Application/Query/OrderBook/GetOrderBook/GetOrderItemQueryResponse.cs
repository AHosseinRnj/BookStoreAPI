namespace Application.Query.GetOrderBook
{
    public class GetOrderItemQueryResponse
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public int OrderId { get; set; }
        public int BookId { get; set; }
    }
}