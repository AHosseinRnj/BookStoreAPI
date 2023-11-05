using Application.Command.CreateOrder;
using Application.Query.GetOrder;
using Application.Repositpries;
using Dapper;
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

        public async Task AddAsync(CreateOrderCommand order)
        {
            var query = "INSERT INTO [Order] (Id, TotalPrice, OrderDate, userId) VALUES (@Id, @TotalPrice, @OrderDate, @userId)";

            var parameters = new DynamicParameters();
            parameters.Add("Id", order.id, DbType.Int32);
            parameters.Add("TotalPrice", order.totalPrice, DbType.Double);
            parameters.Add("OrderDate", order.orderDate, DbType.DateTime);
            parameters.Add("userId", order.userId, DbType.Int32);

            await _dapperContext.Connection.ExecuteAsync(query, parameters, _dapperContext.Transaction);
        }

        public async Task<GetOrderQueryResponse> GetOrderByIdAsync(int id)
        {
            var query = "SELECT * FROM [Order] WHERE Id = @Id";
            var order = await _dapperContext.Connection.QueryFirstAsync<GetOrderQueryResponse>(query, new { id }, _dapperContext.Transaction);

            return order;
        }

        public async Task<IEnumerable<GetOrderQueryResponse>> GetOrdersAsync()
        {
            var query = "SELECT * FROM [Order]";
            var listOfOrders = await _dapperContext.Connection.QueryAsync<GetOrderQueryResponse>(query, null, _dapperContext.Transaction);

            return listOfOrders;
        }
    }
}