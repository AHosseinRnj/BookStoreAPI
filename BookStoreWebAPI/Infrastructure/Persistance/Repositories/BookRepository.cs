using Application.Commands.CreateBook;
using Application.Commands.UpdateBook;
using Application.Query.GetBook;
using Application.Repositpries;
using Dapper;
using System.Data;

namespace Infrastructure.Persistance.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly DapperContext _dapperContext;
        public BookRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task AddAsync(CreateBookCommand book)
        {
            var query = "INSERT INTO Book (Id, Title, ISBN, Price, AuthorId, PublisherId, CategoryId) VALUES " +
                "                         (@Id, @Title, @ISBN, @Price, @AuthorId, @PublisherId, @CategoryId)";

            var parameters = new DynamicParameters();
            parameters.Add("id", book.Id, DbType.Int32);
            parameters.Add("Title", book.Title, DbType.String);
            parameters.Add("ISBN", book.ISBN, DbType.String);
            parameters.Add("Price", book.Price, DbType.Double);
            parameters.Add("AuthorId", book.AuthorId, DbType.Int32);
            parameters.Add("PublisherId", book.PublisherId, DbType.Int32);
            parameters.Add("CategoryId", book.CategoryId, DbType.Int32);

            await _dapperContext.Connection.ExecuteAsync(query, parameters, _dapperContext.Transaction);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var query = "DELETE FROM Book WHERE id = @Id";
            await _dapperContext.Connection.ExecuteAsync(query, new { id }, _dapperContext.Transaction);
        }

        public async Task<IEnumerable<GetBookQueryResponse>> GetBooksAsync()
        {
            var query = "SELECT * FROM Book";
            var listOfBooks = await _dapperContext.Connection.QueryAsync<GetBookQueryResponse>(query, null, _dapperContext.Transaction);

            return listOfBooks;
        }


        public async Task<GetBookQueryResponse> GetBookByIdAsync(int id)
        {
            var query = "SELECT * FROM Book WHERE id=@Id";
            var book = await _dapperContext.Connection.QueryFirstAsync<GetBookQueryResponse>(query, new { id }, _dapperContext.Transaction);

            return book;
        }

        public async Task UpdateAsync(UpdateBookCommand book)
        {
            var query = "UPDATE Book SET Title = @Title, ISBN = @ISBN, Price = @Price, AuthorId = @AuthorId, PublisherId = @PublisherId," +
                                                " CategoryId = @CategoryId WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", book.id, DbType.Int32);
            parameters.Add("Title", book.Title, DbType.String);
            parameters.Add("ISBN", book.ISBN, DbType.String);
            parameters.Add("Price", book.price, DbType.Double);
            parameters.Add("AuthorId", book.AuthorId, DbType.Int32);
            parameters.Add("PublisherId", book.PublisherId, DbType.Int32);
            parameters.Add("CategoryId", book.CategoryId, DbType.Int32);

            await _dapperContext.Connection.ExecuteAsync(query, parameters, _dapperContext.Transaction);
        }
    }
}