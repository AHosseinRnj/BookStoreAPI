using Application.Commands.CreateOrderBook;
using Application.Query.GetOrderBook;
using Application.Repositpries;
using Dapper;
using System.Data;

namespace Infrastructure.Persistance.Repositories
{
    public class OrderBookRepository : IOrderBookRepository
    {
        private readonly DapperContext _dapperContext;
        public OrderBookRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task AddAsync(CreateOrderBookCommand orderBook)
        {
            var query = "INSERT INTO [OrderBook] (OrderId, BookId, Quantity, Price) VALUES (@OrderId, @BookId, @Quantity, @Price)";

            var parameters = new DynamicParameters();
            parameters.Add("OrderId", orderBook.orderId, DbType.Int32);
            parameters.Add("BookId", orderBook.bookId, DbType.Int32);
            parameters.Add("Quantity", orderBook.quantity, DbType.Int32);
            parameters.Add("Price", orderBook.Price, DbType.Double);

            await _dapperContext.Connection.ExecuteAsync(query, parameters, _dapperContext.Transaction);
        }

        public async Task<IEnumerable<GetOrderBookQueryResponse>> GetOrderBooksAsync()
        {
            var query = "SELECT B.Title, OB.Quantity, OB.Price FROM Book AS B JOIN OrderBook AS OB ON B.Id = OB.BookId";
            var orderBooks = await _dapperContext.Connection.QueryAsync<GetOrderBookQueryResponse>(query, null, _dapperContext.Transaction);

            return orderBooks;
        }
    }
}