using Application.Repositories;
using Dapper;
using Domain.Entities;
using System.Data;

namespace Infrastructure.Persistance.Repositories
{
    public class CategoryWriteRepository : ICategoryWriteRepository
    {
        private readonly DapperContext _dapperContext;
        public CategoryWriteRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task AddAsync(Category category)
        {
            var query = "INSERT INTO Category (id, name) VALUES (@id, @Name)";

            var parameters = new DynamicParameters();
            parameters.Add("id", category.Id, DbType.Int32);
            parameters.Add("name", category.Name, DbType.String);

            await _dapperContext.Connection.ExecuteAsync(query, parameters, _dapperContext.Transaction);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var query = "DELETE FROM Category WHERE id = @Id";
            await _dapperContext.Connection.ExecuteAsync(query, new { id }, _dapperContext.Transaction);
        }

        public async Task UpdateAsync(Category category)
        {
            var query = "UPDATE Category SET name = @Name WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", category.Id, DbType.Int32);
            parameters.Add("name", category.Name, DbType.String);

            await _dapperContext.Connection.ExecuteAsync(query, parameters, _dapperContext.Transaction);
        }
    }
}