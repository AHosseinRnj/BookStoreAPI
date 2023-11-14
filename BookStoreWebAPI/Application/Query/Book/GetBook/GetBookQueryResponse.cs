namespace Application.Query.GetBook
{
    public class GetBookQueryResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}