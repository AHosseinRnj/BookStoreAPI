namespace Domain.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int Amount { get; set; }
        public double Price { get; set; }

        public Book Book { get; set; }
        public Order Order { get; set; }
    }
}