using Application.Repositories;
using Dapper;
using Domain.Entities;

namespace Infrastructure.Persistance.Repositories
{
    public class AuthorReadRepository : IAuthorReadRepository
    {
        private readonly DapperContext _dapperContext;
        public AuthorReadRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<IEnumerable<Book>> GetAuthorBooksAsync(int id)
        {
            var query = "SELECT Book.Title, Book.ISBN, Book.Price FROM Books JOIN Authors ON Book.AuthorId = Author.Id WHERE Author.Id = @AutId";
            var listOfBooks = await _dapperContext.Connection.QueryAsync<Book>(query, new { AutId = id }, _dapperContext.Transaction);

            return listOfBooks;
        }

        public async Task<Author> GetAuthorById(int id)
        {
            var query = "SELECT * FROM Authors WHERE id = @Id";
            var author = await _dapperContext.Connection.QueryFirstAsync<Author>(query, new { id }, _dapperContext.Transaction);

            return author;
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync()
        {
            var query = "SELECT * FROM Authors";
            var listOfAuthors = await _dapperContext.Connection.QueryAsync<Author>(query, null, _dapperContext.Transaction);

            return listOfAuthors;
        }
    }
}