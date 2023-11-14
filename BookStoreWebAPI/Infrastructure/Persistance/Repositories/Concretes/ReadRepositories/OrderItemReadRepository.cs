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
            var query = "SELECT OI.Id, OI.BookId, OI.OrderId, OI.Quantity, OI.Price FROM Books AS B JOIN OrderItems AS OI ON B.Id = OI.BookId";
            var orderBooks = await _dapperContext.Connection.QueryAsync<OrderItem>(query, null, _dapperContext.Transaction);

            return orderBooks;
        }
    }
}