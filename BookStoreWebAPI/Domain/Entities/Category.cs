namespace Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Relationships :

        public IEnumerable<Book> Books { get; set; }
    }
}