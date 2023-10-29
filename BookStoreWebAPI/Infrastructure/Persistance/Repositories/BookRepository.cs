using Application.Commands.UpdateBook;
using Application.Query.GetBook;
using Application.Query.GetBooks;
using Application.Repositpries;
using Dapper;
using Domain.Entities;
using System.Data;

namespace Infrastructure.Persistance.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly IDbTransaction _transaction;
        private readonly IDbConnection _connection;
        public BookRepository(IDbTransaction dbTransaction)
        {
            _transaction = dbTransaction;
            _connection = _transaction.Connection;
        }

        public async Task AddAsync(Book book)
        {
            var query = "INSERT INTO Book (Id, Title, ISBN, Price, AuthorId, PublisherId) VALUES (@Id, @Title, @ISBN, @Price, @AuthorId, @PublisherId)";

            var parameters = new DynamicParameters();
            parameters.Add("id", book.Id, DbType.Int32);
            parameters.Add("Title", book.Title, DbType.String);
            parameters.Add("ISBN", book.ISBN, DbType.String);
            parameters.Add("Price", book.Price, DbType.Double);
            parameters.Add("AuthorId", book.AuthorId, DbType.Int32);
            parameters.Add("PublisherId", book.PublisherId, DbType.Int32);

            await _connection.ExecuteAsync(query, parameters, _transaction);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var query = "DELETE FROM Book WHERE id = @Id";
            await _connection.ExecuteAsync(query, new { id }, _transaction);
        }

        public async Task<IEnumerable<GetBookQueryResponse>> GetBooksAsync()
        {
            var query = "SELECT * FROM Book";
            var listOfBooks = await _connection.QueryAsync<GetBookQueryResponse>(query, null, _transaction);

            return listOfBooks;
        }


        public async Task<GetBookQueryResponse> GetBookByIdAsync(int id)
        {
            var query = "SELECT * FROM Book WHERE id=@Id";
            var book = await _connection.QueryFirstAsync<GetBookQueryResponse>(query, new { id }, _transaction);

            return book;
        }

        public async Task UpdateAsync(UpdateBookCommand book)
        {
            var query = "UPDATE Book SET Title = @Title, ISBN = @ISBN, Price = @Price, AuthorId = @AuthorId, PublisherId = @PublisherId WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", book.id, DbType.Int32);
            parameters.Add("Title", book.Title, DbType.String);
            parameters.Add("ISBN", book.ISBN, DbType.String);
            parameters.Add("Price", book.price, DbType.Double);
            parameters.Add("AuthorId", book.AuthorId, DbType.Int32);
            parameters.Add("PublisherId", book.PublisherId, DbType.Int32);

            await _connection.ExecuteAsync(query, parameters, _transaction);
        }
    }
}