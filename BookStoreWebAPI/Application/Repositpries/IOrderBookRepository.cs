using Domain.Entities;

namespace Application.Repositpries
{
    public interface IOrderBookRepository
    {
        Task AddAsync(OrderBook orderBook);
        Task<IEnumerable<OrderBook>> GetOrderBooksAsync();
    }
}