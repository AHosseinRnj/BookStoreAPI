using Domain.Entities;

namespace Application.Repositories
{
    public interface IOrderReadRepository
    {
        Task<IEnumerable<Order>> GetOrdersAsync();
        Task<Order> GetOrderByIdAsync(int id);
    }
}