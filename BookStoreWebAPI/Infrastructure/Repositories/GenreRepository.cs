using Application.Repositories;
using Dapper;
using Domain.Entities;
using System.Data;

namespace Infrastructure.Repositories
{
    public class GenreRepository : Repository, IGenreRepository
    {
        public GenreRepository(IDbTransaction transaction) : base(transaction) { }

        public async Task AddAsync(Genre entity)
        {
            var query = "INSERT INTO Genre (Name) VALUES (@Name)";
            await Connection.ExecuteAsync(
                query,
                param: new { Name = entity.Name },
                transaction: Transaction
            );
        }

        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            var query = "SELECT * FROM Genre";
            return await Connection.QueryAsync<Genre>(query);
        }

        public async Task<Genre> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Genre WHERE Id = @genId";
            return await Connection.QueryFirstOrDefaultAsync<Genre>(
                        query,
                        param: new { genId = id },
                        transaction: Transaction
                        );
        }

        public async Task RemoveAsync(int id)
        {
            var query = "DELETE FROM Genre WHERE Id = @genId";
            await Connection.ExecuteAsync(
                        query,
                        param: new { genId = id },
                        transaction: Transaction
                        );
        }

        public async Task UpdateAsync(int id, Genre entity)
        {
            var query = "UPDATE Genre SET Name = @Name WHERE Id = @genId";
            await Connection.ExecuteAsync(
                        query,
                        param: new { Name = entity.Name, genId = id },
                        transaction: Transaction
                        );
        }
    }
}