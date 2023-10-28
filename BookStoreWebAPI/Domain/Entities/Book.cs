namespace Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? ISBN { get; set; }
        public double Price { get; set; }

        public int AuthorId { get; set; }
    }
}