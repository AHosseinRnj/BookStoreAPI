using Application.Command.CreateOrder;
using Application.Query.GetOrder;

namespace Application.Repositpries
{
    public interface IOrderRepository
    {
        Task AddAsync(CreateOrderCommand order);
        Task<IEnumerable<GetOrderQueryResponse>> GetOrdersAsync();
        Task<GetOrderQueryResponse> GetOrderByIdAsync(int id);
    }
}