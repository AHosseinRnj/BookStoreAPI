using Application.Repositpries;
using Dapper;
using Domain.Entities;
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

        public async Task AddAsync(OrderItem orderBook)
        {
            var query = "INSERT INTO [OrderBook] (OrderId, BookId, Quantity, Price) VALUES (@OrderId, @BookId, @Quantity, @Price)";

            var parameters = new DynamicParameters();
            parameters.Add("OrderId", orderBook.OrderId, DbType.Int32);
            parameters.Add("BookId", orderBook.BookId, DbType.Int32);
            parameters.Add("Quantity", orderBook.Quantity, DbType.Int32);
            parameters.Add("Price", orderBook.Price, DbType.Double);

            await _dapperContext.Connection.ExecuteAsync(query, parameters, _dapperContext.Transaction);
        }

        public async Task<IEnumerable<OrderItem>> GetOrderBooksAsync()
        {
            var query = "SELECT OB.BookId, OB.Quantity, OB.Price FROM Book AS B JOIN OrderBook AS OB ON B.Id = OB.BookId";
            var orderBooks = await _dapperContext.Connection.QueryAsync<OrderItem>(query, null, _dapperContext.Transaction);

            return orderBooks;
        }
    }
}