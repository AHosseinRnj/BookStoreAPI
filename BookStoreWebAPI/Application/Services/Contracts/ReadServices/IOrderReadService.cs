using Application.Query.GetOrder;

namespace Application.Services
{
    public interface IOrderReadService
    {
        Task<IEnumerable<GetOrderQueryResponse>> GetOrdersAsync();
        Task<GetOrderQueryResponse> GetOrderByIdAsync(int id);
    }
}