using Application.Repositories;
using Dapper;
using Domain.Entities;

namespace Infrastructure.Persistance.Repositories
{
    public class BookReadRepository : IBookReadRepository
    {
        private readonly DapperContext _dapperContext;
        public BookReadRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            var query = "SELECT * FROM Book";
            var listOfBooks = await _dapperContext.Connection.QueryAsync<Book>(query, null, _dapperContext.Transaction);

            return listOfBooks;
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            var query = "SELECT * FROM Book WHERE id=@Id";
            var book = await _dapperContext.Connection.QueryFirstAsync<Book>(query, new { id }, _dapperContext.Transaction);

            return book;
        }
    }
}