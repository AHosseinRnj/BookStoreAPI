using Application.Command.CreateOrder;
using Application.Query.GetOrder;
using Application.Repositpries;
using Dapper;
using System.Data;

namespace Infrastructure.Persistance.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IDbTransaction _transaction;
        private readonly IDbConnection _connection;
        public OrderRepository(IDbTransaction dbTransaction)
        {
            _transaction = dbTransaction;
            _connection = _transaction.Connection;
        }

        public async Task AddAsync(CreateOrderCommand order)
        {
            var query = "INSERT INTO [Order] (Id, TotalPrice, OrderDate, userId) VALUES (@Id, @TotalPrice, @OrderDate, @userId)";

            var parameters = new DynamicParameters();
            parameters.Add("Id", order.id, DbType.Int32);
            parameters.Add("TotalPrice", order.totalPrice, DbType.Double);
            parameters.Add("OrderDate", order.orderDate, DbType.DateTime);
            parameters.Add("userId", order.userId, DbType.Int32);

            await _connection.ExecuteAsync(query, parameters, _transaction);
        }

        public async Task<GetOrderQueryResponse> GetOrderByIdAsync(int id)
        {
            var query = "SELECT * FROM [Order] WHERE Id = @Id";
            var order = await _connection.QueryFirstAsync<GetOrderQueryResponse>(query, new { id }, _transaction);

            return order;
        }

        public async Task<IEnumerable<GetOrderQueryResponse>> GetOrdersAsync()
        {
            var query = "SELECT * FROM [Order]";
            var listOfOrders = await _connection.QueryAsync<GetOrderQueryResponse>(query, null, _transaction);

            return listOfOrders;
        }
    }
}