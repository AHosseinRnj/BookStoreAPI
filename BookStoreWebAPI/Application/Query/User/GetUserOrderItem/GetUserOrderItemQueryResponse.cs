namespace Application.Query.GetUserOrders
{
    public class GetUserOrderItemQueryResponse
    {
        public string Title { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double TotalPrice { get; set; }
    }
}