namespace Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        // Relationships :

        public IEnumerable<OrderItem> OrderItems { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}