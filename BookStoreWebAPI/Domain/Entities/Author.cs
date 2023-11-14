namespace Domain.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Biography { get; set; }

        // Relationships :

        public IEnumerable<Book> Books { get; set; }
    }
}