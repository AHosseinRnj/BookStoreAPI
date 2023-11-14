using Domain.Entities;

namespace Application.Repositories
{
    public interface IOrderItemReadRepository
    {
        Task<IEnumerable<OrderItem>> GetOrderItemAsync();
    }
}