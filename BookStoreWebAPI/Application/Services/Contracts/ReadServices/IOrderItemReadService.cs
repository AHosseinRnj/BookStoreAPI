using Application.Query.GetOrderBook;

namespace Application.Services
{
    public interface IOrderItemReadService
    {
        Task<IEnumerable<GetOrderItemQueryResponse>> GetOrderBooksAsync();
    }
}