using Application.Repositories;
using Dapper;
using Domain.Entities;
using System.Data;

namespace Infrastructure.Repositories
{
    public class BookRepository : Repository, IBookRepository
    {
        public BookRepository(IDbTransaction transaction) : base(transaction) { }

        public async Task AddAsync(Book entity)
        {
            var query = "INSERT INTO Book (Title, ISBN, Price, PublicationDate, AuthorId, GenreId) " +
            "VALUES (@Title, @ISBN, @Price, @PublicationDate, @AuthorId, @GenreId)";
            await Connection.ExecuteAsync(
                query,
                param: new
                {
                    entity.Title,
                    entity.ISBN,
                    entity.Price,
                    entity.PublicationDate,
                    entity.AuthorId,
                    entity.GenreId
                },
                transaction: Transaction
            );
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            string query = "SELECT * FROM Book";
            return await Connection.QueryAsync<Book>(query);
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Book WHERE Id = @Id";
            return await Connection.QueryFirstOrDefaultAsync<Book>(
                            query,
                            new { Id = id },
                            Transaction
                            );
        }

        public async Task RemoveAsync(int id)
        {
            var query = "DELETE FROM Book WHERE Id = @Id";
            await Connection.ExecuteAsync(
                        query,
                        new { Id = id },
                        Transaction);
        }

        public async Task UpdateAsync(int id, Book entity)
        {
            var query = "UPDATE Book " +
            "SET Title = @Title, ISBN = @ISBN, Price = @Price, PublicationDate = @PublicationDate, " +
            "AuthorId = @AuthorId, GenreId = @GenreId " +
            "WHERE Id = @Id";
            await Connection.ExecuteAsync(
                query,
                param: new
                {
                    entity.Title,
                    entity.ISBN,
                    entity.Price,
                    entity.PublicationDate,
                    entity.AuthorId,
                    entity.GenreId,
                    Id = id
                },
                Transaction
            );
        }
    }
}