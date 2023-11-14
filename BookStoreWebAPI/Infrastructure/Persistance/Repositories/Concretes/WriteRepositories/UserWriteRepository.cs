using Application.Repositories;
using Dapper;
using Domain.Entities;
using System.Data;

namespace Infrastructure.Persistance.Repositories
{
    public class UserWriteRepository : IUserWriteRepository
    {
        private readonly DapperContext _dapperContext;
        public UserWriteRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task AddAsync(User user)
        {
            var query = "INSERT INTO [dbo].[Users] (Id, FirstName, LastName, Address, Phone) VALUES (@Id, @FirstName, @LastName, @Address, @Phone)";

            var parameters = new DynamicParameters();
            parameters.Add("Id", user.Id, DbType.Int32);
            parameters.Add("FirstName", user.FirstName, DbType.String);
            parameters.Add("LastName", user.LastName, DbType.String);
            parameters.Add("Address", user.Address, DbType.String);
            parameters.Add("Phone", user.Phone, DbType.String);

            await _dapperContext.Connection.ExecuteAsync(query, parameters, _dapperContext.Transaction);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var query = "DELETE FROM [dbo].[Users] WHERE id = @Id";
            await _dapperContext.Connection.ExecuteAsync(query, new { id }, _dapperContext.Transaction);
        }

        public async Task UpdateAsync(User user)
        {
            var query = "UPDATE [dbo].[Users] SET FirstName = @FirstName, LastName = @LastName, Address = @Address, Phone = @Phone WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", user.Id, DbType.Int32);
            parameters.Add("FirstName", user.FirstName, DbType.String);
            parameters.Add("LastName", user.LastName, DbType.String);
            parameters.Add("Address", user.Address, DbType.String);
            parameters.Add("Phone", user.Phone, DbType.String);

            await _dapperContext.Connection.ExecuteAsync(query, parameters, _dapperContext.Transaction);
        }
    }
}