using Application.Repositpries;
using Dapper;
using Domain.Entities;
using System.Data;

namespace Infrastructure.Persistance.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly DapperContext _dapperContext;
        public AuthorRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task AddAsync(Author author)
        {
            var query = "INSERT INTO Author (Id, FirstName, LastName, Biography) VALUES (@Id, @FirstName, @LastName, @Biography)";

            var parameters = new DynamicParameters();
            parameters.Add("Id", author.Id, DbType.Int32);
            parameters.Add("FirstName", author.FirstName, DbType.String);
            parameters.Add("LastName", author.LastName, DbType.String);
            parameters.Add("Biography", author.Biography, DbType.String);

            await _dapperContext.Connection.ExecuteAsync(query, parameters, _dapperContext.Transaction);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var query = "DELETE FROM Author WHERE id = @Id";
            await _dapperContext.Connection.ExecuteAsync(query, new { id }, _dapperContext.Transaction);
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync()
        {
            var query = "SELECT * FROM Author";
            var listOfAuthors = await _dapperContext.Connection.QueryAsync<Author>(query, null, _dapperContext.Transaction);

            return listOfAuthors;
        }

        public async Task<Author> GetAuthorById(int id)
        {
            var query = "SELECT * FROM Author WHERE id = @Id";
            var author = await _dapperContext.Connection.QueryFirstAsync<Author>(query, new { id }, _dapperContext.Transaction);

            return author;
        }
        public async Task<IEnumerable<Book>> GetAuthorBooksAsync(int id)
        {
            var query = "SELECT Book.Title, Book.ISBN, Book.Price FROM Book JOIN Author ON Book.AuthorId = Author.Id WHERE Author.Id = @AutId";
            var listOfBooks = await _dapperContext.Connection.QueryAsync<Book>(query, new { AutId = id }, _dapperContext.Transaction);

            return listOfBooks;
        }

        public async Task UpdateAsync(Author author)
        {
            var query = "UPDATE Author SET firstname = @FirstName, lastname = @LastName, Biography = @Biography WHERE Id=@Id";

            var parameters = new DynamicParameters();
            parameters.Add("id", author.Id, DbType.Int32);
            parameters.Add("FirstName", author.FirstName, DbType.String);
            parameters.Add("LastName", author.LastName, DbType.String);
            parameters.Add("Biography", author.Biography, DbType.String);

            await _dapperContext.Connection.ExecuteAsync(query, parameters, _dapperContext.Transaction);
        }
    }
}