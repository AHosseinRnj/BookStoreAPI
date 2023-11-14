using Domain.Entities;

namespace Application.Repositpries
{
    public interface IOrderItemRepository
    {
        Task AddAsync(OrderItem orderBook);
        Task<IEnumerable<OrderItem>> GetOrderItemAsync();
    }
}