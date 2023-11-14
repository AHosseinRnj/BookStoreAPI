using Application.Repositories;
using Dapper;
using Domain.Entities;

namespace Infrastructure.Persistance.Repositories
{
    public class OrderReadRepository : IOrderReadRepository
    {
        private readonly DapperContext _dapperContext;
        public OrderReadRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            var query = "SELECT * FROM [Orders] WHERE Id = @Id";
            var order = await _dapperContext.Connection.QueryFirstAsync<Order>(query, new { id }, _dapperContext.Transaction);

            return order;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            var query = "SELECT * FROM [Orders]";
            var listOfOrders = await _dapperContext.Connection.QueryAsync<Order>(query, null, _dapperContext.Transaction);

            return listOfOrders;
        }
    }
}