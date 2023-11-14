namespace Application.Query.GetOrder
{
    public class GetOrderQueryResponse
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; }
    }
}