using Domain.Entities;

namespace Application.Repositpries
{
    public interface IOrderBookRepository
    {
        Task AddAsync(OrderItem orderBook);
        Task<IEnumerable<OrderItem>> GetOrderBooksAsync();
    }
}