using Application.Commands.CreateOrderBook;
using Application.Query.GetOrderBook;

namespace Application.Services
{
    public interface IOrderBookService
    {
        Task AddAsync(CreateOrderItemCommand request);
        Task<IEnumerable<GetOrderBookQueryResponse>> GetOrderBooksAsync();
    }
}