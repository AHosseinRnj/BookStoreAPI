using Application.Repositories;
using Dapper;
using Domain.Entities;
using System.Data;

namespace Infrastructure.Persistance.Repositories
{
    public class OrderWriteRepository : IOrderWriteRepository
    {
        private readonly DapperContext _dapperContext;
        public OrderWriteRepository(DapperContext dapperContext)
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
    }
}