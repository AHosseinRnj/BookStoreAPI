using Application.Commands.CreateOrderBook;
using Application.Query.GetOrderBook;
using Application.Repositpries;
using Dapper;
using System.Data;

namespace Infrastructure.Persistance.Repositories
{
    public class OrderBookRepository : IOrderBookRepository
    {
        private readonly IDbTransaction _transaction;
        private readonly IDbConnection _connection;
        public OrderBookRepository(IDbTransaction dbTransaction)
        {
            _transaction = dbTransaction;
            _connection = _transaction.Connection;
        }

        public async Task AddAsync(CreateOrderBookCommand orderBook)
        {
            var query = "INSERT INTO [OrderBook] (OrderId, BookId, Quantity, Price) VALUES (@OrderId, @BookId, @Quantity, @Price)";

            var parameters = new DynamicParameters();
            parameters.Add("OrderId", orderBook.orderId, DbType.Int32);
            parameters.Add("BookId", orderBook.bookId, DbType.Int32);
            parameters.Add("Quantity", orderBook.quantity, DbType.Int32);
            parameters.Add("Price", orderBook.Price, DbType.Double);

            await _connection.ExecuteAsync(query, parameters, _transaction);
        }

        public async Task<IEnumerable<GetOrderBookQueryResponse>> GetOrderBooksAsync()
        {
            var query = "SELECT B.Title, OB.Quantity, OB.Price FROM Book AS B JOIN OrderBook AS OB ON B.Id = OB.BookId";
            var orderBooks = await _connection.QueryAsync<GetOrderBookQueryResponse>(query, null, _transaction);

            return orderBooks;
        }
    }
}