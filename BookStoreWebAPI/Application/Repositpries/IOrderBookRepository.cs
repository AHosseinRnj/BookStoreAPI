using Application.Commands.CreateOrderBook;
using Application.Query.GetOrderBook;

namespace Application.Repositpries
{
    public interface IOrderBookRepository
    {
        Task AddAsync(CreateOrderBookCommand orderBook);
        Task<IEnumerable<GetOrderBookQueryResponse>> GetOrderBooksAsync();
    }
}