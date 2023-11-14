using Application.Repositories;
using Dapper;
using Domain.Entities;

namespace Infrastructure.Persistance.Repositories
{
    public class OrderItemReadRepository : IOrderItemReadRepository
    {
        private readonly DapperContext _dapperContext;
        public OrderItemReadRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemAsync()
        {
            var query = "SELECT OB.BookId, OB.Quantity, OB.Price FROM Book AS B JOIN OrderBook AS OB ON B.Id = OB.BookId";
            var orderBooks = await _dapperContext.Connection.QueryAsync<OrderItem>(query, null, _dapperContext.Transaction);

            return orderBooks;
        }
    }
}