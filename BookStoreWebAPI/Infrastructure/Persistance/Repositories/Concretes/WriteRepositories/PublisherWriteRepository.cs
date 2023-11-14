using Application.Repositories;
using Dapper;
using Domain.Entities;
using System.Data;

namespace Infrastructure.Persistance.Repositories
{
    public class PublisherWriteRepository : IPublisherWriteRepository
    {
        private readonly DapperContext _dapperContext;
        public PublisherWriteRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task AddAsync(Publisher publisher)
        {
            var query = "INSERT INTO Publishers (id, name, Description) VALUES (@id, @Name, @Description)";

            var parameters = new DynamicParameters();
            parameters.Add("id", publisher.Id, DbType.Int32);
            parameters.Add("name", publisher.Name, DbType.String);
            parameters.Add("Description", publisher.Description, DbType.String);

            await _dapperContext.Connection.ExecuteAsync(query, parameters, _dapperContext.Transaction);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var query = "DELETE FROM Publishers WHERE id = @Id";
            await _dapperContext.Connection.ExecuteAsync(query, new { id }, _dapperContext.Transaction);
        }

        public async Task UpdateAsync(Publisher publisher)
        {
            var query = "UPDATE Publishers SET name = @Name, Description = @Description WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", publisher.Id, DbType.Int32);
            parameters.Add("name", publisher.Name, DbType.String);
            parameters.Add("Description", publisher.Description, DbType.String);

            await _dapperContext.Connection.ExecuteAsync(query, parameters, _dapperContext.Transaction);
        }
    }
}