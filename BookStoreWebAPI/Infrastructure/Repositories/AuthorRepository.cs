using Application.Repositories;
using Dapper;
using Domain.Entities;
using System.Data;

namespace Infrastructure.Repositories
{
    public class AuthorRepository : Repository, IAuthorRepository
    {
        public AuthorRepository(IDbTransaction transaction) : base(transaction) { }

        public async Task AddAsync(Author entity)
        {
            var query = "INSERT INTO Author (Name, Biography) VALUES (@Name, @Biography)";
            await Connection.ExecuteAsync(
                query,
                param: new { Name = entity.Name, Biography = entity.Biography },
                transaction: Transaction
            );
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            var query = "SELECT * FROM Author";
            return await Connection.QueryAsync<Author>(query);
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Author WHERE Id = @autId";
            return await Connection.QueryFirstOrDefaultAsync<Author>(
                        query,
                        param: new { autId = id },
                        transaction: Transaction
                        );
        }

        public async Task RemoveAsync(int id)
        {
            var query = "DELETE FROM Author WHERE Id = @autId";
            await Connection.ExecuteAsync(
                        query,
                        param: new { autId = id },
                        transaction: Transaction
                        );
        }

        public async Task UpdateAsync(int id, Author entity)
        {
            var query = "UPDATE Author SET Name = @Name, Biography = @Biography WHERE Id = @autId";
            await Connection.ExecuteAsync(
                        query,
                        param: new { Name = entity.Name, Biography =  entity.Biography, autId = id },
                        transaction: Transaction
                        );
        }
    }
}