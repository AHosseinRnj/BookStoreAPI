namespace Application.Commands.CreateOrder
{
    public class CartItems
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }

    public class CreateOrderRequest
    {
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; }
        public List<CartItems> Items { get; set; }
    }
}