using Application.Repositories;
using Dapper;
using Domain.Entities;
using System.Data;

namespace Infrastructure.Persistance.Repositories
{
    public class AuthorWriteRepository : IAuthorWriteRepository
    {
        private readonly DapperContext _dapperContext;
        public AuthorWriteRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task AddAsync(Author author)
        {
            var query = "INSERT INTO Authors (Id, FirstName, LastName, Biography) VALUES (@Id, @FirstName, @LastName, @Biography)";

            var parameters = new DynamicParameters();
            parameters.Add("Id", author.Id, DbType.Int32);
            parameters.Add("FirstName", author.FirstName, DbType.String);
            parameters.Add("LastName", author.LastName, DbType.String);
            parameters.Add("Biography", author.Biography, DbType.String);

            await _dapperContext.Connection.ExecuteAsync(query, parameters, _dapperContext.Transaction);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var query = "DELETE FROM Authors WHERE id = @Id";
            await _dapperContext.Connection.ExecuteAsync(query, new { id }, _dapperContext.Transaction);
        }

        public async Task UpdateAsync(Author author)
        {
            var query = "UPDATE Authors SET firstname = @FirstName, lastname = @LastName, Biography = @Biography WHERE Id=@Id";

            var parameters = new DynamicParameters();
            parameters.Add("id", author.Id, DbType.Int32);
            parameters.Add("FirstName", author.FirstName, DbType.String);
            parameters.Add("LastName", author.LastName, DbType.String);
            parameters.Add("Biography", author.Biography, DbType.String);

            await _dapperContext.Connection.ExecuteAsync(query, parameters, _dapperContext.Transaction);
        }
    }
}