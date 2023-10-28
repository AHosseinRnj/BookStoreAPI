using Application.Query.Author.GetAuthor;
using Application.Repositpries;
using Dapper;
using Domain.Entities;
using System.Data;

namespace Infrastructure.Persistance.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly IDbTransaction _transaction;
        private readonly IDbConnection _connection;
        public AuthorRepository(IDbTransaction dbTransaction)
        {
            _transaction = dbTransaction;
            _connection = _transaction.Connection;
        }

        public async Task AddAsync(Author author)
        {
            var query = "INSERT INTO Author (id, firstname, lastname, description) VALUES (@id, @FirstName, @LastName, @Description)";
            var parameters = new DynamicParameters();
            parameters.Add("id", author.Id, DbType.Int32);
            parameters.Add("FirstName", author.FirstName, DbType.String);
            parameters.Add("LastName", author.LastName, DbType.String);
            parameters.Add("Description", author.Description, DbType.String);

            await _connection.ExecuteAsync(query, parameters, _transaction);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var query = "DELETE FROM Author WHERE id=@Id";
            await _connection.ExecuteAsync(query, new { id }, _transaction);
        }

        public async Task<GetAuthorQueryResponse> GetAuthorById(int id)
        {
            var query = "SELECT * FROM Author WHERE id=@Id";
            var author = await _connection.QueryFirstAsync<Author>(query, new { id }, _transaction);
            var result = new GetAuthorQueryResponse()
            {
                FirstName = author.FirstName,
                LastName = author.LastName,
                Description = author.Description
            };
            return result;
        }

        public async Task UpdateAsync(Application.Commands.UpdateAuthor.UpdateAuthorCommand author)
        {
            var query = "UPDATE Author SET firstname=@FirstName, lastname = @LastName, description=@Description WHERE Id=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("id", author.id, DbType.Int32);
            parameters.Add("FirstName", author.FirstName, DbType.String);
            parameters.Add("LastName", author.LastName, DbType.String);
            parameters.Add("Description", author.Description, DbType.String);

            await _connection.ExecuteAsync(query, parameters, _transaction);
        }
    }
}