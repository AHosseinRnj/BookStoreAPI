using Application.Command.CreateOrder;
using Application.Query.GetOrder;

namespace Application.Services
{
    public interface IOrderService
    {
        Task AddAsync(CreateOrderCommand request);
        Task<IEnumerable<GetOrderQueryResponse>> GetOrdersAsync();
        Task<GetOrderQueryResponse> GetOrderByIdAsync(int id);
    }
}