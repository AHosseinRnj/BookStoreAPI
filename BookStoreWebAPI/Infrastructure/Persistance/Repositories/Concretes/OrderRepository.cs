using Application.Repositpries;
using Dapper;
using Domain.Entities;
using System.Data;

namespace Infrastructure.Persistance.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DapperContext _dapperContext;
        public OrderRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task AddAsync(Order order)
        {
            var query = "INSERT INTO [Order] (Id, OrderDate, userId) VALUES (@Id, @OrderDate, @userId)";

            var parameters = new DynamicParameters();
            parameters.Add("Id", order.Id, DbType.Int32);
            parameters.Add("OrderDate", order.OrderDate, DbType.DateTime);
            parameters.Add("userId", order.UserId, DbType.Int32);

            await _dapperContext.Connection.ExecuteAsync(query, parameters, _dapperContext.Transaction);
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            var query = "SELECT * FROM [Order] WHERE Id = @Id";
            var order = await _dapperContext.Connection.QueryFirstAsync<Order>(query, new { id }, _dapperContext.Transaction);

            return order;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            var query = "SELECT * FROM [Order]";
            var listOfOrders = await _dapperContext.Connection.QueryAsync<Order>(query, null, _dapperContext.Transaction);

            return listOfOrders;
        }
    }
}